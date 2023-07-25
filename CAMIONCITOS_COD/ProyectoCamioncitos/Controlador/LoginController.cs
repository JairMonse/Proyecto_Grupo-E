using ProyectoCamioncitos.Modelo.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoCamioncitos.Controlador
{
    //Controlador de la Vista del Login
    class LoginController
    {
        LoginView Vista;
        //Constructor
        public LoginController(LoginView view)
        {
            Vista = view;
            //inicializar eventos
            Vista.pSalir.Click += new EventHandler(Cerrar);
            Vista.pMinimizar.Click += new EventHandler(Minimizar);
            Vista.btnIniciarSesion.Click += new EventHandler(LoginBtn);
        }
        //Evento Cerrar Vista
        public void Cerrar(object sender, EventArgs e)
        {
            Vista.Close();
        }
        //Evento Minimizar Vista
        public void Minimizar(object sender, EventArgs e)
        {
            Vista.WindowState = System.Windows.Forms.FormWindowState.Minimized;
        }
        //Evento Login
        public void LoginBtn(object sender, EventArgs e)
        {
            LoginDAO Login = new LoginDAO();

            if (Login.LoginEmpleado(Vista.txtUser.Text, Vista.txtPassword.Text))
            {
                Vista.Close();
            }
            else
            {
                MessageBox.Show("CI o Contraseña Incorrecto", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
