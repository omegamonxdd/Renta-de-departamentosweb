using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using RentaDepartamentosWeb.Data;
using RentaDepartamentosWeb.Models;

namespace RentaDepartamentosWeb.Repositories
{
    /// <summary>
    /// Implementación del acceso a datos de Departamentos usando Microsoft.Data.SqlClient.
    /// Depende de ConexionBD (no de SqlConnection directamente), aplicando el
    /// Principio de Inversión de Dependencias.
    /// </summary>
    public class DepartamentoRepository : IDepartamentoRepository
    {
        private readonly ConexionBD _conexionBD;

        public DepartamentoRepository(ConexionBD conexionBD)
        {
            _conexionBD = conexionBD;
        }

        public void AgregarDepartamento(Departamento departamento)
        {
            using (SqlConnection conexion = _conexionBD.ObtenerConexion())
            {
                string consulta = @"INSERT INTO Departamentos
                (Direccion, Colonia, Ciudad, Habitaciones, Banios, PrecioRenta, Estado, Arrendatario, FechaInicioRenta)
                VALUES
                (@Direccion, @Colonia, @Ciudad, @Habitaciones, @Banios, @PrecioRenta, @Estado, @Arrendatario, @FechaInicioRenta)";

                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@Direccion", departamento.Direccion);
                comando.Parameters.AddWithValue("@Colonia", departamento.Colonia);
                comando.Parameters.AddWithValue("@Ciudad", departamento.Ciudad);
                comando.Parameters.AddWithValue("@Habitaciones", departamento.Habitaciones);
                comando.Parameters.AddWithValue("@Banios", departamento.Banios);
                comando.Parameters.AddWithValue("@PrecioRenta", departamento.PrecioRenta);
                comando.Parameters.AddWithValue("@Estado", departamento.Estado);
                // Enviamos DBNull.Value cuando el arrendatario es nulo; así el mapeo de
                // lectura (que compara con DBNull.Value) es totalmente consistente.
                comando.Parameters.AddWithValue("@Arrendatario",
                    departamento.Arrendatario is null ? (object)DBNull.Value : departamento.Arrendatario);
                comando.Parameters.AddWithValue("@FechaInicioRenta",
                    departamento.FechaInicioRenta.HasValue ? (object)departamento.FechaInicioRenta.Value : DBNull.Value);

                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        public List<Departamento> ObtenerDepartamentos()
        {
            var departamentos = new List<Departamento>();
            using (SqlConnection conexion = _conexionBD.ObtenerConexion())
            {
                string consulta = "SELECT Id, Direccion, Colonia, Ciudad, Habitaciones, Banios, PrecioRenta, Estado, Arrendatario, FechaInicioRenta FROM Departamentos";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                conexion.Open();
                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        departamentos.Add(MapearDepartamento(reader));
                    }
                }
            }
            return departamentos;
        }

        public Departamento ObtenerPorId(int id)
        {
            using (SqlConnection conexion = _conexionBD.ObtenerConexion())
            {
                string consulta = "SELECT Id, Direccion, Colonia, Ciudad, Habitaciones, Banios, PrecioRenta, Estado, Arrendatario, FechaInicioRenta FROM Departamentos WHERE Id = @Id";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@Id", id);
                conexion.Open();
                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return MapearDepartamento(reader);
                    }
                }
            }
            return null!;
        }

        public List<Departamento> BuscarPorCiudadOColonia(string texto)
        {
            var departamentos = new List<Departamento>();
            using (SqlConnection conexion = _conexionBD.ObtenerConexion())
            {
                string consulta = @"SELECT Id, Direccion, Colonia, Ciudad, Habitaciones, Banios, PrecioRenta, Estado, Arrendatario, FechaInicioRenta 
                                    FROM Departamentos 
                                    WHERE Ciudad LIKE @Texto OR Colonia LIKE @Texto";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@Texto", $"%{(texto ?? "")}%");
                conexion.Open();
                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        departamentos.Add(MapearDepartamento(reader));
                    }
                }
            }
            return departamentos;
        }

        public List<Departamento> BuscarPorFiltros(string ciudad, string colonia, string estado, decimal? precioMin, decimal? precioMax)
        {
            var departamentos = new List<Departamento>();
            using (SqlConnection conexion = _conexionBD.ObtenerConexion())
            {
                var consulta = new System.Text.StringBuilder(
                    "SELECT Id, Direccion, Colonia, Ciudad, Habitaciones, Banios, PrecioRenta, Estado, Arrendatario, FechaInicioRenta FROM Departamentos WHERE 1=1");
                
                var comandos = new List<SqlParameter>();

                if (!string.IsNullOrWhiteSpace(ciudad))
                {
                    consulta.Append(" AND Ciudad LIKE @Ciudad");
                    comandos.Add(new SqlParameter("@Ciudad", $"%{ciudad.Trim()}%"));
                }
                if (!string.IsNullOrWhiteSpace(colonia))
                {
                    consulta.Append(" AND Colonia LIKE @Colonia");
                    comandos.Add(new SqlParameter("@Colonia", $"%{colonia.Trim()}%"));
                }
                if (!string.IsNullOrWhiteSpace(estado))
                {
                    consulta.Append(" AND Estado = @Estado");
                    comandos.Add(new SqlParameter("@Estado", estado.Trim()));
                }
                if (precioMin.HasValue)
                {
                    consulta.Append(" AND PrecioRenta >= @PrecioMin");
                    comandos.Add(new SqlParameter("@PrecioMin", precioMin.Value));
                }
                if (precioMax.HasValue)
                {
                    consulta.Append(" AND PrecioRenta <= @PrecioMax");
                    comandos.Add(new SqlParameter("@PrecioMax", precioMax.Value));
                }

                SqlCommand comando = new SqlCommand(consulta.ToString(), conexion);
                foreach (var param in comandos)
                {
                    comando.Parameters.Add(param);
                }

                conexion.Open();
                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        departamentos.Add(MapearDepartamento(reader));
                    }
                }
            }
            return departamentos;
        }

        public List<Departamento> ObtenerDisponibles()
        {
            var departamentos = new List<Departamento>();
            using (SqlConnection conexion = _conexionBD.ObtenerConexion())
            {
                // Se parametriza el valor 'Disponible' por consistencia con el resto
                // de consultas del repositorio, aunque no exista riesgo de inyección aquí.
                string consulta = @"SELECT Id, Direccion, Colonia, Ciudad, Habitaciones, Banios, PrecioRenta, Estado, Arrendatario, FechaInicioRenta 
                                    FROM Departamentos 
                                    WHERE Estado = @Estado";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@Estado", "Disponible");
                conexion.Open();
                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        departamentos.Add(MapearDepartamento(reader));
                    }
                }
            }
            return departamentos;
        }

        public void ActualizarDepartamento(Departamento departamento)
        {
            using (SqlConnection conexion = _conexionBD.ObtenerConexion())
            {
                string consulta = @"UPDATE Departamentos 
                                    SET Direccion = @Direccion, 
                                        Colonia = @Colonia, 
                                        Ciudad = @Ciudad, 
                                        Habitaciones = @Habitaciones, 
                                        Banios = @Banios, 
                                        PrecioRenta = @PrecioRenta, 
                                        Estado = @Estado, 
                                        Arrendatario = @Arrendatario, 
                                        FechaInicioRenta = @FechaInicioRenta 
                                    WHERE Id = @Id";

                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@Id", departamento.Id);
                comando.Parameters.AddWithValue("@Direccion", departamento.Direccion);
                comando.Parameters.AddWithValue("@Colonia", departamento.Colonia);
                comando.Parameters.AddWithValue("@Ciudad", departamento.Ciudad);
                comando.Parameters.AddWithValue("@Habitaciones", departamento.Habitaciones);
                comando.Parameters.AddWithValue("@Banios", departamento.Banios);
                comando.Parameters.AddWithValue("@PrecioRenta", departamento.PrecioRenta);
                comando.Parameters.AddWithValue("@Estado", departamento.Estado);
                // Consistencia: DBNull.Value cuando el arrendatario es nulo.
                comando.Parameters.AddWithValue("@Arrendatario",
                    departamento.Arrendatario is null ? (object)DBNull.Value : departamento.Arrendatario);
                comando.Parameters.AddWithValue("@FechaInicioRenta",
                    departamento.FechaInicioRenta.HasValue ? (object)departamento.FechaInicioRenta.Value : DBNull.Value);

                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        public void EliminarDepartamento(int id)
        {
            using (SqlConnection conexion = _conexionBD.ObtenerConexion())
            {
                string consulta = "DELETE FROM Departamentos WHERE Id = @Id";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@Id", id);
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        public void CambiarEstado(int id, string nuevoEstado, string arrendatario, DateTime? fechaInicioRenta)
        {
            using (SqlConnection conexion = _conexionBD.ObtenerConexion())
            {
                string consulta = @"UPDATE Departamentos 
                                    SET Estado = @Estado, 
                                        Arrendatario = @Arrendatario, 
                                        FechaInicioRenta = @FechaInicioRenta 
                                    WHERE Id = @Id";

                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@Id", id);
                comando.Parameters.AddWithValue("@Estado", nuevoEstado);
                // Consistencia: DBNull.Value cuando el arrendatario es nulo.
                comando.Parameters.AddWithValue("@Arrendatario",
                    arrendatario is null ? (object)DBNull.Value : arrendatario);
                comando.Parameters.AddWithValue("@FechaInicioRenta",
                    fechaInicioRenta.HasValue ? (object)fechaInicioRenta.Value : DBNull.Value);

                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        private Departamento MapearDepartamento(SqlDataReader reader)
        {
            return new Departamento
            {
                Id = Convert.ToInt32(reader["Id"]),
                Direccion = reader["Direccion"]?.ToString() ?? string.Empty,
                Colonia = reader["Colonia"]?.ToString() ?? string.Empty,
                Ciudad = reader["Ciudad"]?.ToString() ?? string.Empty,
                Habitaciones = Convert.ToInt32(reader["Habitaciones"]),
                Banios = Convert.ToInt32(reader["Banios"]),
                PrecioRenta = Convert.ToDecimal(reader["PrecioRenta"]),
                Estado = reader["Estado"]?.ToString() ?? string.Empty,
                Arrendatario = reader["Arrendatario"] == DBNull.Value ? null : reader["Arrendatario"]?.ToString(),
                FechaInicioRenta = reader["FechaInicioRenta"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(reader["FechaInicioRenta"])
            };
        }
    }
}
