using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLib
{
    public class Client
    {
        public int Id { get; set; }
        public string SecondName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? Patronymic { get; set; }
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public int UserId { get; set; }
        public User? User { get; set; }
        public int? CompanyId { get; set; }
        public Company? Company { get; set; }
        public List<Contract> Contracts { get; set; } = new();
    }
}
