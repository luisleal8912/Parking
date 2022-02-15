using Parking.Data.Objects;
using System;
using System.Collections.Generic;

namespace Parking.Data.Access.Implementation
{
    public interface IVehicle
    {
        int Add(Objects.Vehicle vehicle);
        Objects.Vehicle GetByPlate(string plate);
        List<ResultFilter> GetFilter(DateTime from, DateTime to);
    }
}