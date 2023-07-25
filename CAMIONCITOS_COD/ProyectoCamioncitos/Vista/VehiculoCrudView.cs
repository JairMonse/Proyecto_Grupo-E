using ProyectoCamioncitos.Controlador;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoCamioncitos.Vista
{
    //Vista CRUD Cliente
    public partial class VehiculoCrudView : Form
    {
        public VehiculoCrudView()
        {
            InitializeComponent();
            //Vista a Controlador
            VehiculoCrudController ctrl = new VehiculoCrudController(this);
        }
    }
}
