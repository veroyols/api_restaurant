# TP2-REST-Scholz_Veronica | Parte 2

Los criterios de aceptación son:
1. Debe permitir registrar la mercadería (platos, bebida o postre).
2. Debe permitir registrar las comandas (el pedido del cliente)
3. Debe enlistar las comandas con el detalle de los platos según la fecha que se le ingrese.
4. Debe enlistar la información de la mercadería y permitir filtrar por nombre y/o tipo y ordenar por precio.
5. Debe permitir modificar la información de la mercadería.
6. Debe permitir eliminar la mercadería.
7. Agregar búsqueda de mercadería por nombre y/o tipo y ordenar por precio.
8. Agregar búsqueda de comanda por id.

Consigna:
Realizar una aplicación con la arquitectura API Rest, que exponga los servicios necesarios para cumplir con los criterios de aceptación.
Se deberá reescribir la aplicación de consola realizada en el TP 1 y adaptarla a los nuevos requerimientos.

● La aplicación se debe ajustar al estándar de REST. Tanto los métodos de petición como los de respuesta.

● Los endpoints deben respetar la definición de OpenApi. Pueden ver la definición utilizando la herramienta de Swagger UI

Aclaración:

● Las url, parámetros, body, status y response definidos en la definición de OpenApi deben respetarse.

● Los endpoints con método HTTP [Options] definido en el archivo, deben ser reemplazado por el estudiante según el estándar REST.

● La mercadería no puede ser eliminada si existe una encomienda que dependa de esta.

● No puede existir mercadería con el mismo nombre

● Los filtros son siempre opcionales

● El filtro de orden sólo puede admitir los valores “ASC” y “DESC”

Entrega:
Esta práctica debe ser entregada antes del cierre del sprint actual.
El código debe ser subido al campus virtual o enviado por mail al docente en formato ZIP o RAR con el nombre: “TP2-REST-Apellido_Nombre”

