namespace camioncitos.Models.Chofer
{
    public class Chofer
    {
        int id_chofer;
        String cedula;
        String nombres;
        String apellidos;
        int edad;
        int anios_exp;
        Boolean activo;
        String telefono;
        String correo;
        String fecha_creacion;

        public Chofer() { }

        public Chofer(int id_chofer, string cedula, string nombres, 
            string apellidos, int edad, int anios_exp, bool activo, 
            string telefono, string correo, string fecha_creacion)
        {
            this.id_chofer = id_chofer;
            this.cedula = cedula;
            this.nombres = nombres;
            this.apellidos = apellidos;
            this.edad = edad;
            this.anios_exp = anios_exp;
            this.activo = activo;
            this.telefono = telefono;
            this.correo = correo;
            this.fecha_creacion = fecha_creacion;
        }

        public int Id_chofer { get => id_chofer; set => id_chofer = value; }
        public string Cedula { get => cedula; set => cedula = value; }
        public string Nombres { get => nombres; set => nombres = value; }
        public string Apellidos { get => apellidos; set => apellidos = value; }
        public int Edad { get => edad; set => edad = value; }
        public int Anios_exp { get => anios_exp; set => anios_exp = value; }
        public bool Activo { get => activo; set => activo = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public string Correo { get => correo; set => correo = value; }
        public string Fecha_creacion { get => fecha_creacion; set => fecha_creacion = value; }
    }
}
