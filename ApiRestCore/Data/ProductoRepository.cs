using ApiRestCore.Model;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestCore.Data {
    public class ProductoRepository {

        private readonly CloudTable table;

        public string TableName {
            get {
                return "ProductosOscar";
            }
        }

        public ProductoRepository(string connectionString) {
            CloudStorageAccount account = CloudStorageAccount.Parse(connectionString);
            CloudTableClient tableClient = account.CreateCloudTableClient(new TableClientConfiguration());
            table = tableClient.GetTableReference(TableName);
            //table.DeleteIfExists();
            table.CreateIfNotExists();
        }

        public GetResponse GetProductos(int itemsPorPagina, TableContinuationToken continuationToken) {
            TableQuerySegment<Producto> queryResult = table.ExecuteQuerySegmented(new TableQuery<Producto>().Take(itemsPorPagina), continuationToken);
            return new GetResponse() {
                Productos = queryResult.ToList(),
                Pagination = new Pagination() {
                    ItemsPorPagina = itemsPorPagina,
                    ContinuationToken = queryResult.ContinuationToken,
                }
            };
        }

        public Producto GetProducto(string RowKey) {
            return table.Execute(TableOperation.Retrieve<Producto>("Producto", RowKey))?.Result as Producto;
        }

        public bool InsertOrUpdateProducto(Producto model) {
            return table.Execute(TableOperation.InsertOrMerge(model))?.Result != null ? true : false;
        }

        public bool DeleteProducto(Producto model) {
            return table.Execute(TableOperation.Delete(model))?.Result != null ? true : false;
        }

        public IEnumerable<Producto> GetProductosPorRevision() {
            return table.ExecuteQuery(new TableQuery<Producto>()
                .Where(TableQuery.GenerateFilterCondition("Estado", QueryComparisons.Equal, "0")))
                .ToList();
        }
    }
}
