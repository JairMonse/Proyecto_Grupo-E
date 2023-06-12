using ProyectoCamioncitos.Modelo.DAO;
using ProyectoCamioncitos.Modelo.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoCamioncitos.Controlador
{
    //Controlador de la Vista CRUD Cliente
    class ClienteCrudController
    {
        ClienteCrudView Vista;

        //Constructor
        public ClienteCrudController(ClienteCrudView view)
        {
            Vista = view;
            //inicializar eventos
            Vista.Load += new EventHandler(Load);
            Vista.txtBuscarCliente.TextChanged += new EventHandler(Busqueda);
            Vista.tblClientes.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvClientes_SelectedRows);
            Vista.btnLimpiar.Click += new EventHandler(btnLimpiar);
            Vista.btnGuardar.Click += new EventHandler(CreateUser);
            Vista.btnEliminar.Click += new EventHandler(DeleteUser);
            Vista.btnEditar.Click += new EventHandler(UpdateUser);
        }
        //Evento Cargar Vista para cuanto es abierta la Vista
        public void Load(object sender, EventArgs e)
        {
            CargarClientes();
            Limpiar();
        }
        //Evento Buscar Clientes
        public void Busqueda(object sender, EventArgs e)
        {
            CargarClientes();
        }
        //Método Cargar Clientes
        public void CargarClientes()
        {
            ClienteDAO db = new ClienteDAO();
            Vista.tblClientes.DataSource =
                db.VerRegistros(Vista.txtBuscarCliente.Text);
        }
        //Evento Seleccion Fila Cliente
        public void dgvClientes_SelectedRows(object sender, EventArgs e)
        {
            //Pasa los datos de la fila seleccionada de la tabla Cliente a los textboxs
            if (Vista.tblClientes.SelectedRows.Count > 0)
            {
                ClienteDAO db = new ClienteDAO();
                List<Cliente> ClienteR = db.VerRegistros(Vista.tblClientes.CurrentRow.Cells[0].Value.ToString());
                Vista.txtRUC.Text = ClienteR[0].RUC;
                Vista.txtNombre.Text = ClienteR[0].Nombre;
                Vista.txtTelefono.Text = ClienteR[0].Telefono;
                Vista.txtCorreo.Text = ClienteR[0].Correo;
                Vista.txtDireccion.Text = ClienteR[0].Direccion;

                Vista.btnEditar.Enabled = true;
                Vista.btnGuardar.Enabled = false;
                Vista.btnEliminar.Enabled = true;
                Vista.txtRUC.Enabled = false;
            }
        }
        //Evento Limpiar Datos
        public void btnLimpiar(object sender, EventArgs e)
        {
            Limpiar();
        }
        //Evento Crear Cliente
        public void CreateUser(object sender, EventArgs e)
        {
            ClienteDAO db = new ClienteDAO();

            if (db.Create(Vista.txtRUC.Text, Vista.txtNombre.Text, Vista.txtTelefono.Text, Vista.txtCorreo.Text, Vista.txtDireccion.Text))
            {
                CargarClientes();
                Limpiar();
            }
            else
            {
                MessageBox.Show("Error al Crear Usuario", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Evento Eliminar Cliente
        public void DeleteUser(object sender, EventArgs e)
        {
            ClienteDAO db = new ClienteDAO();

            if (db.Delete(Vista.txtRUC.Text))
            {
                CargarClientes();
                Limpiar();
            }
            else
            {
                MessageBox.Show("Error al Eliminar Usuario", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Evento Modificar Cliente
        public void UpdateUser(object sender, EventArgs e)
        {
            ClienteDAO db = new ClienteDAO();

            if (db.Update(Vista.txtRUC.Text, Vista.txtNombre.Text, Vista.txtTelefono.Text, Vista.txtCorreo.Text, Vista.txtDireccion.Text))
            {
                CargarClientes();
                Limpiar();
            }
            else
            {
                MessageBox.Show("Error al Actualizar Usuario", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Método limpiar txts
        public void Limpiar()
        {
            Vista.txtRUC.Text = "";
            Vista.txtNombre.Text = "";
            Vista.txtTelefono.Text = "";
            Vista.txtCorreo.Text = "";
            Vista.txtDireccion.Text = "";

            Vista.tblClientes.ClearSelection();
            Vista.btnEditar.Enabled = false;
            Vista.btnGuardar.Enabled = true;
            Vista.btnEliminar.Enabled = false;
            Vista.txtRUC.Enabled = true;
        }

    }
}
