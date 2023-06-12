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
    //Vista Menu Secretaria
    public partial class SecretariaMenuView : Form
    {
        public SecretariaMenuView()
        {
            InitializeComponent();
            //Vista a Controlador
            SecretariaMenuController ctrl = new SecretariaMenuController(this);
        }
    }
}
