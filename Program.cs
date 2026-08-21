using ClinicaPatitasFelices.Console.Services;
using ClinicaPatitasFelices.Console.UI;

// Composition root: aquí y solo aquí se arma el grafo de dependencias.
var pacienteService = new PacienteService();
var mascotaService = new MascotaService();

var menu = new MenuPrincipal(pacienteService, mascotaService);
menu.Mostrar();
