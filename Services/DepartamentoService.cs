using System;
using System.Collections.Generic;
using RentaDepartamentosWeb.Models;
using RentaDepartamentosWeb.Repositories;

namespace RentaDepartamentosWeb.Services
{
    /// <summary>
    /// Implementación de la lógica de negocio de Departamentos.
    /// Recibe IDepartamentoRepository por inyección de dependencias.
    /// Valida las reglas de negocio antes de realizar operaciones de persistencia.
    /// </summary>
    public class DepartamentoService : IDepartamentoService
    {
        private readonly IDepartamentoRepository _departamentoRepository;

        public DepartamentoService(IDepartamentoRepository departamentoRepository)
        {
            _departamentoRepository = departamentoRepository;
        }

        public void RegistrarDepartamento(Departamento departamento)
        {
            ValidarYNormalizarDepartamento(departamento);
            _departamentoRepository.AgregarDepartamento(departamento);
        }

        public List<Departamento> ObtenerDepartamentos()
        {
            return _departamentoRepository.ObtenerDepartamentos();
        }

        public Departamento ObtenerPorId(int id)
        {
            return _departamentoRepository.ObtenerPorId(id);
        }

        public List<Departamento> ObtenerDepartamentosDisponibles()
        {
            return _departamentoRepository.ObtenerDisponibles();
        }

        public List<Departamento> BuscarPorCiudadOColonia(string texto)
        {
            return _departamentoRepository.BuscarPorCiudadOColonia(texto);
        }

        public List<Departamento> BuscarPorFiltros(string ciudad, string colonia, string estado, decimal? precioMin, decimal? precioMax)
        {
            return _departamentoRepository.BuscarPorFiltros(ciudad, colonia, estado, precioMin, precioMax);
        }

        public void ActualizarDepartamento(Departamento departamento)
        {
            ValidarYNormalizarDepartamento(departamento);

            var deptoExistente = _departamentoRepository.ObtenerPorId(departamento.Id);
            if (deptoExistente == null)
            {
                throw new ArgumentException($"No se encontró ningún departamento con el ID {departamento.Id}.", nameof(departamento.Id));
            }

            _departamentoRepository.ActualizarDepartamento(departamento);
        }

        public void EliminarDepartamento(int id)
        {
            var deptoExistente = _departamentoRepository.ObtenerPorId(id);
            if (deptoExistente == null)
            {
                throw new ArgumentException($"No se encontró ningún departamento con el ID {id}.", nameof(id));
            }

            _departamentoRepository.EliminarDepartamento(id);
        }

        public void CambiarEstadoDepartamento(int id, string nuevoEstado, string arrendatario, DateTime? fechaInicioRenta)
        {
            // Regla 4: El estado debe ser exactamente uno de: "Disponible", "Rentado", "Mantenimiento".
            if (nuevoEstado != "Disponible" && nuevoEstado != "Rentado" && nuevoEstado != "Mantenimiento")
            {
                throw new ArgumentException("El estado debe ser exactamente uno de: 'Disponible', 'Rentado', 'Mantenimiento'.", nameof(nuevoEstado));
            }

            // Regla 5: Si el estado es "Rentado", debe capturarse obligatoriamente el nombre del arrendatario.
            if (nuevoEstado == "Rentado")
            {
                if (string.IsNullOrWhiteSpace(arrendatario))
                {
                    throw new ArgumentException("Si el estado es 'Rentado', el nombre del arrendatario es obligatorio.", nameof(arrendatario));
                }
            }
            // Regla 6: Si el estado es "Disponible" o "Mantenimiento", el arrendatario y la fecha de inicio de renta deben quedar en null (limpiar automáticamente).
            else
            {
                arrendatario = null!;
                fechaInicioRenta = null;
            }

            var deptoExistente = _departamentoRepository.ObtenerPorId(id);
            if (deptoExistente == null)
            {
                throw new ArgumentException($"No se encontró ningún departamento con el ID {id}.", nameof(id));
            }

            _departamentoRepository.CambiarEstado(id, nuevoEstado, arrendatario, fechaInicioRenta);
        }

        /// <summary>
        /// Valida las reglas de negocio de un departamento y normaliza los campos según su estado.
        /// </summary>
        private void ValidarYNormalizarDepartamento(Departamento departamento)
        {
            if (departamento == null)
            {
                throw new ArgumentNullException(nameof(departamento), "El departamento no puede ser nulo.");
            }

            // Regla 1: La dirección no debe estar vacía.
            if (string.IsNullOrWhiteSpace(departamento.Direccion))
            {
                throw new ArgumentException("La dirección no debe estar vacía.", nameof(departamento.Direccion));
            }

            // Regla 2: El precio de renta debe ser mayor a cero.
            if (departamento.PrecioRenta <= 0)
            {
                throw new ArgumentException("El precio de renta debe ser mayor a cero.", nameof(departamento.PrecioRenta));
            }

            // Regla 3: El número de habitaciones debe ser mayor a cero.
            if (departamento.Habitaciones <= 0)
            {
                throw new ArgumentException("El número de habitaciones debe ser mayor a cero.", nameof(departamento.Habitaciones));
            }

            // Regla 4: El estado debe ser exactamente uno de: "Disponible", "Rentado", "Mantenimiento".
            if (departamento.Estado != "Disponible" && departamento.Estado != "Rentado" && departamento.Estado != "Mantenimiento")
            {
                throw new ArgumentException("El estado debe ser exactamente uno de: 'Disponible', 'Rentado', 'Mantenimiento'.", nameof(departamento.Estado));
            }

            // Regla 5: Si el estado es "Rentado", debe capturarse obligatoriamente el nombre del arrendatario.
            if (departamento.Estado == "Rentado")
            {
                if (string.IsNullOrWhiteSpace(departamento.Arrendatario))
                {
                    throw new ArgumentException("Si el estado es 'Rentado', el nombre del arrendatario es obligatorio.", nameof(departamento.Arrendatario));
                }
            }
            // Regla 6: Si el estado es "Disponible" o "Mantenimiento", el arrendatario y la fecha de inicio de renta deben quedar en null (limpiar automáticamente).
            else
            {
                departamento.Arrendatario = null;
                departamento.FechaInicioRenta = null;
            }
        }
    }
}
