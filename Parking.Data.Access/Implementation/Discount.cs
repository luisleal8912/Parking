using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Data.Access.Implementation
{
    public class Discount : IDiscount
    {
        private Connection Conection;
        private Parameter[] Parameter;

        public Discount()
        {
            Conection = new Connection();
        }

        public int Add(Objects.Discount discount)
        {
            List<Parameter> list = new List<Parameter>();
            StringBuilder sentence = new StringBuilder();
            sentence.Append(" INSERT INTO DISCOUNT(ID, INVOICE_NUMBER)");
            sentence.Append(" VALUES(@ID, @INVOICE_NUMBER)");

            list.Add(new Parameter("@ID", discount.Id));
            list.Add(new Parameter("@INVOICE_NUMBER", discount.InvoiceNumber));

            Parameter = list.ToArray();
            try
            {
                Conection.Insert(sentence.ToString(), Parameter);
                return discount.Id;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public Objects.Discount GetByInvoice(string invoice)
        {
            List<Parameter> list = new List<Parameter>();
            StringBuilder sentence = new StringBuilder();

            sentence.Append(" SELECT ");
            sentence.Append(" 	ID,");
            sentence.Append(" 	INVOICE_NUMBER");
            sentence.Append(" FROM DISCOUNT");
            sentence.Append(" WHERE INVOICE_NUMBER = @INVOICE_NUMBER");

            Objects.Discount entry = null;
            DataSet data = new DataSet();
            try
            {
                list.Add(new Parameter("@INVOICE_NUMBER", invoice));

                Parameter = list.ToArray();
                data = Conection.GetDataSet(sentence.ToString(), Parameter);

                if (data.Tables[0].Rows.Count > 0)
                {
                    entry = new Objects.Discount()
                    {
                        Id = int.Parse(data.Tables[0].Rows[0]["ID"].ToString()),
                        InvoiceNumber = data.Tables[0].Rows[0]["INVOICE_NUMBER"].ToString()
                    };
                }

                return entry;

            }
            catch (Exception ex)
            {
                return entry;
            }
        }
    }
}
