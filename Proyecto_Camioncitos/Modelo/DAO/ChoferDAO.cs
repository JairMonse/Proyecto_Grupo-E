using ProyectoCamioncitos.Modelo.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCamioncitos.Modelo.DAO
{
    //Clase Conductor Data Acces Object
    //Aqui se ejecutan todos los procesos que involucren a la BD para el Controlador del CRUD Chofer
    public class ChoferDAO : DBContext
    {
        SqlDataReader LeerFilas;
        SqlCommand Comando = new SqlCommand();

        //METODOS CRUD

        //Metodo Leer Chofer
        public List<Chofer> VerRegistros(string Condicion)
        {
            Comando.Connection = Conexion;
            Comando.CommandText = "ObtenerChofer";
            Comando.CommandType = CommandType.StoredProcedure;
            Comando.Parameters.AddWithValue("@Condicion", Condicion);
            Conexion.Open();
            LeerFilas = Comando.ExecuteReader();

            //DTO

            List<Chofer> ListaChofer = new List<Chofer>();
            while (LeerFilas.Read())
            {
                ListaChofer.Add(new Chofer
                {
                    CI = LeerFilas["Cedula"].ToString(),
                    Nombre = LeerFilas["Nombre"].ToString(),
                    Apellido = LeerFilas["Apellido"].ToString(),
                    Celular = LeerFilas["Celular"].ToString(),
                    Edad = Int32.Parse(LeerFilas["Edad"].ToString()),
                    Correo = LeerFilas["Correo"].ToString(),
                    Direccion = LeerFilas["Direccion"].ToString(),
                    Disponibilidad = LeerFilas["Disponibilidad"].ToString(),
                });
            }
            LeerFilas.Close();
            Conexion.Close();
            return ListaChofer;
        }

        //Metodo Crear Chofer
        public bool Create(string CI, string Nombre, string Apellido, string Celular,
            string Edad, string Correo, string Direccion, string Contraseña)
        {
            try
            {
                Comando.Connection = Conexion;
                Comando.CommandText = "CrearEmpleado";
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.AddWithValue("@CI", CI);
                Comando.Parameters.AddWithValue("@NOMBRE", Nombre);
                Comando.Parameters.AddWithValue("@APELLIDO", Apellido);
                Comando.Parameters.AddWithValue("@CELULAR", Celular);
                Comando.Parameters.AddWithValue("@EDAD", Edad);
                Comando.Parameters.AddWithValue("@CORREO", Correo);
                Comando.Parameters.AddWithValue("@DIRECCION", Direccion);
                Comando.Parameters.AddWithValue("@CONTRASEÑA", Contraseña);
                Comando.Parameters.AddWithValue("@TIPO", "Chofer");
                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Método Eliminar Chofer
        public bool Delete(string CI)
        {
            try
            {
                Comando.Connection = Conexion;
                Comando.CommandText = "EliminarEmpleado";
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.AddWithValue("@CI", CI);
                Conexion.Open();
                Comando.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Método Modificar Chofer
        public bool Update(string CI, string Nombre, string Apellido, string Celular, string Edad, string Correo, string Direccion , string Disponibilidad)
        {
            try
            {
                Comando.Connection = Conexion;
                Comando.CommandText = "ModificarChofer";
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.AddWithValue("@CI", CI);
                Comando.Parameters.AddWithValue("@NOMBRE", Nombre);
                Comando.Parameters.AddWithValue("@APELLIDO", Apellido);
                Comando.Parameters.AddWithValue("@CELULAR", Celular);
                Comando.Parameters.AddWithValue("@EDAD", Edad);
                Comando.Parameters.AddWithValue("@CORREO", Correo);
                Comando.Parameters.AddWithValue("@DIRECCION", Direccion);
                Comando.Parameters.AddWithValue("@DISPONIBILIDAD", Disponibilidad);
                Conexion.Open();
                Comando.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
