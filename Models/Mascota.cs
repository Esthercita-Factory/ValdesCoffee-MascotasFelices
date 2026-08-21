using ClinicaPatitasFelices.Console.Interfaces;

namespace ClinicaPatitasFelices.Console.Models
{
    public class Mascota : IRegistrable
    {
        public Guid Id { get; private set; }
        public string Nombre { get; set; }
        public string Especie { get; set; }
        public string Raza { get; set; }
        public Paciente? Dueno { get; set; }

        public Mascota(string nombre, string especie, string raza)
        {
            Id = Guid.NewGuid();
            Nombre = nombre;
            Especie = especie;
            Raza = raza;
        }

        public void Registrar()
        {
            // Lógica de dominio (ej. marcar como "Activo" en el sistema).
        }

        // Retorna la cadena en vez de imprimir: quien imprime es la capa UI.
        public string ObtenerInformacion()
        {
            string infoDueno = Dueno != null ? Dueno.Nombre : "Sin dueño asignado";
            return $"ID: {Id}\nNombre: {Nombre}\nEspecie: {Especie}\nRaza: {Raza}\nDueño: {infoDueno}";
        }
    }
}
