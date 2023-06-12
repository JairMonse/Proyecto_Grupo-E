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

namespace ProyectoCamioncitos.Vista.Conductor
{
    public partial class ChoferCrudView : Form
    {
        public ChoferCrudView()
        {
            InitializeComponent();
            //Vista a Controlador
            ChoferCrudController ctrl = new ChoferCrudController(this);
        }
    }
}
