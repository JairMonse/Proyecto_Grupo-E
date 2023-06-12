namespace camioncitos.Utilitaria
{
    using System.Data.SqlClient;
    using System.Data;
    public class Utilitaria
    {
        String CONECTION_STRING = "workstation id=almidbone.mssql.somee.com;packet size=4096;user id=almidev_SQLLogin_1;pwd=ameo4brmgg;data source=almidbone.mssql.somee.com;persist security info=False;initial catalog=almidbone";

        public SqlConnection getConection()
        {
            SqlConnection conn = new SqlConnection(CONECTION_STRING);
            conn.Open();
            return conn;          
        }

        public DataSet exec_query(String spName,SqlParameter sqlParm)
        {
            DataSet ds = new();
            //modificar
            using (SqlConnection conn = getConection())
            {
                using (SqlCommand command = new SqlCommand(spName,conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(sqlParm);
                    command.Connection = conn;

                    try
                    {
                        conn.Open();
                        SqlDataReader dr = command.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(dr);
                        ds.Tables.Add(dt);
                    }
                    catch (Exception e)
                    {
                        conn.Close();
                    }
                }
            return ds;
            }
        }
    }
}
