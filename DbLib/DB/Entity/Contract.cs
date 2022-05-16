using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLib
{
    public class Contract
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateOver { get; set; }
        public int CountYears { get; set; }
        public int? WorkerId { get; set; }
        public Worker? Worker { get; set; }
        public int? CounterpartyId { get; set; }
        public Counterparty? Counterparty { get; set; }
        public int? StatusId { get; set; }
        public Status? Status { get; set; }
        public List<Product> Products { get; set; } = new();

        
        public void AddOrChange()
        {
            if (Id == 0) DbConnect.Db.Contracts.Add(this);
            DbConnect.Db.SaveChanges();
        }

        public void Delete()
        {
            DbConnect.Db.Contracts.Remove(this);
            DbConnect.Db.SaveChanges();
        }
    }
}
