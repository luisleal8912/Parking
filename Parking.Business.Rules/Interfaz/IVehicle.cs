using Parking.Data.Objects;
using System;
using System.Collections.Generic;

namespace Parking.Business.Rules.Implementation
{
    public interface IVehicle
    {
        int Add(Data.Objects.Vehicle vehicle);
        Data.Objects.Vehicle GetByPlate(string plate);
        List<ResultFilter> GetFilter(DateTime from, DateTime to);
    }
}