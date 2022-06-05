using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLib.DB.Entity
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int CountInStock { get; set; }
        public decimal Price { get; set; }
        public decimal CompanyPrice { get; set; }
        public byte[]? Image { get; set; }
        public List<Contract> Contracts { get; set; } = new();
        public List<ProductRequest> ProductRequests { get; set; } = new();

        [NotMapped]
        public string Color
        {
            get
            {
                if (CountInStock >= 500)
                    return "Transparent";
                else if (CountInStock >= 250)
                    return "Yellow";
                else 
                    return "Red";
            }
            set { }
        }

        public void AddOrChange()
        {
            if (Id == 0) DbConnect.Db.Products.Add(this);
            DbConnect.Db.SaveChanges();
        }
    }
}
