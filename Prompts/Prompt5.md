## Prompt 5 — Capa de repositorio con interfaz (Repositories)

```
Crea la interfaz "IDepartamentoRepository.cs" y su implementación
"DepartamentoRepository.cs" dentro de la carpeta Repositories.

La interfaz debe declarar los siguientes métodos:
- void AgregarDepartamento(Departamento departamento)
- List<Departamento> ObtenerDepartamentos()
- Departamento ObtenerPorId(int id)
- List<Departamento> BuscarPorCiudadOColonia(string texto)
- List<Departamento> BuscarPorFiltros(string ciudad, string colonia, string estado, decimal? precioMin, decimal? precioMax)
- List<Departamento> ObtenerDisponibles()
- void ActualizarDepartamento(Departamento departamento)
- void EliminarDepartamento(int id)
- void CambiarEstado(int id, string nuevoEstado, string arrendatario, DateTime? fechaInicioRenta)

La implementación DepartamentoRepository debe:
- Recibir ConexionBD por constructor (no SqlConnection directamente), aplicando el
  Principio de Inversión de Dependencias.
- Usar SIEMPRE parámetros en las consultas SQL (SqlParameter o
  comando.Parameters.AddWithValue) para evitar inyección SQL. Nunca concatenar strings
  directamente en las consultas.
- Manejar correctamente la apertura y cierre de conexiones con bloques "using".
- Mapear manualmente cada SqlDataReader a objetos Departamento.

Toma como referencia este ejemplo de método AgregarDepartamento ya definido en el
documento de requerimientos (mantén ese mismo estilo y nivel de detalle para los demás
métodos):

using Microsoft.Data.SqlClient;
using RentaDepartamentosWeb.Data;
using RentaDepartamentosWeb.Models;

namespace RentaDepartamentosWeb.Repositories
{
    public class DepartamentoRepository
    {
        private readonly ConexionBD conexionBD;

        public DepartamentoRepository(ConexionBD conexionBD)
        {
            this.conexionBD = conexionBD;
        }

        public void AgregarDepartamento(Departamento departamento)
        {
            using (SqlConnection conexion = conexionBD.ObtenerConexion())
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
                comando.Parameters.AddWithValue("@Arrendatario", departamento.Arrendatario ?? "");
                comando.Parameters.AddWithValue("@FechaInicioRenta", departamento.FechaInicioRenta ?? (object)DBNull.Value);

                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }
    }
}

Registra IDepartamentoRepository → DepartamentoRepository en Program.cs.