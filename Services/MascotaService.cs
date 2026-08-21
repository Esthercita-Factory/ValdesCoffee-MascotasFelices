using ClinicaPatitasFelices.Console.Models;

namespace ClinicaPatitasFelices.Console.Services
{
    public class MascotaService
    {
        private readonly List<Mascota> _mascotas = new();

        // Simula un registro con espera (ej. apertura de historia clínica).
        public async Task<Mascota> RegistrarMascotaAsync(string nombre, string especie, string raza, Paciente? dueno = null)
        {
            await Task.Delay(1000);

            var mascota = new Mascota(nombre, especie, raza);
            dueno?.AgregarMascota(mascota);
            mascota.Registrar();
            _mascotas.Add(mascota);
            return mascota;
        }

        public IReadOnlyList<Mascota> ObtenerTodos() => _mascotas;
    }
}
