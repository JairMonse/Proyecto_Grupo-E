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
    //Vista CRUD Cliente
    public partial class ClienteCrudView : Form
    {
        public ClienteCrudView()
        {
            InitializeComponent();
            //Vista a Controlador
            ClienteCrudController ctrl = new ClienteCrudController(this);
        }

        private void gbClienteDatos_Enter(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}
