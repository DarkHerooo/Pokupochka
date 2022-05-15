using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLib
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public double Price { get; set; }
        public byte[]? Image { get; set; }
        public List<Contract> Contracts { get; set; } = new();
    }
}
