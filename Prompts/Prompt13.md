## Prompt 13 — Revisión de Clean Code y principios SOLID

```
Revisa todo el código generado hasta ahora del proyecto RentaDepartamentosWeb
(Models, Data, Repositories, Services, Controllers) y elabora un reporte breve que
confirme, con ejemplos puntuales tomados del propio código del proyecto, cómo se cumple
cada principio SOLID:

- S (Responsabilidad Única): ¿qué hace y qué NO hace cada una de las clases
  ConexionBD, DepartamentoRepository, DepartamentoService y DepartamentoController?
- O (Abierto/Cerrado): si quisiéramos agregar un nuevo tipo de inmueble (por ejemplo,
  "Casa"), ¿qué tendríamos que agregar y qué NO tendríamos que modificar?
- L (Sustitución de Liskov): si existiera una clase base "Inmueble" de la cual heredaran
  "Departamento" y "Casa", ¿qué métodos comunes deberían comportarse de forma consistente
  (MostrarInformacion, Guardar, Actualizar)?
- I (Segregación de Interfaces): ¿conviene dividir IDepartamentoRepository o
  IDepartamentoService en interfaces más pequeñas? Justifica tu respuesta.
- D (Inversión de Dependencias): muestra en qué puntos exactos del código el controlador y
  el servicio dependen de interfaces o abstracciones y no de clases concretas.

Si durante la revisión detectas violaciones a buenas prácticas (nombres poco
descriptivos, SQL embebido en una vista, falta de parámetros en una consulta, código
repetido, falta de separación de responsabilidades), corrígelas directamente en el código
y explica brevemente qué cambiaste y por qué.
```