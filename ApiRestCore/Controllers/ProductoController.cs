using System;
using System.Collections.Generic;
using ApiRestCore.Data;
using ApiRestCore.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ApiRestCore.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase {

        private ProductoRepository _repository;

        public ProductoController(IConfiguration configuration) {
            _repository = new ProductoRepository(configuration.GetConnectionString("DbConnectionString"));
        }

        /// <summary>
        /// Obtener todos los Productos.
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<Producto>> Get([FromBody] Pagination model) {
            return Ok(_repository.GetProductos(model.ItemsPorPagina, model.ContinuationToken));
        }

        /// <summary>
        /// Obtener producto por RowKey
        /// </summary>
        /// <param name="rowKey">RowKey de la tabla.</param>
        [HttpGet("{rowKey}")]
        public ActionResult<Producto> Get(string rowKey) {
            var producto = _repository.GetProducto(rowKey);
            if(producto == null) {
                return NotFound();
            }
            return Ok(producto);
        }

        /// <summary>
        /// Insertar Producto
        /// </summary>
        [HttpPost]
        public ActionResult<Producto> Post([FromBody] Producto producto) {
            if(!_repository.InsertOrUpdateProducto(producto)) {
                return Conflict();
            }
            return Ok(producto);
        }

        /// <summary>
        /// Actualizar Producto.
        /// </summary>
        [HttpPut]
        public ActionResult<Producto> Put(Producto producto) {
            var item = _repository.GetProducto(producto.RowKey);
            if(item == null) {
                return NotFound();
            }
            item.Id = producto.Id;
            item.Nombre = producto.Nombre;
            item.Estado = producto.Estado;
            item.HoraDeRevision = producto.HoraDeRevision;
            if(!_repository.InsertOrUpdateProducto(item)) {
                return Conflict();
            }
            return Ok(item);
        }

        /// <summary>
        /// Eliminar Producto.
        /// </summary>
        /// <param name="rowKey">RowKey del Producto</param>
        [HttpDelete("{rowKey}")]
        public ActionResult<Producto> Delete(string rowKey) {
            var item = _repository.GetProducto(rowKey);
            if(item == null) {
                return NotFound();
            }
            _repository.DeleteProducto(item);
            return Ok();
        }
    }
}
