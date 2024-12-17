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

📋 *Listar mercadería con filtros y ordenamiento*
`GET /api/v1/Mercaderia?tipo={tipo}&nombre={nombre}&orden={orden}`

```
    get (query tipo, nombre, orden ASC):
      responses:
        '200' Success: [MercaderiaGetResponse]
        '400' Bad Request: BadRequest
```

➕ *Registrar una nueva mercadería*
`POST /api/v1/Mercaderia`

```
    options (body MercaderiaRequest):
      responses:
        '201' Success: MercaderiaResponse
        '400' Bad Request: BadRequest
        '409' Conflict: BadRequest
```

🔍 *Buscar mercadería por ID*
`GET /api/v1/Mercaderia/{id}`

```
  /api/v1/Mercaderia/{id}:
    get (id):
      responses:
        '200' Success: MercaderiaResponse
        '400' Bad Request: BadRequest
        '404' Not Found: BadRequest
```

✏️ *Modificar una mercadería*
`PUT /api/v1/Mercaderia/{id}`

```
    options (id)
      requestBody: MercaderiaRequest
      responses:
        '200' Success: MercaderiaResponse
        '400' Bad Request: BadRequest
        '404' Not Found: BadRequest
        '409' Conflict: BadRequest
```

🗑️ *Eliminar una mercadería*
`DELETE /api/v1/Mercaderia/{id}`

```
    delete (id):
      responses:
        '200' Success: MercaderiaResponse
        '400' Bad Request: BadRequest
        '409' Conflict: BadRequest
```

### 📝 Comandas

📋 *Listar comandas por fecha*
`GET /api/v1/Comanda?fecha={fecha}`

```
  get (query fecha):
      responses:
          '200' Success: [ComandaResponse]
          '400' Bad Request: BadRequest
```

➕ *Registrar nueva comanda*
`POST /api/v1/Comanda`

```
  post (body ComandaRequest)
      responses:
        '201' Success: ComandaResponse
        '400' Bad Request: BadRequest
```
🔍 *Buscar comanda por ID*
`OPTIONS /api/v1/Comanda/{id}`

```
  /api/v1/Comanda/{id}:
    OPTIONS (id):
      responses:
        '200' Success: ComandaGetResponse
        '400' Bad Request: BadRequest
        '404' Not Found: BadRequest
```
