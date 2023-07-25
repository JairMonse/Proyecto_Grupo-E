using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCamioncitos.Modelo.DTO
{
    public class Cliente
    {
        //ATRIBUTOS
        string _RUC;
        string _Nombre;
        string _Telefono;
        string _Correo;
        string _Direccion;

        //GETTERS Y SETTERS
        public string RUC { get => _RUC; set => _RUC = value; }
        public string Nombre { get => _Nombre; set => _Nombre = value; }
        public string Telefono { get => _Telefono; set => _Telefono = value; }
        public string Correo { get => _Correo; set => _Correo = value; }
        public string Direccion { get => _Direccion; set => _Direccion = value; }
    }
}
