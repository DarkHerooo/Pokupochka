using DbLib.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLib.DB.Entity
{
    public class Counterparty
    {
        public int Id { get; set; }
        public string SecondName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? Patronymic { get; set; }
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int CompanyId { get; set; }
        public Company? Company { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public List<Contract> Contracts { get; set; } = new();

        public Counterparty()
        {
            Company = new();
            User = new();
        }

        [NotMapped]
        public string FIO
        {
            get
            {
                return AdditionalFields.GetFIO(SecondName, FirstName, Patronymic);
            }
            set { }
        }

        public void AddOrChange()
        {
            if (Id == 0) DbConnect.Db.Counterparties.Add(this);
            DbConnect.Db.SaveChanges();
        }
    }
}
