using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLib.DB.Entity
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public byte[]? Image { get; set; }
        public string INN { get; set; } = null!;
        public string KPP { get; set; } = null!;
        public string OKPO { get; set; } = null!;
        public Counterparty? Counterparty { get; set; }

        public void AddOrChange()
        {
            if (Id == 0) DbConnect.Db.Companies.Add(this);
            DbConnect.Db.SaveChanges();
        }
    }
}
