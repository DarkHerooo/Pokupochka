﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLib
{
    public class Request
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int CounterpartyId { get; set; }
        public Counterparty? Counterparty { get; set; }
        public int StatusId { get; set; }
        public Status? Status { get; set; }
        public List<Product> Products { get; set; } = new();
    }
}
