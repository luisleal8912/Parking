using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Data.Access.Implementation
{
    public class Payment : IPayment
    {
        private Connection Conection;
        private Parameter[] Parameter;

        public Payment()
        {
            Conection = new Connection();
        }

        public int Add(Objects.Payment payment)
        {
            List<Parameter> list = new List<Parameter>();
            StringBuilder sentence = new StringBuilder();
            sentence.Append(" INSERT INTO PAYMENT(ID, NIT, TOTAL_MINUTES, TOTAL, SUB_TOTAL, IVA)");
            sentence.Append(" VALUES(@ID, @NIT, @TOTAL_MINUTES, @TOTAL, @SUB_TOTAL, @IVA)");

            list.Add(new Parameter("@ID", payment.Id));
            list.Add(new Parameter("@NIT", payment.Nit));
            list.Add(new Parameter("@TOTAL_MINUTES", payment.TotalMinutes));
            list.Add(new Parameter("@TOTAL", payment.Total));
            list.Add(new Parameter("@SUB_TOTAL", payment.SubTotal));
            list.Add(new Parameter("@IVA", payment.Iva));

            Parameter = list.ToArray();
            try
            {
                Conection.Insert(sentence.ToString(), Parameter);
                return payment.Id;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
