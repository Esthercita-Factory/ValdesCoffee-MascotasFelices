namespace ClinicaVeterinariaLINQ.Models
{
    // Representa la mascota asociada a un paciente (dueño)
    public class Mascota
    {
        // Propiedades automáticas
        public string Nombre { get; set; }
        public string Especie { get; set; }   // Ej: "Perro", "Gato", "Ave"
        public string Raza { get; set; }       // Puede quedar vacía o null si no está definida
        public int Edad { get; set; }
    }
}
