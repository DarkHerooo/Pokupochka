using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLib.DB.Entity
{
    public class ProductWarehouse
    {
        //[Key, Column(Order = 0)]
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; } = null!;

        //[Key, Column(Order = 1)]
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int Count { get; set; }
    }
}
