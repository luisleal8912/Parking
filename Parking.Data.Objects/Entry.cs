using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Data.Objects
{
    public class Entry
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public DateTime EntryTime {get; set; }
        public DateTime? ExitTime { get; set; }        
    }
}
