using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLib.DB.Entity
{
    public class Request
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public int CounterpartyId { get; set; }
        public Counterparty? Counterparty { get; set; }
        public int StatusId { get; set; }
        public Status? Status { get; set; }
        public List<ProductRequest> ProductRequests { get; set; } = new();

        [NotMapped]
        public string ShortDate
        {
            get => Date.ToShortDateString();
            set { }
        }

        public void AddOrChange()
        {
            if (Id == 0) DbConnect.Db.Requests.Add(this);
            DbConnect.Db.SaveChanges();
        }
    }
}
