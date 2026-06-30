## Prompt 12 — Funcionalidad de cambio de estado

```
Implementa el flujo completo para "Cambiar estado del departamento" como una
funcionalidad explícita y visible del sistema (no solo a través del formulario de
edición general):

- En Index.cshtml, agrega junto a cada fila un botón o enlace "Cambiar estado".
- Crea una vista o un modal (puedes usar un pequeño modal de Bootstrap) llamado
  CambiarEstado.cshtml que permita seleccionar el nuevo estado y, si el nuevo estado es
  "Rentado", capture obligatoriamente el nombre del arrendatario y la fecha de inicio de
  renta (estos campos deben ocultarse/deshabilitarse si el estado elegido es "Disponible"
  o "Mantenimiento").
- Conecta esta vista con la acción CambiarEstado() del controlador y con el método
  CambiarEstadoDepartamento() del servicio, asegurando que las reglas de negocio del
  Prompt 6 se respeten (por ejemplo, no permitir "Rentado" sin arrendatario).

Verifica que, al guardar, la tabla de Index.cshtml refleje el nuevo estado sin necesidad
de recargar manualmente otros datos.
```
