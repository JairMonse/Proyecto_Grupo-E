namespace camioncitos.Utilitaria
{
    using System.Data.SqlClient;
    public class Utilitaria
    {
        String CONECTION_STRING = "workstation id=almidbone.mssql.somee.com;packet size=4096;user id=almidev_SQLLogin_1;pwd=ameo4brmgg;data source=almidbone.mssql.somee.com;persist security info=False;initial catalog=almidbone";


        public void getConection()
        {
            SqlConnection conection = new SqlConnection(CONECTION_STRING);

            conection.Open();

            conection.Close();
        }


    }
}
