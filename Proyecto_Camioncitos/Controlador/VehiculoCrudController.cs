using ProyectoCamioncitos.Modelo.DAO;
using ProyectoCamioncitos.Modelo.DTO;
using ProyectoCamioncitos.Vista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoCamioncitos.Controlador
{
    //Controlador de la Vista CRUD Vehiculo
    class VehiculoCrudController
    {
        VehiculoCrudView Vista;

        //Constructor
        public VehiculoCrudController(VehiculoCrudView view)
        {
            Vista = view;
            //inicializar eventos
            Vista.Load += new EventHandler(Load);
            Vista.txtBuscarVehiculo.TextChanged += new EventHandler(Busqueda);
            Vista.tblVehiculos.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvVehiculos_SelectedRows);
            Vista.btnLimpiar.Click += new EventHandler(btnLimpiar);
            Vista.btnGuardar.Click += new EventHandler(CreateVehicle);
            Vista.btnEliminar.Click += new EventHandler(DeleteVehicle);
            Vista.btnEditar.Click += new EventHandler(UpdateVehicle);
        }
        //Evento Cargar Vista para cuanto es abierta la Vista
        public void Load(object sender, EventArgs e)
        {
            CargarVehiculos();
            CargarCboxTipo();
            Limpiar();
        }
        //Evento Buscar Vehiculos
        public void Busqueda(object sender, EventArgs e)
        {
            CargarVehiculos();
        }
        //Método Cargar Vehiculos
        public void CargarVehiculos()
        {
            VehiculoDAO db = new VehiculoDAO();
            Vista.tblVehiculos.DataSource =
                db.VerRegistros(Vista.txtBuscarVehiculo.Text);

            Vista.tblVehiculos.Columns["Year"].Visible = false;
        }
        //Evento Seleccion Fila Vehiculo
        public void dgvVehiculos_SelectedRows(object sender, EventArgs e)
        {
            //Pasa los datos de la fila seleccionada de la tabla Vehiculo a los textboxs
            if (Vista.tblVehiculos.SelectedRows.Count > 0)
            {
                VehiculoDAO db = new VehiculoDAO();
                List<Vehiculo> VehiculoR = db.VerRegistros(Vista.tblVehiculos.CurrentRow.Cells[0].Value.ToString());
                Vista.txtMatricula.Text = VehiculoR[0].Matricula;
                Vista.txtMarca.Text = VehiculoR[0].Marca;
                Vista.txtYear.Text = VehiculoR[0].Year;
                Vista.cboxTipo.SelectedItem = VehiculoR[0].Tipo;
                Vista.cboxDisponibilidad.SelectedItem = VehiculoR[0].Disponibilidad;

                Vista.btnEditar.Enabled = true;
                Vista.btnGuardar.Enabled = false;
                Vista.btnEliminar.Enabled = true;
                Vista.txtMatricula.Enabled = false;
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
        //Evento Crear Vehiculo
        public void CreateVehicle (object sender, EventArgs e)
        {
            VehiculoDAO db = new VehiculoDAO();

            if (db.Create(Vista.txtMatricula.Text, Vista.txtMarca.Text, Vista.txtYear.Text, Vista.cboxTipo.SelectedItem.ToString()))
            {
                CargarVehiculos();
                Limpiar();
            }
            else
            {
                MessageBox.Show("Error al Crear Vehiculo", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Evento Eliminar Vehiculo
        public void DeleteVehicle(object sender, EventArgs e)
        {
            VehiculoDAO db = new VehiculoDAO();

            if (db.Delete(Vista.txtMatricula.Text))
            {
                CargarVehiculos();
                Limpiar();
            }
            else
            {
                MessageBox.Show("Error al Eliminar Vehiculo", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Evento Modificar Vehiculo
        public void UpdateVehicle(object sender, EventArgs e)
        {
            VehiculoDAO db = new VehiculoDAO();

            if (db.Update(Vista.txtMatricula.Text, Vista.txtMarca.Text, Vista.txtYear.Text, Vista.cboxTipo.SelectedItem.ToString(), Vista.cboxDisponibilidad.SelectedItem.ToString()))
            {
                CargarVehiculos();
                Limpiar();
            }
            else
            {
                MessageBox.Show("Error al Actualizar Vehiculo", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Método limpiar txts
        public void Limpiar()
        {
            Vista.txtMatricula.Text = "";
            Vista.txtMarca.Text = "";
            Vista.txtYear.Text = "";
            Vista.cboxTipo.SelectedItem = null;
            Vista.cboxDisponibilidad.SelectedItem = null;

            Vista.tblVehiculos.ClearSelection();
            Vista.btnEditar.Enabled = false;
            Vista.btnGuardar.Enabled = true;
            Vista.btnEliminar.Enabled = false;
            Vista.txtMatricula.Enabled = true;
            Vista.cboxDisponibilidad.Enabled = false;
            Vista.cboxDisponibilidad.Visible = false;
            Vista.lblDisponibilidad.Visible = false;
        }
        //Carga el combobox de tipos de vehiculos
        public void CargarCboxTipo()
        {
            VehiculoDAO db = new VehiculoDAO();
            Vista.cboxTipo.DataSource = db.CargarListaTipos();
        }

    }
}
