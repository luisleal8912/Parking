using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Business.Rules.Implementation
{
    public class Discount : IDiscount
    {
        private readonly Data.Access.Implementation.IDiscount module;

        public Discount(Data.Access.Implementation.IDiscount module)
        {
            this.module = module;
        }

        public int Add(Data.Objects.Discount discount)
        {
            try
            {
                return module.Add(discount);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public Data.Objects.Discount GetByInvoice(string invoice)
        {
            try
            {
                return module.GetByInvoice(invoice);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
