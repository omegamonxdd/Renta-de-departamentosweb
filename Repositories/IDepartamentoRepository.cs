using System;
using System.Collections.Generic;
using RentaDepartamentosWeb.Models;

namespace RentaDepartamentosWeb.Repositories
{
    /// <summary>
    /// Contrato para el acceso a datos de Departamentos.
    /// Define las operaciones CRUD y búsquedas personalizadas.
    /// </summary>
    public interface IDepartamentoRepository
    {
        void AgregarDepartamento(Departamento departamento);
        List<Departamento> ObtenerDepartamentos();
        Departamento ObtenerPorId(int id);
        List<Departamento> BuscarPorCiudadOColonia(string texto);
        List<Departamento> BuscarPorFiltros(string ciudad, string colonia, string estado, decimal? precioMin, decimal? precioMax);
        List<Departamento> ObtenerDisponibles();
        void ActualizarDepartamento(Departamento departamento);
        void EliminarDepartamento(int id);
        void CambiarEstado(int id, string nuevoEstado, string arrendatario, DateTime? fechaInicioRenta);
    }
}
