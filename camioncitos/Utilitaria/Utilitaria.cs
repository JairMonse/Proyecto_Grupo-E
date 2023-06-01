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

            return conn;          
        }

        public void exec_query(String query, SqlConnection conn)
        {
            //modificar
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                

                
            }

                
        }
    }
}
