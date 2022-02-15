using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Data.Access.Implementation
{
    public class TypeVehicle : ITypeVehicle
    {
        private Connection Conection;
        private Parameter[] Parameter;

        public TypeVehicle()
        {
            Conection = new Connection();
        }

        public Objects.TypeVehicle Get(int typeId)
        {
            List<Parameter> list = new List<Parameter>();
            StringBuilder sentence = new StringBuilder();


            sentence.Append("SELECT ");
            sentence.Append(" type_id,");
            sentence.Append(" name,");
            sentence.Append(" price ");
            sentence.Append("FROM TYPE_VEHICLE ");
            sentence.Append("WHERE type_id = @TYPE_ID");


            Objects.TypeVehicle entry = null;
            DataSet data = new DataSet();
            try
            {
                list.Add(new Parameter("@TYPE_ID", typeId));

                Parameter = list.ToArray();
                data = Conection.GetDataSet(sentence.ToString(), Parameter);

                if (data.Tables[0].Rows.Count > 0)
                {
                    entry = new Objects.TypeVehicle()
                    {
                        Id = int.Parse(data.Tables[0].Rows[0]["TYPE_ID"].ToString()),
                        Name = data.Tables[0].Rows[0]["NAME"].ToString(),
                        Price = int.Parse(data.Tables[0].Rows[0]["price"].ToString())
                    };
                }

                return entry;

            }
            catch (Exception ex)
            {
                return entry;
            }
        }

        private Objects.TypeVehicle Construct(SqlDataReader reader)
        {
            Objects.TypeVehicle item = new Objects.TypeVehicle();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.IsDBNull(i)) continue;

                string fieldName = reader.GetName(i).ToUpper();

                switch (fieldName)
                {
                    case "ID_TYPE":
                        item.Id = reader.GetInt32(i);
                        break;
                    case "NAME":
                        item.Name = reader.GetString(i);
                        break;
                    case "PRICE":
                        item.Price = reader.GetInt32(i);
                        break;
                }
            }

            return item;
        }
    }
}
