using ClinicaPatitasFelices.Services;
using ClinicaPatitasFelices.ConsoleApp.UI;
using ClinicaPatitasFelices.Services;

// 1. Configuración de Servicios (Dependency Injection manual)
var pacienteService = new PacienteService();
var mascotaService = new MascotaService();

// 2. Instanciación de la Interfaz de Usuario
var menu = new MenuPrincipal(pacienteService, mascotaService);

// 3. Ejecución de la aplicación
menu.Mostrar();