## Prompt 7 — Controlador web (Controllers)

```
Crea el controlador "DepartamentoController.cs" dentro de la carpeta Controllers. Este
controlador debe recibir las peticiones web y delegar TODA la lógica al servicio
IDepartamentoService inyectado por constructor (el controlador no debe contener reglas de
negocio ni SQL).

Acciones requeridas:
- Index() → lista todos los departamentos (vista Index.cshtml).
- Create() [GET] → muestra el formulario de registro.
- Create(Departamento departamento) [POST] → valida ModelState, llama al servicio,
  captura excepciones de validación de negocio y las muestra como errores en la vista.
- Edit(int id) [GET] → obtiene el departamento por id y muestra el formulario de edición.
- Edit(int id, Departamento departamento) [POST] → actualiza usando el servicio.
- Details(int id) → muestra el detalle de un departamento.
- Delete(int id) [GET] → muestra confirmación de eliminación.
- Delete(int id) [POST, ActionName("Delete")] → elimina el registro.
- Buscar(string ciudad, string colonia, string estado, decimal? precioMin, decimal? precioMax)
  → retorna la vista de resultados de búsqueda.
- Disponibles() → retorna solo los departamentos con estado "Disponible".
- CambiarEstado(int id, string nuevoEstado, string arrendatario, DateTime? fechaInicioRenta)
  [POST] → cambia el estado de un departamento y redirige a Index.

Maneja los errores de validación de forma amigable (TempData o ModelState.AddModelError)
para que el usuario vea el mensaje en la vista en lugar de un error genérico del servidor.
```
