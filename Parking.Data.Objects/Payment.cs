using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Data.Objects
{
    public class Payment
    {
        public int Id { get; set; }

        public string Nit { get; set; }

        public int TotalMinutes { get; set; }

        public decimal SubTotal { get; set; }

        public decimal Total { get; set; }

        public decimal Iva { get; set; }
        public decimal Discount { get; set; }
    }
}
