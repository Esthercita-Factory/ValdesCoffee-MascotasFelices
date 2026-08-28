using ClinicaPatitasFelices.Console.Interfaces;
using ClinicaPatitasFelices.Console.Models;

namespace ClinicaPatitasFelices.Console.Services
{
    // Implementa IAtendible: toda subclase queda obligada a definir su propio Atender().
    public abstract class ServicioVeterinario : IAtendible
    {
        public DateTime Fecha { get; set; } = DateTime.Now;

        public abstract void Atender(Mascota mascota);
    }
}
