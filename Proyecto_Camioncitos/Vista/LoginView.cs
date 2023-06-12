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

namespace ProyectoCamioncitos
{
    //Vista del LOGIN
    public partial class LoginView : Form
    {
        public LoginView()
        {
            InitializeComponent();
            //Vista a Controlador
            LoginController ctrl = new LoginController(this);
        }
    }
}
