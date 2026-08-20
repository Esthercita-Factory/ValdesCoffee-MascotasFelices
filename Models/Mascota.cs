namespace ClinicaPatitasFelices.Models
{
    // Mantenemos un único namespace coherente y evitamos colisiones.
    public class Mascota : IRegistrable
    {
        public Guid Id { get; private set; }
        public string Nombre { get; set; }
        public string Especie { get; set; }
        public string Raza { get; set; }
        public Paciente? Dueno { get; set; }

        public Mascota(string nombre, string especie, string raza)
        {
            Id = Guid.NewGuid(); // Autogeneración del UUID al instanciar
            Nombre = nombre;
            Especie = especie;
            Raza = raza;
        }

        public void Registrar()
        {
            // Aquí va la lógica de dominio (ej. cambiar un estado interno a "Activo"), 
            // no la impresión en consola.
        }

        // En lugar de Console.WriteLine, retornamos la cadena.
        public string ObtenerInformacion()
        {
            string infoDueno = Dueno != null ? Dueno.Nombre : "Sin dueño asignado";
            return $"ID: {Id}\nNombre: {Nombre}\nEspecie: {Especie}\nRaza: {Raza}\nDueño: {infoDueno}";
        }
    }
}