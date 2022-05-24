using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLib.DB.Entity
{
    public class Status
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Color { get; set; }

        public List<Contract> Contracts { get; set; } = new();
        public List<Request> Requests { get; set; } = new();
    }
}
