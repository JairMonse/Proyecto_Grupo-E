namespace camioncitos.Models.Retroalimentacion
{
    public class Retro_chofer
    {
        int id_retro;
        int id_chofer;
        String mensaje;
        int calificacion;
        String fecha_creacion;

        public Retro_chofer() { }

        public Retro_chofer(int id_retro, int id_chofer, string mensaje, int calificacion, string fecha_creacion)
        {
            this.id_retro = id_retro;
            this.id_chofer = id_chofer;
            this.mensaje = mensaje;
            this.calificacion = calificacion;
            this.fecha_creacion = fecha_creacion;
        }

        public int Id_retro { get => id_retro; set => id_retro = value; }
        public int Id_chofer { get => id_chofer; set => id_chofer = value; }
        public string Mensaje { get => mensaje; set => mensaje = value; }
        public int Calificacion { get => calificacion; set => calificacion = value; }
        public string Fecha_creacion { get => fecha_creacion; set => fecha_creacion = value; }
    }
}
