Desafio Tecnico de EncodeLabs

Para ejecutar la app abrir con Visual Studio/VSCode o usar el comando dotnet run con ".NET CLI"
La configuración del puerto se encuentra dentro de AppSettings.json
La coleccion de Postman se encuentra dentro de la carpeta "Postman"

Endpoints:

POST /api/Productos/add 

Añade un producto a la base de datos

Body: Tipo JSON
{
    "name": "NombreProducto",
    "description": "DescripcionProducto",
    "price": PrecioProducto,
    "quantity": CantidadProducto
}

Returns: Codigo 201 - El producto subido en la base de datos formato JSON

GET  /api/Productos/enlist

Crea una lista de productos que se encuentran en el momento dentro de la base de datos

Returns: Codigo 200 - Una lista de productos en formato JSON

GET  /api/Productos/find/{id}

Busca un producto dentro de la base de datos usando un ID

Returns: Codigo 200 - El producto como figura dentro de la base de datos formato JSON

PUT  /api/Productos/update/{id}

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

DEL  /api/Productos/remove/{id}

Remueve un producto de la base de datos

Returns: Codigo 204
