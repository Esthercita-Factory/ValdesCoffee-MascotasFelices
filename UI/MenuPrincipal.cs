using System.Diagnostics;
using ClinicaPatitasFelices.Console.Models;
using ClinicaPatitasFelices.Console.Services;

namespace ClinicaPatitasFelices.Console.UI
{
    public class MenuPrincipal
    {
        private readonly PacienteService _pacienteService;
        private readonly MascotaService _mascotaService;

        public MenuPrincipal(PacienteService pacienteService, MascotaService mascotaService)
        {
            _pacienteService = pacienteService;
            _mascotaService = mascotaService;
        }

        public async Task MostrarAsync()
        {
            bool continuar = true;
            while (continuar)
            {
                System.Console.ForegroundColor = System.ConsoleColor.Magenta;
                System.Console.WriteLine("\n===== CLÍNICA PATITAS FELICES =====");
                System.Console.WriteLine("1. Registrar paciente (dueño)");
                System.Console.WriteLine("2. Registrar mascota");
                System.Console.WriteLine("3. Enviar recordatorio de cita");
                System.Console.WriteLine("4. Listar pacientes y sus mascotas");
                System.Console.WriteLine("5. Demo: registrar varios pacientes en paralelo ");
                System.Console.WriteLine("6. Salir");
                System.Console.Write("Elige una opción: ");

                string opcion = System.Console.ReadLine() ?? "";

                switch (opcion)
                {
                    case "1":
                        await RegistrarPacienteUIAsync();
                        break;
                    case "2":
                        await RegistrarMascotaUIAsync();
                        break;
                    case "3":
                        await EnviarRecordatorioUIAsync();
                        break;
                    case "4":
                        ListarPacientesUI();
                        break;
                    case "5":
                        await RegistrarPacientesEnParaleloDemoUIAsync();
                        break;
                    case "6":
                        continuar = false;
                        break;
                    default:
                        System.Console.WriteLine("Opción inválida.");
                        break;
                }
            }
        }

        private async Task RegistrarPacienteUIAsync()
        {
            System.Console.ForegroundColor = System.ConsoleColor.DarkRed;
            System.Console.Write("Nombre del dueño: ");
            string nombre = System.Console.ReadLine() ?? "";
            System.Console.Write("Teléfono: ");
            string telefono = System.Console.ReadLine() ?? "";
            System.Console.Write("Email: ");
            string email = System.Console.ReadLine() ?? "";

            System.Console.WriteLine("[Antes] Enviando datos para registrar al paciente...");
            Task<Paciente> tareaRegistro = _pacienteService.RegistrarPacienteAsync(nombre, telefono, email);
            System.Console.WriteLine("[Durante] El registro se procesa en segundo plano; la aplicación no se bloquea...");

            Paciente paciente = await tareaRegistro;
            System.Console.WriteLine($"[Después] Paciente registrado exitosamente. ID: {paciente.Id}");
        }

        private async Task RegistrarMascotaUIAsync()
        {
            System.Console.ForegroundColor = System.ConsoleColor.DarkRed;
            System.Console.Write("Nombre de la mascota: ");
            string nombre = System.Console.ReadLine() ?? "";
            System.Console.Write("Especie: ");
            string especie = System.Console.ReadLine() ?? "";
            System.Console.Write("Raza: ");
            string raza = System.Console.ReadLine() ?? "";
            System.Console.Write("ID del dueño (vacío si no tiene): ");
            string idDueno = System.Console.ReadLine() ?? "";

            Paciente? dueno = null;
            if (Guid.TryParse(idDueno, out var id))
            {
                dueno = _pacienteService.BuscarPorId(id);
                if (dueno == null)
                {
                    System.Console.WriteLine("No se encontró un dueño con ese ID. Se registrará sin dueño.");
                }
            }

            System.Console.WriteLine("[Antes] Enviando datos para registrar la mascota...");
            Task<Mascota> tareaRegistro = _mascotaService.RegistrarMascotaAsync(nombre, especie, raza, dueno);
            System.Console.WriteLine("[Durante] El registro se procesa en segundo plano; la aplicación no se bloquea...");

            Mascota mascota = await tareaRegistro;
            System.Console.WriteLine($"[Después] Mascota registrada exitosamente. ID: {mascota.Id}");
        }

        private async Task EnviarRecordatorioUIAsync()
        {
            System.Console.Write("ID del paciente a notificar: ");
            string idPaciente = System.Console.ReadLine() ?? "";

            if (!Guid.TryParse(idPaciente, out var id))
            {
                System.Console.WriteLine("ID inválido.");
                return;
            }

            System.Console.WriteLine("[Antes] Enviando recordatorio de cita por los canales disponibles...");
            Task<string> tareaRecordatorio = _pacienteService.EnviarRecordatorioCitaAsync(id);
            System.Console.WriteLine("[Durante] Compitiendo SMS vs Email (Task.WhenAny), sin bloquear la aplicación...");

            string canalGanador = await tareaRecordatorio;

            if (string.IsNullOrEmpty(canalGanador))
            {
                System.Console.WriteLine("No se encontró un paciente con ese ID.");
                return;
            }

            System.Console.WriteLine($"[Después] Recordatorio entregado por el canal más rápido: {canalGanador}.");
        }

        private void ListarPacientesUI()
        {
            var pacientes = _pacienteService.ObtenerTodos();
            if (pacientes.Count == 0)
            {
                System.Console.WriteLine("No hay pacientes registrados.");
                return;
            }

            foreach (var paciente in pacientes)
            {
                System.Console.WriteLine("\n" + paciente.ObtenerInformacion());
                foreach (var mascota in paciente.Mascotas)
                {
                    System.Console.WriteLine($"  - {mascota.Nombre} ({mascota.Especie}, {mascota.Raza})");
                }
            }
        }

        // Registra 3 pacientes de ejemplo al mismo tiempo para demostrar Task.WhenAll:
        // el tiempo total es el de la tarea más lenta, no la suma de las tres.
        private async Task RegistrarPacientesEnParaleloDemoUIAsync()
        {
            System.Console.WriteLine("[Antes] Registrando 3 pacientes de ejemplo en paralelo...");
            var cronometro = Stopwatch.StartNew();

            Task<Paciente> tareaUno = _pacienteService.RegistrarPacienteAsync("Ana Torres", "3001111111", "ana@example.com");
            Task<Paciente> tareaDos = _pacienteService.RegistrarPacienteAsync("Luis Gómez", "3002222222", "luis@example.com");
            Task<Paciente> tareaTres = _pacienteService.RegistrarPacienteAsync("Marta Ruiz", "3003333333", "marta@example.com");

            System.Console.WriteLine("[Durante] Las 3 tareas corren al mismo tiempo, sin bloquear el hilo principal...");

            Paciente[] pacientesRegistrados = await Task.WhenAll(tareaUno, tareaDos, tareaTres);

            cronometro.Stop();
            System.Console.WriteLine($"[Después] Registro paralelo finalizado en {cronometro.ElapsedMilliseconds} ms:");
            foreach (var paciente in pacientesRegistrados)
            {
                System.Console.WriteLine($"  - {paciente.Nombre} (ID: {paciente.Id})");
            }
        }
    }
}
