using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Data.Objects
{
    public class Vehicle
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public string Plate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Comments { get; set; }

    }
}
