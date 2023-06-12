using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCamioncitos.Modelo.DTO
{
    public class Chofer : Empleado
    {
        //ATRIBUTOS

        string _Disponibilidad;

        //Getters y Setters

        public string Disponibilidad { get => _Disponibilidad; set => _Disponibilidad = value; }
    }
}
