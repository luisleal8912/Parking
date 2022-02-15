using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Parking.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Vehicle : ControllerBase
    {
        private readonly Business.Rules.Implementation.IVehicle module;
        private readonly Business.Rules.Implementation.IEntry moduleEntry;

        public Vehicle(Business.Rules.Implementation.IVehicle module, Business.Rules.Implementation.IEntry moduleEntry)
        {
            this.module = module;
            this.moduleEntry = moduleEntry;
        }
        
        [HttpPost]
        public string Add(Data.Objects.Vehicle vehicle)
        {
            try
            {
                Data.Objects.Vehicle _vehicle = module.GetByPlate(vehicle.Plate);
                if (_vehicle == null)
                {
                    vehicle.Id = module.Add(vehicle);
                }

                bool result = AddEntry(vehicle.Id);
                if (result)
                {
                    return "Vehicle successfully entered";
                }
                else
                {
                    return "Vehicle is already registered";
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private bool AddEntry(int vehicleId)
        {
            Data.Objects.Entry _entry = new Data.Objects.Entry();
            _entry.VehicleId = vehicleId;
            _entry.EntryTime = DateTime.Now;
            try
            {

                if (moduleEntry.GetDepartureNull(vehicleId) == null)
                {
                    moduleEntry.Add(_entry);
                    return true;
                }

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return false;
        }

        [HttpPost]
        [Route("filter")]
        public List<Data.Objects.ResultFilter> GetFilter(string from, string to)
        {
            try
            {               
                DateTime f = DateTime.Parse(from);
                DateTime t = string.IsNullOrEmpty(to) ? DateTime.Now : DateTime.Parse(to);


                return module.GetFilter(f, t);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
