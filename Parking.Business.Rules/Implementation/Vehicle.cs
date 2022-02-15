using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Business.Rules.Implementation
{
    public class Vehicle : IVehicle
    {
        private readonly Data.Access.Implementation.IVehicle module;

        public Vehicle(Data.Access.Implementation.IVehicle module)
        {
            this.module = module;
        }

        public int Add(Data.Objects.Vehicle vehicle)
        {
            try
            {
                return module.Add(vehicle);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public Data.Objects.Vehicle GetByPlate(string plate)
        {
            try
            {
                return module.GetByPlate(plate);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public List<Data.Objects.ResultFilter> GetFilter(DateTime from, DateTime to)
        {
            try
            {
                return module.GetFilter(from, to);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
