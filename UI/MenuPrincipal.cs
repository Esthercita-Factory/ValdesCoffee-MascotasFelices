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

        public void Mostrar()
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
                System.Console.WriteLine("5. Salir");
                System.Console.Write("Elige una opción: ");

                string opcion = System.Console.ReadLine() ?? "";

                switch (opcion)
                {
                    case "1":
                        RegistrarPacienteUI();
                        break;
                    case "2":
                        RegistrarMascotaUI();
                        break;
                    case "3":
                        EnviarRecordatorioUI();
                        break;
                    case "4":
                        ListarPacientesUI();
                        break;
                    case "5":
                        continuar = false;
                        break;
                    default:
                        System.Console.WriteLine("Opción inválida.");
                        break;
                }
            }
        }

        private void RegistrarPacienteUI()
        {
            System.Console.ForegroundColor = System.ConsoleColor.DarkRed;
            System.Console.Write("Nombre del dueño: ");
            string nombre = System.Console.ReadLine() ?? "";
            System.Console.Write("Teléfono: ");
            string telefono = System.Console.ReadLine() ?? "";
            System.Console.Write("Email: ");
            string email = System.Console.ReadLine() ?? "";

            var paciente = _pacienteService.Registrar(nombre, telefono, email);
            System.Console.WriteLine($"Paciente registrado exitosamente. ID: {paciente.Id}");
        }

        private void RegistrarMascotaUI()
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

            var mascota = _mascotaService.Registrar(nombre, especie, raza, dueno);
            System.Console.WriteLine($"Mascota registrada exitosamente. ID: {mascota.Id}");
        }

        private void EnviarRecordatorioUI()
        {
            System.Console.Write("ID del paciente a notificar: ");
            string idPaciente = System.Console.ReadLine() ?? "";

            if (!Guid.TryParse(idPaciente, out var id) || !_pacienteService.EnviarRecordatorioCita(id))
            {
                System.Console.WriteLine("No se encontró un paciente con ese ID.");
            }
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
    }
}
