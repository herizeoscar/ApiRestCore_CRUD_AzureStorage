using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestCore.Model {

    [Serializable]
    public class Pagination {

        public int ItemsPorPagina { get; set; }
        public TableContinuationToken ContinuationToken { get; set; }

    }
}
