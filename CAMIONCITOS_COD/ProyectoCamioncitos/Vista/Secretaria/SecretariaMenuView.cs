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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pCerrar_Click(object sender, EventArgs e)
        {

        }

        private void panelForms_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtCI_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
