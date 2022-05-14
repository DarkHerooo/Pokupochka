using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLib
{
    public class Contract
    {
        public int Id { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateOver { get; set; }
        public int? WorkerId { get; set; }
        public Worker? Worker { get; set; }
        public int? ClientId { get; set; }
        public Counterparty? Client { get; set; }
    }
}
