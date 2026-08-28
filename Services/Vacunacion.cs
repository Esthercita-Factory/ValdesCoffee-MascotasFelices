using ClinicaPatitasFelices.Console.Models;

namespace ClinicaPatitasFelices.Console.Services
{
    public class Vacunacion : ServicioVeterinario
    {
        public string NombreVacuna { get; set; }
        public bool Aplicada { get; private set; }

        public Vacunacion(string nombreVacuna)
        {
            NombreVacuna = nombreVacuna;
        }

        // Implementación de IAtendible (heredada vía ServicioVeterinario).
        public override void Atender(Mascota mascota)
        {
            Aplicada = true;
        }
    }
}
