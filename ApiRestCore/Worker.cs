using ApiRestCore.Data;
using ApiRestCore.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiRestCore {
    public class Worker : BackgroundService {

        private ProductoRepository _repository;

        public Worker(IConfiguration configuration) {
            _repository = new ProductoRepository(configuration.GetConnectionString("DbConnectionString"));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
            while(!stoppingToken.IsCancellationRequested) {
                IEnumerable<Producto> productos = _repository.GetProductosPorRevision();
                foreach(var producto in productos) {                    
                    producto.HoraDeRevision = DateTime.Now.ToString("HH:mm");
                    _repository.InsertOrUpdateProducto(producto);
                }
                await Task.Delay(10000);
            }
        }
    }
}
