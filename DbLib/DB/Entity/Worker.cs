using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DbLib.DB;

namespace DbLib.DB.Entity
{
    public class Worker
    {
        public int Id { get; set; }
        public string SecondName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? Patronymic { get; set; }
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int UserId { get; set; }
        public User? User { get; set; }

        public Worker()
        {
            User = new();
        }

        // Дополнительные поля
        [NotMapped]
        public string FIO
        {
            get
            {
                return AdditionalFields.GetFIO(SecondName, FirstName, Patronymic);
            }
            set { }
        }

        // Методы
        public void SetData(string secondName, string firstName, string patronymic, string phone, string email)
        {
            SecondName = secondName;
            FirstName = firstName;
            Patronymic = patronymic;
            Phone = phone;
            Email = email;
        }

        public void AddOrChange()
        {
            if (Id == 0) DbConnect.Db.Workers.Add(this);
            DbConnect.Db.SaveChanges();
        }
    }
}
