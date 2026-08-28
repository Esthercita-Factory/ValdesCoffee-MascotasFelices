using ClinicaPatitasFelices.Console.Models;

namespace ClinicaPatitasFelices.Console.Services
{
    public abstract class ServicioVeterinario
    {
        public DateTime Fecha { get; set; } = DateTime.Now;

        public abstract void Atender(Mascota mascota);
    }
}
