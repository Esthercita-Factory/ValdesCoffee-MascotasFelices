using ClinicaPatitasFelices.Console.Models;

namespace ClinicaPatitasFelices.Console.Services
{
    public class MascotaService
    {
        private readonly List<Mascota> _mascotas = new();

        public Mascota Registrar(string nombre, string especie, string raza, Paciente? dueno = null)
        {
            var mascota = new Mascota(nombre, especie, raza);
            dueno?.AgregarMascota(mascota);
            mascota.Registrar();
            _mascotas.Add(mascota);
            return mascota;
        }

        public IReadOnlyList<Mascota> ObtenerTodos() => _mascotas;
    }
}
