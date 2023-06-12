using ProyectoCamioncitos.Modelo.DAO;
using ProyectoCamioncitos.Modelo.DTO;
using ProyectoCamioncitos.Vista.Conductor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoCamioncitos.Controlador
{
    //Controlador de la Vista CRUD Chofer
    class ChoferCrudController
    {
        ChoferCrudView Vista;

        //Constructor
        public ChoferCrudController(ChoferCrudView view)
        {
            Vista = view;
            //inicializar eventos
            Vista.Load += new EventHandler(Load);
            Vista.txtBuscarChofer.TextChanged += new EventHandler(Busqueda);
            Vista.tblChofer.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvChofer_SelectedRows);
            Vista.btnLimpiar.Click += new EventHandler(btnLimpiar);
            Vista.btnNuevoChofer.Click += new EventHandler(CreateChofer);
            Vista.btnEliminar.Click += new EventHandler(DeleteChofer);
            Vista.btnEditar.Click += new EventHandler(UpdateChofer);
        }
        //Evento Cargar Vista para cuanto es abierta la Vista
        public void Load(object sender, EventArgs e)
        {
            CargarChofer();
            Limpiar();
        }
        //Evento Buscar Chofer
        public void Busqueda(object sender, EventArgs e)
        {
            CargarChofer();
        }
        //Método Cargar Chofer
        public void CargarChofer()
        {
            ChoferDAO db = new ChoferDAO();
            Vista.tblChofer.DataSource =
                db.VerRegistros(Vista.txtBuscarChofer.Text);

            Vista.tblChofer.Columns["Contraseña"].Visible = false;
            Vista.tblChofer.Columns["Celular"].Visible = false;
            Vista.tblChofer.Columns["Edad"].Visible = false;
            Vista.tblChofer.Columns["Correo"].Visible = false;
            Vista.tblChofer.Columns["Direccion"].Visible = false;

            Vista.tblChofer.Columns["CI"].DisplayIndex = 0;
            Vista.tblChofer.Columns["Disponibilidad"].DisplayIndex = 3;
        }
        //Evento Seleccion Fila Chofer
        public void dgvChofer_SelectedRows(object sender, EventArgs e)
        {
            //Pasa los datos de la fila seleccionada de la tabla Chofer a los textboxs
            if (Vista.tblChofer.SelectedRows.Count > 0)
            {
                ChoferDAO db = new ChoferDAO();
                List<Chofer> ChoferR = db.VerRegistros(Vista.tblChofer.CurrentRow.Cells[1].Value.ToString());
                Vista.txtCI.Text = ChoferR[0].CI;
                Vista.txtNombre.Text = ChoferR[0].Nombre;
                Vista.txtApellido.Text = ChoferR[0].Apellido;
                Vista.txtCelular.Text = ChoferR[0].Celular;
                Vista.txtEdad.Text = ChoferR[0].Edad.ToString();
                Vista.txtCorreo.Text = ChoferR[0].Correo;
                Vista.txtDireccion.Text = ChoferR[0].Direccion;
                Vista.cboxDisponibilidad.SelectedItem = ChoferR[0].Disponibilidad;

                Vista.btnEditar.Enabled = true;
                Vista.btnNuevoChofer.Enabled = false;
                Vista.btnEliminar.Enabled = true;

                Vista.txtPassword.Visible = false;
                Vista.txtPassword.Enabled = false;
                Vista.lblPassword.Visible = false;
                Vista.txtCI.Enabled = false;
                Vista.cboxDisponibilidad.Visible = true;
                Vista.cboxDisponibilidad.Enabled = true;
                Vista.lblDisponibilidad.Visible = true;
            }
        }
        //Evento Limpiar Datos
        public void btnLimpiar(object sender, EventArgs e)
        {
            Limpiar();
        }
        //Evento Crear Chofer
        public void CreateChofer(object sender, EventArgs e)
        {
            ChoferDAO db = new ChoferDAO();

            if (db.Create(Vista.txtCI.Text, Vista.txtNombre.Text, Vista.txtApellido.Text, Vista.txtCelular.Text,
                Vista.txtEdad.Text, Vista.txtCorreo.Text, Vista.txtDireccion.Text, Vista.txtPassword.Text))
            {
                CargarChofer();
                Limpiar();
            }
            else
            {
                MessageBox.Show("Error al Crear Chofer", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Evento Eliminar Chofer
        public void DeleteChofer(object sender, EventArgs e)
        {
            ChoferDAO db = new ChoferDAO();

            if (db.Delete(Vista.txtCI.Text))
            {
                CargarChofer();
                Limpiar();
            }
            else
            {
                MessageBox.Show("Error al Eliminar Chofer", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Evento Modificar Chfoer
        public void UpdateChofer(object sender, EventArgs e)
        {
            ChoferDAO db = new ChoferDAO();

            if (db.Update(Vista.txtCI.Text, Vista.txtNombre.Text, Vista.txtApellido.Text,
                Vista.txtCelular.Text, Vista.txtEdad.Text, Vista.txtCorreo.Text, Vista.txtDireccion.Text,
                Vista.cboxDisponibilidad.SelectedItem.ToString()))
            {
                CargarChofer();
                Limpiar();
            }
            else
            {
                MessageBox.Show("Error al Editar Chofer", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Método limpiar txts
        public void Limpiar()
        {
            Vista.txtCI.Text = "";
            Vista.txtNombre.Text = "";
            Vista.txtApellido.Text = "";
            Vista.txtCelular.Text = "";
            Vista.txtEdad.Text = "";
            Vista.txtCorreo.Text = "";
            Vista.txtDireccion.Text = "";
            Vista.cboxDisponibilidad.SelectedItem = null;

            Vista.tblChofer.ClearSelection();

            Vista.btnEditar.Enabled = false;
            Vista.btnEliminar.Enabled = false;
            Vista.btnNuevoChofer.Enabled = true;

            Vista.txtPassword.Visible = true;
            Vista.txtPassword.Enabled = true;
            Vista.lblPassword.Visible = true;
            Vista.txtCI.Enabled = true;
            Vista.cboxDisponibilidad.Enabled = false;
            Vista.cboxDisponibilidad.Visible = false;
            Vista.lblDisponibilidad.Visible = false;
        }

    }
}
