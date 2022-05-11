using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLib
{
    public class Company
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public List<Client> Clients { get; set; } = new();
    }
}
