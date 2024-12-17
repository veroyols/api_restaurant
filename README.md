# 🍽️ API REST - Restaurant | Parte 2

Una API RESTful para la gestión de mercadería y comandas en un restaurante.

## 📜 Descripción

Esta API permite administrar la mercadería (platos, bebidas y postres) y gestionar las comandas de los clientes. Implementa operaciones de **alta**, **baja**, **modificación**, **búsqueda** y **listado** cumpliendo con el estándar **REST** y respetando la definición de **OpenAPI**.

## 🚀 Funcionalidades

1. ✅ **Registrar mercadería** (platos, bebidas o postres).
2. ✅ **Registrar comandas** (pedidos de los clientes).
3. ✅ **Listar comandas** con detalle de platos por fecha ingresada.
4. ✅ **Listar mercadería** con filtros por nombre y/o tipo, y ordenación por precio (ASC/DESC).
5. ✅ **Modificar la información de la mercadería**.
6. ✅ **Eliminar mercadería** (solo si no está asociada a una comanda).
7. ✅ **Buscar mercadería por ID**.
8. ✅ **Buscar comanda por ID**.

## 🛠️ Tecnologías Utilizadas

- 🌐 **.NET Core / ASP.NET Core**: Framework principal para el desarrollo de la API.
- 📦 **Swagger UI**: Documentación y prueba de endpoints.
- 🗄️ **PostgreSQL / SQL Server**: Base de datos para almacenar mercadería y comandas.

## 🔗 Endpoints
### 🍽️ Mercaderia
```
  /api/v1/Mercaderia: 
    get (query tipo, nombre, orden ASC):
      responses:
        '200' Success: [MercaderiaGetResponse]
        '400' Bad Request: BadRequest
    options (body MercaderiaRequest):
      responses:
        '201' Success: MercaderiaResponse
        '400' Bad Request: BadRequest
        '409' Conflict: BadRequest
  /api/v1/Mercaderia/{id}:
    get (id):
      responses:
        '200' Success: MercaderiaResponse
        '400' Bad Request: BadRequest
        '404' Not Found: BadRequest
    options (id)
      requestBody: MercaderiaRequest
      responses:
        '200' Success: MercaderiaResponse
        '400' Bad Request: BadRequest
        '404' Not Found: BadRequest
        '409' Conflict: BadRequest
    delete (id):
      responses:
        '200' Success: MercaderiaResponse
        '400' Bad Request: BadRequest
        '409' Conflict: BadRequest
```

### 📝 Comandas
```
/api/v1/Comanda:
    get (query fecha):
      responses:
        '200' Success: [ComandaResponse]
        '400' Bad Request: BadRequest
    post (body ComandaRequest)
      responses:
        '201' Success: ComandaResponse
        '400' Bad Request: BadRequest
  /api/v1/Comanda/{id}:
    options (id):
      responses:
        '200' Success: ComandaGetResponse
        '400' Bad Request: BadRequest
        '404' Not Found: BadRequest
```
