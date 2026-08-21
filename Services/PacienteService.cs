using ClinicaPatitasFelices.Console.Models;

namespace ClinicaPatitasFelices.Console.Services
{
    public class PacienteService
    {
        private readonly List<Paciente> _pacientes = new();

        public Paciente Registrar(string nombre, string telefono, string email)
        {
            var paciente = new Paciente(nombre, telefono, email);
            paciente.Registrar();
            _pacientes.Add(paciente);
            return paciente;
        }

        public IReadOnlyList<Paciente> ObtenerTodos() => _pacientes;

        public Paciente? BuscarPorId(Guid id) => _pacientes.FirstOrDefault(p => p.Id == id);

        public bool EnviarRecordatorioCita(Guid pacienteId)
        {
            var paciente = BuscarPorId(pacienteId);
            if (paciente == null)
            {
                return false;
            }

            paciente.EnviarNotificacion();
            return true;
        }
    }
}
