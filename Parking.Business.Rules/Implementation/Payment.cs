using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Business.Rules.Implementation
{
    public class Payment : IPayment
    {
        private readonly Data.Access.Implementation.IPayment module;

        public Payment(Data.Access.Implementation.IPayment module)
        {
            this.module = module;
        }

        public int Add(Data.Objects.Payment payment)
        {
            try
            {
                return module.Add(payment);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
