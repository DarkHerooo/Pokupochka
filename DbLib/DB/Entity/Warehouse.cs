using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLib.DB.Entity
{
    public class Warehouse
    {
        public int Id { get; set; }
        public string? Note { get; set; }
        public int Size { get; set; }
        public List<Product> Products { get; set; } = new();
    }
}
