using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCamioncitos.Modelo.DTO
{
    public class Vehiculo
    {
        //ATRIBUTOS
        string _Matricula;
        string _Marca;
        string _Year;
        string _Disponibilidad;
        string _Tipo;

        //GETTERS Y SETTERS
        public string Matricula { get => _Matricula; set => _Matricula = value; }
        public string Marca { get => _Marca; set => _Marca = value; }
        public string Year { get => _Year; set => _Year = value; }
        public string Disponibilidad { get => _Disponibilidad; set => _Disponibilidad = value; }
        public string Tipo { get => _Tipo; set => _Tipo = value; }

    }
}
