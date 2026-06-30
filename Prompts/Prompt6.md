## Prompt 6 — Capa de servicios con reglas de negocio (Services)

```
Crea la interfaz "IDepartamentoService.cs" y su implementación "DepartamentoService.cs"
dentro de la carpeta Services. Esta capa es responsable de validar las reglas de negocio
ANTES de guardar, actualizar o eliminar información (Principio de Responsabilidad Única).

DepartamentoService debe recibir IDepartamentoRepository por constructor (no la clase
concreta, aplicando Inversión de Dependencias e Inyección de Dependencias).

Reglas de negocio que debe validar, lanzando excepciones de tipo ArgumentException (o una
excepción personalizada) con mensajes claros cuando no se cumplan:
1. La dirección no debe estar vacía.
2. El precio de renta debe ser mayor a cero.
3. El número de habitaciones debe ser mayor a cero.
4. El estado debe ser exactamente uno de: "Disponible", "Rentado", "Mantenimiento".
5. Si el estado es "Rentado", debe capturarse obligatoriamente el nombre del arrendatario.
6. Si el estado es "Disponible" o "Mantenimiento", el arrendatario y la fecha de inicio de
   renta deben quedar en null (limpiar esos campos automáticamente).

Métodos que debe exponer la interfaz (deben reflejar los mismos casos de uso que el
repositorio, pero pasando primero por las validaciones):
- RegistrarDepartamento(Departamento departamento)
- ObtenerDepartamentos()
- ObtenerPorId(int id)
- ObtenerDepartamentosDisponibles()
- BuscarPorCiudadOColonia(string texto)
- BuscarPorFiltros(string ciudad, string colonia, string estado, decimal? precioMin, decimal? precioMax)
- ActualizarDepartamento(Departamento departamento)
- EliminarDepartamento(int id)
- CambiarEstadoDepartamento(int id, string nuevoEstado, string arrendatario, DateTime? fechaInicioRenta)

Usa nombres de métodos descriptivos y significativos (evita nombres como x(), dato(),
funcion1()). Registra IDepartamentoService → DepartamentoService en Program.cs.
```
