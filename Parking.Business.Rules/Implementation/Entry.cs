using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Business.Rules.Implementation
{
    public class Entry : IEntry
    {
        private readonly Data.Access.Implementation.IEntry module;

        public Entry(Data.Access.Implementation.IEntry module)
        {
            this.module = module;
        }

        public int Add(Data.Objects.Entry entry)
        {
            try
            {
                return module.Add(entry);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public int Update(Data.Objects.Entry entry)
        {
            try
            {
                return module.Update(entry);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public Data.Objects.Entry GetDepartureNull(int vehicleId)
        {
            try
            {
                return module.GetDepartureNull(vehicleId);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
