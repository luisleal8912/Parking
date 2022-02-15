using System;
using System.Data;
using System.Data.SqlClient;

namespace Parking.Data.Access
{
    public class Connection
    {  /// <summary>
       /// Funcion que se conecta a la base de Datos 
       /// </summary>
       /// <returns>Connection</returns>
        private SqlConnection GetConnection()
        {
            try
            {
                string connectionString = "Conecction string";

                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// El método de extensión. Añade los parámetros de consulta para comandar objeto.
        /// </summary>
        /// <param name="pCommand">Command object.</param>
        /// <param name="pParms"> Arma los parámetros de consulta de nombre y valor.</param>
        private void SetParameters(SqlCommand pCommand, Parameter[] pParms)
        {
            SqlParameter sqlPara;
            if (pParms != null && pParms.Length > 0)
            {
                for (int i = 0; i < pParms.Length; i++)
                {
                    if (pParms[i] != null)
                    {
                        sqlPara = new SqlParameter();
                        sqlPara.ParameterName = pParms[i].ParameterName;
                        sqlPara.Value = pParms[i].Value;
                        pCommand.Parameters.Add(sqlPara);
                    }
                }
            }
        }

        /// <summary>
        /// Insert un registro en la base de datos 
        /// </summary>
        /// <param name="pSql">Sentencia Sql</param>
        /// <param name="pParms">Parametros</param>
        /// <returns>Valor</returns>
        public int Insert(string pSql, Parameter[] pParms)
        {
            try
            {
                using (SqlConnection vConnection = GetConnection())
                {
                    using (SqlCommand vCommand = new SqlCommand())
                    {
                        vCommand.Connection = vConnection;
                        SetParameters(vCommand, pParms);
                        vCommand.CommandText = pSql;
                        int i = Convert.ToInt16(vCommand.ExecuteScalar());
                        vConnection.Close();
                        return i;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(" Insert Error : " + ex.Message);
            }

        }

        /// <summary>
        /// Modifica Registro en la Base de datos
        /// </summary>
        /// <param name="pSql">Sentencia Sql</param>
        /// <param name="pParms">Parametros</param>
        /// <returns>Valor</returns>
        public int Update(string pSql, Parameter[] pParms)
        {
            try
            {
                using (SqlConnection vConnection = GetConnection())
                {
                    using (SqlCommand vCommand = new SqlCommand())
                    {
                        vCommand.Connection = vConnection;
                        vCommand.CommandText = pSql;
                        SetParameters(vCommand, pParms);
                        int i = vCommand.ExecuteNonQuery();
                        vConnection.Close();
                        return i;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(" Update Error : " + ex.Message);
            }
        }

        /// <summary>
        /// Funcion que retorna un conjunto de datos 
        /// </summary>
        /// <param name="pSql">Sentencia Sql</param>
        /// <returns>Dataset</returns>
        public DataSet GetDataSet(string pSql)
        {
            try
            {
                DataSet vDatos = new DataSet();
                using (SqlConnection vConnection = GetConnection())
                {
                    using (SqlCommand vCommand = new SqlCommand())
                    {
                        vCommand.Connection = vConnection;
                        vCommand.CommandText = pSql;
                        using (SqlDataAdapter vAdapter = new SqlDataAdapter())
                        {

                            vAdapter.SelectCommand = new SqlCommand();
                            vAdapter.SelectCommand = vCommand;
                            vAdapter.Fill(vDatos);
                            vConnection.Close();
                            return vDatos;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(" GetDataSet Error : " + ex.Message);
            }
        }

        /// <summary>
        /// Funcion que retorna un conjunto de datos 
        /// </summary>
        /// <param name="pSql">Sentencia Sql</param>
        /// <param name="pParms">Parametros</param>
        /// <returns>Dataset</returns>
        public DataSet GetDataSet(string pSql, Parameter[] pParms)
        {
            try
            {
                DataSet vDatos = new DataSet();
                using (SqlConnection vConnection = GetConnection())
                {
                    using (SqlCommand vCommand = new SqlCommand())
                    {
                        vCommand.Connection = vConnection;
                        vCommand.CommandText = pSql;
                        SetParameters(vCommand, pParms);                        
                        using (SqlDataAdapter vAdapter = new SqlDataAdapter())
                        {
                            vAdapter.SelectCommand = new SqlCommand();
                            vAdapter.SelectCommand = vCommand;
                            vAdapter.Fill(vDatos);
                            vConnection.Close();
                            return vDatos;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(" GetDataSet Error : " + ex.Message);
            }
        }
    }
}
