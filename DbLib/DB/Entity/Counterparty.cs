using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLib
{
    public class Counterparty
    {
        public int Id { get; set; }
        public string Company { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string SecondName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? Patronymic { get; set; }
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int UserId { get; set; }
        public User? User { get; set; }
        public List<Contract> Contracts { get; set; } = new();

        public Counterparty()
        {
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

        public void SetData(string company, string address, string secondName, string firstName, string patronymic, string phone, string email)
        {
            Company = company;
            Address = address;
            SecondName = secondName;
            FirstName = firstName;
            Patronymic = patronymic;
            Phone = phone;
            Email = email;
        }

        public void AddOrChange()
        {
            if (Id == 0) DbConnect.Db.Counterparties.Add(this);
            DbConnect.Db.SaveChanges();
        }
    }
}
