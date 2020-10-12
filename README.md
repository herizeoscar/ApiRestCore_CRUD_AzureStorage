# **Documentación de ApiRestCore: Servicio CRUD a Azure Storage**
### **API Rest Core con servicio para realizar CRUD de Productos a una tabla almacenada en Azure Storage.**

---

## ***`- Campos en la Base de Datos`***

- PartitionKey: siempre igual a "Producto".
- RowKey: es autogenerada por la API.
- Estado: Hace referencia a si el producto se encuentra por revisión o revisado. `"0"` corresponde a `Por Revisión`, `"1"` corresponde a `Revisado`.
- Hora de Revisión: Hora en formato de 24 horas.


## ***`- GET`***
*En Body:*

Para ver primeros registros se indica la cantidad de registros por pagina y en el `ContinuationToken` se establece `null`:

*Ejemplo:*

```json
{
    "ItemsPorPagina": 5,
    "ContinuationToken": null    
}
```

*Ejemplo respuesta:*
```json
{
    "productos": [
        {
            "id": 7,
            "nombre": "Producto 7",
            "estado": "1",
            "horaDeRevision": "18:00",
            "partitionKey": "Producto",
            "rowKey": "064d71d1-5556-4404-8e6c-4b7d48543af1",
            "timestamp": "2020-10-11T16:15:02.7315609-04:00",
            "eTag": "W/\"datetime'2020-10-11T20%3A15%3A02.7315609Z'\""
        },
        {
            "id": 8,
            "nombre": "Producto 8",
            "estado": "1",
            "horaDeRevision": "18:00",
            "partitionKey": "Producto",
            "rowKey": "0c7195a6-f3c5-4aba-b20d-d06915de7be2",
            "timestamp": "2020-10-11T16:15:02.867656-04:00",
            "eTag": "W/\"datetime'2020-10-11T20%3A15%3A02.867656Z'\""
        },
        {
            "id": 6,
            "nombre": "Producto 6",
            "estado": "1",
            "horaDeRevision": "18:00",
            "partitionKey": "Producto",
            "rowKey": "2676f611-c2fc-4c09-bb25-ed8c8c91d2e6",
            "timestamp": "2020-10-11T16:15:02.5984677-04:00",
            "eTag": "W/\"datetime'2020-10-11T20%3A15%3A02.5984677Z'\""
        },
        {
            "id": 11,
            "nombre": "Producto 11",
            "estado": "1",
            "horaDeRevision": "18:00",
            "partitionKey": "Producto",
            "rowKey": "5cfbcd81-7126-4d20-a0ac-49b0f0b2f6ae",
            "timestamp": "2020-10-11T16:15:03.2919531-04:00",
            "eTag": "W/\"datetime'2020-10-11T20%3A15%3A03.2919531Z'\""
        },
        {
            "id": 10,
            "nombre": "Producto 10",
            "estado": "1",
            "horaDeRevision": "18:00",
            "partitionKey": "Producto",
            "rowKey": "5ff553f2-c73e-4dd0-ba24-4f6fe1a91a0d",
            "timestamp": "2020-10-11T16:15:03.1638631-04:00",
            "eTag": "W/\"datetime'2020-10-11T20%3A15%3A03.1638631Z'\""
        }
    ],
    "pagination": {
        "itemsPorPagina": 5,
        "continuationToken": {
            "nextPartitionKey": "1!12!UHJvZHVjdG8-",
            "nextRowKey": "1!48!OGQwNjE1YTUtNjI4ZC00OGQ5LTk2NjgtZjIxYzc3NzEwMjQ2",
            "nextTableName": null,
            "targetLocation": 0
        }
    }
}
```

En la respuesta podemos ver el valor correspondiente a ContinuationToken para ver la siguiente pagina de registros con la cantidad de registros por pagina utilizada. 

Para avanzar a la siguiente pagina solo hace falta realizar la petición get indicando ahora el ContinuationToken

*Ejemplo:*
```json
{
    "ItemsPorPagina": 5,
    "continuationToken": {
            "nextPartitionKey": "1!12!UHJvZHVjdG8-",
            "nextRowKey": "1!48!OGQwNjE1YTUtNjI4ZC00OGQ5LTk2NjgtZjIxYzc3NzEwMjQ2",
            "nextTableName": null,
            "targetLocation": 0
        }   
}
```

## ***`- GET Por RowKey`***

Para obtener un solo produto es necesario hacer uso del campo RowKey. Para ello es necesario realizar la petición get con el valor de este campo de la siguiente manera:

*Ejemplo:*

```
RowKey: 064d71d1-5556-4404-8e6c-4b7d48543af1

Petición GET a la siguiente URL:

http://localhost:8080/api/producto/064d71d1-5556-4404-8e6c-4b7d48543af1

```

*Ejemplo Respuesta:*
```Json
{
    "id": 7,
    "nombre": "Producto 7",
    "estado": "1",
    "horaDeRevision": "18:00",
    "partitionKey": "Producto",
    "rowKey": "064d71d1-5556-4404-8e6c-4b7d48543af1",
    "timestamp": "2020-10-11T16:15:02.7315609-04:00",
    "eTag": "W/\"datetime'2020-10-11T20%3A15%3A02.7315609Z'\""
}
```

## ***`- POST`***

Para insertar un producto es necesario enviar junto a la petición POST un json con los valores de los campos, no es necesario colocar los campos `PartitionKey` y `RowKey` ya que la API los asigna internamente. 

*Ejemplo*
```Json
{
    "id": 20,
    "nombre": "Producto 20",
    "estado": "1",
    "horaDeRevision": "18:00"
}
```

## ***`- PUT`***

Para actualizar un producto es necesario enviar junto a la petición PUT un json con los valores de los campos **`indicando el valor correspondiende al campo RowKey.`**

*Ejemplo*
```Json
{
    "rowKey": "13236081-2b05-4938-85c4-77098ff0f5a3",
    "id": 20,
    "nombre": "Producto 20 Actualizado",
    "estado": "1",
    "horaDeRevision": "18:00"
}
``` 

## ***`- DELETE`***

Para eliminar un produto es necesario hacer uso del campo RowKey. Para ello es necesario realizar la petición DELETE con el valor de este campo de la siguiente manera:

*Ejemplo:*

```
RowKey: 13236081-2b05-4938-85c4-77098ff0f5a3

Petición DELETE a la siguiente URL:

http://localhost:8080/api/producto/13236081-2b05-4938-85c4-77098ff0f5a3

```


## ***`- Worker Service`***

La API constantemente (cada minuto), estará actualizando el campo `HoraDeRevision` de los registros que se encuentren con estado `Por Revisión (Estado = 0)`, estableciendo la Hora de Revisión a la Hora Actual. 