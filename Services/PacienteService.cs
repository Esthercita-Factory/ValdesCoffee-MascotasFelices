using ClinicaPatitasFelices.Console.Models;

namespace ClinicaPatitasFelices.Console.Services
{
    public class PacienteService
    {
        private readonly List<Paciente> _pacientes = new();

        // Simula un registro con espera (ej. escritura en base de datos).
        // Al ser async, el hilo que la invoca queda libre mientras el Task.Delay corre.
        public async Task<Paciente> RegistrarPacienteAsync(string nombre, string telefono, string email)
        {
            await Task.Delay(1200);

            var paciente = new Paciente(nombre, telefono, email);
            paciente.Registrar();
            _pacientes.Add(paciente);
            return paciente;
        }

        public IReadOnlyList<Paciente> ObtenerTodos() => _pacientes;

        public Paciente? BuscarPorId(Guid id) => _pacientes.FirstOrDefault(p => p.Id == id);

        // Simula el envío de un recordatorio por dos canales en paralelo (SMS y Email)
        // y se queda con el que responda primero mediante Task.WhenAny.
        public async Task<string> EnviarRecordatorioCitaAsync(Guid pacienteId)
        {
            var paciente = BuscarPorId(pacienteId);
            if (paciente == null)
            {
                return string.Empty;
            }

            var tareaSms = SimularEnvioPorCanalAsync("SMS");
            var tareaEmail = SimularEnvioPorCanalAsync("Email");

            var tareaGanadora = await Task.WhenAny(tareaSms, tareaEmail);
            string canalGanador = await tareaGanadora;

            paciente.EnviarNotificacion();
            return canalGanador;
        }

        private static async Task<string> SimularEnvioPorCanalAsync(string nombreCanal)
        {
            int latenciaMs = Random.Shared.Next(300, 1500);
            await Task.Delay(latenciaMs);
            return nombreCanal;
        }
    }
}
