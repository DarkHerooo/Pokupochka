using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLib.DB.Entity
{
    public class ProductRequest
    {
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int RequestId { get; set; }
        public Request Request { get; set; } = null!;
        public int Count { get; set; }

        [NotMapped]
        public decimal Price
        {
            get => Math.Round(Product.Price * Count, 2);
            set { }
        }
    }
}
