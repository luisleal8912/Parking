using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Parking.Data.Access.Implementation
{
    public class Vehicle : IVehicle
    {

        private Connection Conection;
        private Parameter[] Parameter;

        public Vehicle()
        {
            Conection = new Connection();
        }

        public int Add(Objects.Vehicle vehicle)
        {
            List<Parameter> list = new List<Parameter>();
            StringBuilder sentence = new StringBuilder();
            sentence.Append(" INSERT INTO VEHICLE(ID, TYPE_ID, PLATE, BRAND, MODEL, COMMENTS)");
            sentence.Append(" VALUES(@ID, @TYPE_ID, @PLATE, @BRAND, @MODEL, @COMMENTS)");

            vehicle.Id = GetIdentityVehicle();

            list.Add(new Parameter("@ID", vehicle.Id));
            list.Add(new Parameter("@TYPE_ID", vehicle.TypeId));
            list.Add(new Parameter("@PLATE", vehicle.Plate));
            list.Add(new Parameter("@BRAND", vehicle.Brand));
            list.Add(new Parameter("@MODEL", vehicle.Model));
            list.Add(new Parameter("@COMMENTS", vehicle.Comments));

            Parameter = list.ToArray();
            try
            {
                Conection.Insert(sentence.ToString(), Parameter);
                return vehicle.Id;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public Objects.Vehicle GetByPlate(string plate)
        {
            List<Parameter> list = new List<Parameter>();
            StringBuilder sentence = new StringBuilder();

            sentence.Append(" SELECT ID,");
            sentence.Append("	   TYPE_ID,");
            sentence.Append("	   PLATE,");
            sentence.Append("	   BRAND,");
            sentence.Append("	   MODEL,");
            sentence.Append("	   COMMENTS");
            sentence.Append("	   FROM VEHICLE");
            sentence.Append(" WHERE PLATE = @PLATE");

            Objects.Vehicle vehicle = null;
            DataSet data = new DataSet();
            try
            {
                list.Add(new Parameter("@PLATE", plate));

                Parameter = list.ToArray();
                data = Conection.GetDataSet(sentence.ToString(), Parameter);

                if (data.Tables[0].Rows.Count > 0)
                {
                    vehicle = new Objects.Vehicle()
                    {
                        Id = int.Parse(data.Tables[0].Rows[0]["ID"].ToString()),
                        TypeId = int.Parse(data.Tables[0].Rows[0]["TYPE_ID"].ToString()),
                        Brand = data.Tables[0].Rows[0]["BRAND"].ToString(),
                        Plate = data.Tables[0].Rows[0]["PLATE"].ToString(),
                        Comments = data.Tables[0].Rows[0]["COMMENTS"].ToString(),
                        Model = data.Tables[0].Rows[0]["MODEL"].ToString(),
                    };
                }

                return vehicle;
            }
            catch (Exception ex)
            {
                return vehicle;
            }

        }

        public List<Data.Objects.ResultFilter> GetFilter(DateTime from, DateTime to)
        {
            List<Parameter> list = new List<Parameter>();
            StringBuilder sentence = new StringBuilder();

            sentence.Append(" SELECT ");
            sentence.Append(" 	tv.name,");
            sentence.Append(" 	v.brand,");
            sentence.Append(" 	v.model,");
            sentence.Append(" 	v.plate,");
            sentence.Append(" 	v.comments,");
            sentence.Append(" 	ve.time_entry,");
            sentence.Append(" 	ve.departure_time,");
            sentence.Append(" 	p.total_minutes,");
            sentence.Append(" 	p.total,");
            sentence.Append(" 	d.invoice_number");
            sentence.Append(" FROM VEHICLE v");
            sentence.Append(" JOIN TYPE_VEHICLE tv ON tv.type_id = v.type_id ");
            sentence.Append(" JOIN VEHICLE_ENTRY ve ON ve.vehicle_id = v.id");
            sentence.Append(" LEFT JOIN PAYMENT p ON p.id = ve.id");
            sentence.Append(" LEFT JOIN DISCOUNT d ON d.id = p.id ");
            sentence.Append(" WHERE time_entry BETWEEN @FROM AND @TO");

            List<Data.Objects.ResultFilter> result = null;
            DataSet data = new DataSet();
            try
            {
                list.Add(new Parameter("@FROM", from));
                list.Add(new Parameter("@TO", to));

                Parameter = list.ToArray();
                data = Conection.GetDataSet(sentence.ToString(), Parameter);

                if (data.Tables[0].Rows.Count > 0)
                {
                    result = new List<Objects.ResultFilter>();

                    for (int i = 0; i < data.Tables[0].Rows.Count; i++)
                    {
                        result.Add(new Objects.ResultFilter()
                        {
                            Name = data.Tables[0].Rows[i]["NAME"].ToString(),
                            Brand = data.Tables[0].Rows[i]["BRAND"].ToString(),
                            Model = data.Tables[0].Rows[i]["MODEL"].ToString(),
                            Plate = data.Tables[0].Rows[i]["PLATE"].ToString(),
                            Comments = data.Tables[0].Rows[i]["COMMENTS"].ToString(),
                            EntryTime = DateTime.Parse(data.Tables[0].Rows[i]["TIME_ENTRY"].ToString()),
                            ExitTime = string.IsNullOrEmpty(data.Tables[0].Rows[i]["DEPARTURE_TIME"].ToString()) ? null : DateTime.Parse(data.Tables[0].Rows[i]["DEPARTURE_TIME"].ToString()),
                            Minutes = string.IsNullOrEmpty(data.Tables[0].Rows[i]["TOTAL_MINUTES"].ToString()) ? 0 : int.Parse(data.Tables[0].Rows[i]["TOTAL_MINUTES"].ToString()),
                            Total = string.IsNullOrEmpty(data.Tables[0].Rows[i]["TOTAL"].ToString()) ? null : decimal.Round(decimal.Parse(data.Tables[0].Rows[i]["TOTAL"].ToString()), 2),
                            InvoiceNumber = data.Tables[0].Rows[i]["INVOICE_NUMBER"].ToString()
                        });
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private int GetIdentityVehicle()
        {
            int number = 0;
            string Sentence = "";
            DataSet dataset = new DataSet();
            try
            {
                Sentence = "SELECT ISNULL(MAX(ID), 0) AS NUMB FROM VEHICLE";
                dataset = Conection.GetDataSet(Sentence);
                number = int.Parse(dataset.Tables[0].Rows[0]["NUMB"].ToString());
                number++;
                return number;
            }
            catch
            {
                number = 0;
                number++;
                return number;
            }
        }
    }
}
