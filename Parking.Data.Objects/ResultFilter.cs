using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Data.Objects
{
    public class ResultFilter : Vehicle
    {
        public string Name { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
        public int Minutes { get; set; }
        public decimal? Total { get; set; }
        public string InvoiceNumber { get; set; }

    }
}
