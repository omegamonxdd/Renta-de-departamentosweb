using System;
using System.Collections.Generic;
using RentaDepartamentosWeb.Models;

namespace RentaDepartamentosWeb.Services
{
    /// <summary>
    /// Contrato para la lógica de negocio relacionada con Departamentos.
    /// Define las reglas de validación y flujos de negocio.
    /// </summary>
    public interface IDepartamentoService
    {
        void RegistrarDepartamento(Departamento departamento);
        List<Departamento> ObtenerDepartamentos();
        Departamento ObtenerPorId(int id);
        List<Departamento> ObtenerDepartamentosDisponibles();
        List<Departamento> BuscarPorCiudadOColonia(string texto);
        List<Departamento> BuscarPorFiltros(string ciudad, string colonia, string estado, decimal? precioMin, decimal? precioMax);
        void ActualizarDepartamento(Departamento departamento);
        void EliminarDepartamento(int id);
        void CambiarEstadoDepartamento(int id, string nuevoEstado, string arrendatario, DateTime? fechaInicioRenta);
    }
}
