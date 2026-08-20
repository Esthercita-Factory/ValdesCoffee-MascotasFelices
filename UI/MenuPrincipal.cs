namespace ClinicaPatitasFelices.ConsoleApp.UI
{
    using ClinicaPatitasFelices.Services;
    
    public class MenuPrincipal
    {
        private readonly PacienteService _pacienteService;
        private readonly MascotaService _mascotaService;

        // Inyección de dependencias a través del constructor
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
                System.Console.WriteLine("\n===== CLÍNICA SALUD+ =====");
                System.Console.WriteLine("1. Registrar paciente");
                System.Console.WriteLine("2. Salir");
                System.Console.Write("Elige una opción: ");

                string opcion = System.Console.ReadLine() ?? "";

                switch (opcion)
                {
                    case "1":
                        RegistrarPacienteUI();
                        break;
                    case "2":
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
            // La capa UI captura los datos y maneja los Console.WriteLine
            System.Console.Write("Nombre del paciente: ");
            string nombre = System.Console.ReadLine() ?? "";
            
            // Se envía la petición pura al servicio, separando la lógica
            // _pacienteService.Registrar(nombre, ...);
            System.Console.WriteLine("Paciente registrado exitosamente.");
        }
    }
}