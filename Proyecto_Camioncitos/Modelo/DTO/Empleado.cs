using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCamioncitos.Modelo.DTO
{
    public class Empleado
    {
        //ATRIBUTOS
        protected string _CI;
        protected string _Nombre;
        protected string _Apellido;
        protected string _Celular;
        protected int _Edad;
        protected string _Correo;
        protected string _Direccion;
        protected string _Contraseña;

        //Getters y Setters
        public string CI { get => _CI; set => _CI = value; }
        public string Nombre { get => _Nombre; set => _Nombre = value; }
        public string Apellido { get => _Apellido; set => _Apellido = value; }
        public string Celular { get => _Celular; set => _Celular = value; }
        public int Edad { get => _Edad; set => _Edad = value; }
        public string Correo { get => _Correo; set => _Correo = value; }
        public string Direccion { get => _Direccion; set => _Direccion = value; }
        public string Contraseña { get => _Contraseña; set => _Contraseña = value; }
    }
}
