using ClinicaPatitasFelices.Console.Models;

namespace ClinicaPatitasFelices.Console.Interfaces
{
    public interface IAtendible
    {
        // Solo la firma. Cada servicio veterinario decide qué implica "atender".
        void Atender(Mascota mascota);
    }
}
