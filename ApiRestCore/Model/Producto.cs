using Microsoft.Azure.Cosmos.Table;
using System;

namespace ApiRestCore.Model {
    [Serializable]
    public class Producto : TableEntity {

        public Producto() {
            PartitionKey = "Producto";
            RowKey = Guid.NewGuid().ToString();
        }

        public int Id { get; set; }

        public string Nombre { get; set; }
        
        // "0" = Por Revisión, "1" = Revisado.
        public string Estado { get; set; }

        public string HoraDeRevision { get; set; }

    }
}
