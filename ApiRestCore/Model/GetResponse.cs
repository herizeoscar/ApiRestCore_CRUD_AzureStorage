using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestCore.Model {

    [Serializable]
    public class GetResponse {

        public IEnumerable<Producto> Productos { get; set; }
        public Pagination Pagination { get; set; }

    }
}
