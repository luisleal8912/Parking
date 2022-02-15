using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Data.Access.Implementation
{
    public class Entry : IEntry
    {
        private Connection Conection;
        private Parameter[] Parameter;

        public Entry()
        {
            Conection = new Connection();
        }

        public int Add(Objects.Entry entry)
        {
            List<Parameter> list = new List<Parameter>();
            StringBuilder sentence = new StringBuilder();
            sentence.Append(" INSERT INTO VEHICLE_ENTRY(ID, VEHICLE_ID, TIME_ENTRY)");
            sentence.Append(" VALUES(@ID, @VEHICLE_ID, @TIME_ENTRY)");

            entry.Id = GetIdentityEntry();

            list.Add(new Parameter("@ID", entry.Id));
            list.Add(new Parameter("@VEHICLE_ID", entry.VehicleId));
            list.Add(new Parameter("@TIME_ENTRY", entry.EntryTime));

            Parameter = list.ToArray();
            try
            {
                Conection.Insert(sentence.ToString(), Parameter);
                return entry.Id;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public Objects.Entry GetDepartureNull(int vehicleId)
        {
            List<Parameter> list = new List<Parameter>();
            StringBuilder sentence = new StringBuilder();

            sentence.Append(" SELECT ");
            sentence.Append(" 	id,");
            sentence.Append(" 	vehicle_id,");
            sentence.Append(" 	time_entry,");
            sentence.Append(" 	departure_time");
            sentence.Append(" FROM VEHICLE_ENTRY");
            sentence.Append(" WHERE vehicle_id = @VEHICLE_ID");
            sentence.Append(" AND departure_time IS NULL");

            Objects.Entry entry = null;
            DataSet data = new DataSet();
            try
            {
                list.Add(new Parameter("@VEHICLE_ID", vehicleId));

                Parameter = list.ToArray();
                data = Conection.GetDataSet(sentence.ToString(), Parameter);

                if (data.Tables[0].Rows.Count > 0)
                {
                    entry = new Objects.Entry()
                    {
                        Id = int.Parse(data.Tables[0].Rows[0]["ID"].ToString()),
                        VehicleId = int.Parse(data.Tables[0].Rows[0]["VEHICLE_ID"].ToString()),
                        EntryTime = DateTime.Parse(data.Tables[0].Rows[0]["TIME_ENTRY"].ToString()),
                        ExitTime = string.IsNullOrEmpty(data.Tables[0].Rows[0]["DEPARTURE_TIME"].ToString()) ? null : DateTime.Parse(data.Tables[0].Rows[0]["DEPARTURE_TIME"].ToString())
                    };
                }

                return entry;

            }
            catch (Exception ex)
            {
                return entry;
            }
        }

        public int Update(Objects.Entry entry)
        {
            List<Parameter> list = new List<Parameter>();
            StringBuilder sentence = new StringBuilder();

            sentence.Append(" UPDATE VEHICLE_ENTRY ");
            sentence.Append(" SET DEPARTURE_TIME = @DEPARTURE_TIME");
            sentence.Append(" WHERE ID = @ID");

            list.Add(new Parameter("@ID", entry.Id));
            list.Add(new Parameter("@DEPARTURE_TIME", entry.ExitTime));

            Parameter = list.ToArray();
            try
            {
                Conection.Update(sentence.ToString(), Parameter);
                return entry.Id;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        private int GetIdentityEntry()
        {
            int number = 0;
            string Sentence = "";
            DataSet dataset = new DataSet();
            try
            {
                Sentence = "SELECT ISNULL(MAX(ID), 0) AS NUMB FROM VEHICLE_ENTRY";
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
