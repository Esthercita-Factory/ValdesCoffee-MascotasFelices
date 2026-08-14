namespace ClinicaVeterinariaLINQ.Models
{
     // Clase Paciente con propiedades básicas usando propiedades automáticas
    public class Paciente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Sintoma { get; set; }

        // Datos adicionales usados en las consultas LINQ de las siguientes tareas
        public string Telefono { get; set; }
        public Mascota Mascota { get; set; }

        // Sobrescribimos ToString para mostrar la info de forma legible en consola
        public override string ToString()
        {
            string raza = string.IsNullOrEmpty(Mascota?.Raza) ? "Sin raza definida" : Mascota.Raza;
            return $"[{Id}] {Nombre} ({Edad} años) - Síntoma: {Sintoma} - Tel: {Telefono} " +
                   $"| Mascota: {Mascota?.Nombre} ({Mascota?.Especie}, {raza}, {Mascota?.Edad} años)";
        }
    }
}
