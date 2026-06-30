## Prompt 11 — Vista de búsqueda y de departamentos disponibles

```
Crea dos vistas adicionales dentro de Views/Departamentos:

1. Buscar.cshtml — un formulario con los siguientes filtros, todos opcionales y
   combinables entre sí:
   - Ciudad (texto)
   - Colonia (texto)
   - Estado (select: Disponible/Rentado/Mantenimiento/Todos)
   - Rango de precio (precio mínimo y precio máximo)
   Debajo del formulario, muestra una tabla con los resultados que cumplan los filtros
   aplicados (puedes reutilizar el partial o estructura de tabla de Index.cshtml).

2. Disponibles.cshtml — muestra ÚNICAMENTE los departamentos cuyo Estado sea
   "Disponible", en formato de tabla o tarjetas, pensado para un usuario que está buscando
   rentar (vista pública, sin opciones de editar/eliminar).

Conecta ambas vistas con las acciones Buscar() y Disponibles() del
DepartamentoController creadas en el Prompt 7.
```
