using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLib.DB.Entity
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public double Price { get; set; }
        public byte[]? Image { get; set; }
        public List<Contract> Contracts { get; set; } = new();
        public List<ProductWarehouse> ProductWarehouses { get; set; } = new();
        public List<ProductRequest> ProductRequests { get; set; } = new();

        public void AddOrChange()
        {
            if (Id == 0) DbConnect.Db.Products.Add(this);
            DbConnect.Db.SaveChanges();
        }
    }
}
