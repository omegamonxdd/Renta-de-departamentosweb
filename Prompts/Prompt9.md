## Prompt 9 — Vistas Razor: Registro y lista de departamentos

```
Crea las siguientes vistas Razor dentro de Views/Departamentos, basadas en el modelo
Departamento:

1. Create.cshtml — formulario de registro con los campos:
   Dirección, Colonia, Ciudad, Número de habitaciones, Número de baños, Precio de renta,
   Estado (como <select> con las opciones Disponible/Rentado/Mantenimiento), Nombre del
   arrendatario, Fecha de inicio de renta.
   Debe mostrar los mensajes de validación (asp-validation-summary y
   asp-validation-for) tanto de las Data Annotations del modelo como de las excepciones de
   negocio que pueda devolver el controlador.

2. Index.cshtml — tabla con todos los departamentos registrados, mostrando las columnas:
   ID, Dirección, Colonia, Ciudad, Habitaciones, Baños, Precio de renta, Estado,
   Arrendatario, Fecha de inicio de renta, y una columna de Opciones con enlaces/botones
   para: Ver detalles, Editar, Eliminar.

Usa Bootstrap para que la tabla sea responsiva y el formulario tenga un diseño claro,
manteniendo consistencia visual con la página principal del Prompt 8.
```
