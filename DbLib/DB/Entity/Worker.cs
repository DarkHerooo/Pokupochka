using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DbLib
{
    public class Worker
    {
        public int Id { get; set; }
        public string SecondName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? Patronymic { get; set; }

        [MaxLength(11)]
        public string Phone { get; set; } = null!;
        public int? UserId { get; set; }
        public User? User { get; set; }

        // Дополнительные поля
        [NotMapped]
        public string FIO
        {
            get
            {
                string secondName = "";
                for (int i = 0; i < SecondName.Length; i++)
                {
                    secondName += i == 0 ? Char.ToUpper(SecondName[i]) : Char.ToLower(SecondName[i]);
                }
                secondName = secondName.Trim();

                string firstName = "";
                for (int i = 0; i < FirstName.Length; i++)
                {
                    firstName += i == 0 ? Char.ToUpper(FirstName[i]) : Char.ToLower(FirstName[i]);
                }
                firstName = firstName.Trim();

                string patronymic = "";
                for (int i = 0; i < Patronymic?.Length; i++)
                {
                    patronymic += i == 0 ? Char.ToUpper(Patronymic[i]) : Char.ToLower(Patronymic[i]);
                }
                patronymic = patronymic.Trim();

                return secondName + " " + firstName + " " + patronymic;
            }
            set { }
        }

        [NotMapped]
        public string CorrectPhone
        {
            get
            {
                string phone = "+";
                phone += Phone[0] + " ";
                phone += "(" + Phone.Substring(1, 3) + ") ";
                phone += Phone.Substring(4, 3) + "-";
                phone += Phone.Substring(7, 2) + "-";
                phone += Phone.Substring(9, 2);
                return phone;
            }
            set { }
        }

        // Методы
        public void SetData(string secondName, string firstName, string patronymic, string phone)
        {
            SecondName = secondName;
            FirstName = firstName;
            Patronymic = patronymic;
            Phone = phone;
        }

        public void AddOrChange()
        {
            if (Id == 0) DbConnect.Db.Workers.Add(this);
            DbConnect.Db.SaveChanges();
        }

        public void Delete()
        {
            DbConnect.Db.Workers.Remove(this);
            DbConnect.Db.SaveChanges();
        }
    }
}
