# Desafio Tecnico de EncodeLabs

## Ejecucion de la API:

### Requisitos:
- .NET SDK 8.0
- Visual Studio (Con soporte para ASP .NET) o VSCode (Con extensiones C# y .NET)
- Certificado DEV: dotnet dev-certs https --trust

### Ejecución en VS:
1. Abrir el .sln
2. Seleccionar el perfil https
3. Ejecutar con F5 (Debug) o ctrl + F5

### Ejecución en VS Code
1. Abrir carpeta raíz del proyecto
2. Abrir la terminal y ejecutar con: dotnet run
	
### Ejecución con .NET CLI:
1. Abrir una terminal en la carpeta raíz
2. Ejecutar con: dotnet run

La configuración del puerto se encuentra dentro de AppSettings.json.

La coleccion de Postman se encuentra dentro de la carpeta "Postman"
 
- - -
 
## Endpoints:

### POST /api/Productos/add 
```
Añade un producto a la base de datos

Body: Tipo JSON
{
   "name": "NombreProducto",
   "description": "DescripcionProducto",
   "price": PrecioProducto,
   "quantity": CantidadProducto
}

Returns: Codigo 201 - El producto subido en la base de datos formato JSON
```
----

### GET  /api/Productos/enlist
```
Crea una lista de productos que se encuentran en el momento dentro de la base de datos

Returns: Codigo 200 - Una lista de productos en formato JSON
```
---

### GET  /api/Productos/find/{id}
```
Busca un producto dentro de la base de datos usando un ID

Returns: Codigo 200 - El producto como figura dentro de la base de datos formato JSON
```
---

### PUT  /api/Productos/update/{id}
```
Actualiza un producto que se encuentra en la base de datos usando un ID

Params: id - ID del producto a actualizar

Body: Tipo JSON
{
    "name": "NombreActualizadoProducto",
    "description": "DescripcionActualizadoProducto",
    "price": PrecioActualizadoProducto,
    "quantity": CantidadActualizadoProducto
}

Returns: Codigo 204
```
---

### DEL  /api/Productos/remove/{id}
```
Remueve un producto de la base de datos

Returns: Codigo 204
```
