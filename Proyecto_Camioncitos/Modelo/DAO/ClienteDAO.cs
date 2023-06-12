using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ProyectoCamioncitos.Modelo.DTO;
using System.Data;

namespace ProyectoCamioncitos.Modelo.DAO
{
    //Clase Cliente Data Acces Object
    //Aqui se ejecutan todos los procesos que involucren a la BD para el Controlador del CRUD Cliente
    class ClienteDAO :DBContext
    {
        SqlDataReader LeerFilas;
        SqlCommand Comando = new SqlCommand();

        //METODOS CRUD

        //Metodo Leer Csuario
        public List<Cliente> VerRegistros(string Condicion)
        {
            Comando.Connection = Conexion;
            Comando.CommandText = "ObtenerCliente";
            Comando.CommandType = CommandType.StoredProcedure;
            Comando.Parameters.AddWithValue("@Condicion", Condicion);
            Conexion.Open();
            LeerFilas = Comando.ExecuteReader();
            //DTO

            List<Cliente> ListaCliente = new List<Cliente>();//Lista generica
            while (LeerFilas.Read())
            {
                ListaCliente.Add(new Cliente
                {
                    RUC = LeerFilas.GetString(0),
                    Nombre = LeerFilas.GetString(1),
                    Telefono = LeerFilas.GetString(2),
                    Correo = LeerFilas.GetString(3),
                    Direccion = LeerFilas.GetString(4),
                });
            }
            LeerFilas.Close();
            Conexion.Close();
            return ListaCliente;
        }

        //Metodo Crear Cliente
        public bool Create(string RUC, string Nombre, string Telefono, string Correo, string Direccion) 
        {
            try
            {
                Comando.Connection = Conexion;
                Comando.CommandText = "CrearCliente";
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.AddWithValue("@RUC", RUC);
                Comando.Parameters.AddWithValue("@NOMBRE", Nombre);
                Comando.Parameters.AddWithValue("@TELEFONO", Telefono);
                Comando.Parameters.AddWithValue("@CORREO", Correo);
                Comando.Parameters.AddWithValue("@DIRECCION", Direccion);
                Conexion.Open();
                Comando.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Método Eliminar Cliente
        public bool Delete(string RUC) 
        {
            try
            {
                Comando.Connection = Conexion;
                Comando.CommandText = "EliminarCliente";
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.AddWithValue("@RUC", RUC);
                Conexion.Open();
                Comando.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Método Modificar Cliente
        public bool Update(string RUC, string Nombre, string Telefono, string Correo, string Direccion) 
        {
            try
            {
                Comando.Connection = Conexion;
                Comando.CommandText = "ModificarCliente";
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.AddWithValue("@RUC", RUC);
                Comando.Parameters.AddWithValue("@NOMBRE", Nombre);
                Comando.Parameters.AddWithValue("@TELEFONO", Telefono);
                Comando.Parameters.AddWithValue("@CORREO", Correo);
                Comando.Parameters.AddWithValue("@DIRECCION", Direccion);
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
