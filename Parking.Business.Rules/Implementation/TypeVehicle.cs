using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Business.Rules.Implementation
{
    public class TypeVehicle : ITypeVehicle
    {
        private readonly Data.Access.Implementation.ITypeVehicle module;

        public TypeVehicle(Data.Access.Implementation.ITypeVehicle module)
        {
            this.module = module;
        }

        public Data.Objects.TypeVehicle Get(int typeId)
        {
            try
            {
                return module.Get(typeId);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
