using ClinicaPets.Models;
using ClinicaPets.Services;

var pacienteService = new PacienteService();
var mascotaService = new MascotaService();

bool continuar = true;

while (continuar)
{
    Console.WriteLine("\n===== CLÍNICA SALUD+ =====");
    Console.WriteLine("1. Registrar paciente");
    Console.WriteLine("2. Listar pacientes");
    Console.WriteLine("3. Buscar paciente");
    Console.WriteLine("4. Modificar paciente");
    Console.WriteLine("5. Eliminar paciente");
    Console.WriteLine("6. Registrar mascota");
    Console.WriteLine("7. Ver mascotas de un paciente");
    Console.WriteLine("8. Consultas y estadísticas ");
    Console.WriteLine("9. Sonidos de las mascotas");
    Console.WriteLine("10. Registrar atención veterinaria");
    Console.WriteLine("11. Salir");
    Console.Write("Elige una opción: ");

    string opcion = Console.ReadLine() ?? "";

    switch (opcion)
    {
        case "1":
            RegistrarPaciente(pacienteService);
            break;

        case "2":
            ListarPacientes(pacienteService);
            break;

        case "3":
            BuscarPaciente(pacienteService);
            break;

        case "4":
            ModificarPaciente(pacienteService);
            break;

        case "5":
            EliminarPaciente(pacienteService);
            break;

        case "6":
            RegistrarMascota(pacienteService, mascotaService);
            break;

        case "7":
            VerMascotasDePaciente(pacienteService);
            break;

        case "8":
            MostrarConsultasLinq(pacienteService, mascotaService);
            break;

        case "9":
            DemostrarPolimorfismo(mascotaService);
            break;

        case "10":
            RegistrarAtencion(mascotaService);
            break;

        case "11":
            Console.WriteLine("\n¡Hasta luego!");
            continuar = false;
            break;

        default:
            Console.WriteLine("Opción no válida, intenta de nuevo.");
            break;
    }
}

static void RegistrarPaciente(PacienteService pacienteService)
{
    Console.Write("\nNombre del paciente: ");
    string nombre = Console.ReadLine() ?? "";

    if (string.IsNullOrWhiteSpace(nombre))
    {
        Console.WriteLine("El nombre no puede estar vacío.");
        return;
    }

    Console.Write("Edad del paciente: ");
    int edad;
    try
    {
        edad = Convert.ToInt32(Console.ReadLine());
        if (edad < 0)
        {
            Console.WriteLine("La edad no puede ser negativa.");
            return;
        }
    }
    catch (FormatException)
    {
        Console.WriteLine("La edad debe ser un número válido.");
        return;
    }

    Console.Write("Síntoma (opcional, Enter para omitir): ");
    string sintoma = Console.ReadLine() ?? "";

    Console.Write("Dirección (opcional): ");
    string direccion = Console.ReadLine() ?? "";

    Console.Write("Teléfono (opcional): ");
    string telefono = Console.ReadLine() ?? "";

    try
    {
        var paciente = pacienteService.Registrar(
            nombre, edad, string.IsNullOrWhiteSpace(sintoma) ? null : sintoma, direccion, telefono);

        Console.WriteLine();
        paciente.Registrar(); // IRegistrable: confirma el registro
        paciente.MostrarInformacion();
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"No se pudo registrar el paciente: {ex.Message}");
    }
}

static void ListarPacientes(PacienteService pacienteService)
{
    var pacientes = pacienteService.ListarTodos();

    if (pacientes.Count == 0)
    {
        Console.WriteLine("\nNo hay pacientes registrados.");
        return;
    }

    Console.WriteLine("\n--- Pacientes registrados ---");
    foreach (var paciente in pacientes)
    {
        paciente.MostrarInformacion();
    }
}

static void BuscarPaciente(PacienteService pacienteService)
{
    Console.Write("\nNombre del paciente a buscar: ");
    string nombre = Console.ReadLine() ?? "";

    var paciente = pacienteService.BuscarPorNombre(nombre);

    if (paciente == null)
    {
        Console.WriteLine("No se encontró ningún paciente con ese nombre.");
        return;
    }

    Console.WriteLine();
    paciente.MostrarInformacion();
}

static void ModificarPaciente(PacienteService pacienteService)
{
    Console.Write("\nId del paciente a modificar: ");
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("El Id debe ser un número válido.");
        return;
    }

    var paciente = pacienteService.BuscarPorId(id);
    if (paciente == null)
    {
        Console.WriteLine("No existe un paciente con ese Id.");
        return;
    }

    Console.Write("Nueva edad: ");
    if (!int.TryParse(Console.ReadLine(), out int nuevaEdad))
    {
        Console.WriteLine("La edad debe ser un número válido.");
        return;
    }

    try
    {
        pacienteService.ModificarEdad(id, nuevaEdad);
        Console.WriteLine("\n✓ Paciente actualizado:");
        paciente.MostrarInformacion();
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"No se pudo actualizar: {ex.Message}");
    }
}

static void EliminarPaciente(PacienteService pacienteService)
{
    Console.Write("\nId del paciente a eliminar: ");
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("El Id debe ser un número válido.");
        return;
    }

    if (pacienteService.Eliminar(id))
        Console.WriteLine("✓ Paciente eliminado correctamente.");
    else
        Console.WriteLine("No existe un paciente con ese Id.");
}

static void RegistrarMascota(PacienteService pacienteService, MascotaService mascotaService)
{
    Console.Write("\nId del paciente dueño: ");
    if (!int.TryParse(Console.ReadLine(), out int idPaciente))
    {
        Console.WriteLine("El Id debe ser un número válido.");
        return;
    }

    var paciente = pacienteService.BuscarPorId(idPaciente);
    if (paciente == null)
    {
        Console.WriteLine("No existe un paciente con ese Id. Registra primero al paciente.");
        return;
    }

    Console.Write("Nombre de la mascota: ");
    string nombre = Console.ReadLine() ?? "";
    if (string.IsNullOrWhiteSpace(nombre))
    {
        Console.WriteLine("El nombre no puede estar vacío.");
        return;
    }

    Console.Write("Especie (ej. Perro, Gato): ");
    string especie = Console.ReadLine() ?? "";

    Console.Write("Raza: ");
    string raza = Console.ReadLine() ?? "";

    Console.Write("Edad de la mascota: ");
    if (!int.TryParse(Console.ReadLine(), out int edad) || edad < 0)
    {
        Console.WriteLine("La edad debe ser un número válido.");
        return;
    }

    var mascota = mascotaService.Registrar(paciente, nombre, especie, raza, edad);

    Console.WriteLine();
    mascota.Registrar(); // IRegistrable: confirma el registro
    mascota.MostrarInformacion();
}

static void VerMascotasDePaciente(PacienteService pacienteService)
{
    Console.Write("\nId del paciente: ");
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("El Id debe ser un número válido.");
        return;
    }

    var paciente = pacienteService.BuscarPorId(id);
    if (paciente == null)
    {
        Console.WriteLine("No existe un paciente con ese Id.");
        return;
    }

    if (paciente.Mascotas.Count == 0)
    {
        Console.WriteLine($"\n{paciente.Nombre} no tiene mascotas registradas.");
        return;
    }

    Console.WriteLine($"\n--- Mascotas de {paciente.Nombre} ---");
    foreach (var mascota in paciente.Mascotas)
    {
        mascota.MostrarInformacion();
    }
}

static void MostrarConsultasLinq(PacienteService pacienteService, MascotaService mascotaService)
{
    var pacientes = pacienteService.ListarTodos();
    var mascotas = mascotaService.ListarTodas();

    if (pacientes.Count == 0)
    {
        Console.WriteLine("\nNo hay pacientes registrados todavía.");
        return;
    }

    Console.WriteLine("\n--- Consultas con LINQ ---");

    // Where con sintaxis de método: pacientes mayores de edad
    var mayoresDeEdad = pacientes.Where(p => p.Edad >= 18).ToList();
    Console.WriteLine($"Pacientes mayores de edad: {mayoresDeEdad.Count}");

    // Sintaxis de consulta: nombres de todos los pacientes
    var nombres = from p in pacientes
                  select p.Nombre;
    Console.WriteLine("Nombres de pacientes: " + string.Join(", ", nombres));

    // Select para transformar datos: nombres en mayúsculas
    var nombresMayus = pacientes.Select(p => p.Nombre.ToUpper()).ToList();
    Console.WriteLine("Nombres en mayúsculas: " + string.Join(", ", nombresMayus));

    // OrderBy para ordenar alfabéticamente
    var ordenAlfabetico = pacientes.OrderBy(p => p.Nombre).Select(p => p.Nombre);
    Console.WriteLine("Pacientes en orden alfabético: " + string.Join(", ", ordenAlfabetico));

    // OrderBy / OrderByDescending + First para encontrar extremos
    var masJoven = pacientes.OrderBy(p => p.Edad).First();
    var masGrande = pacientes.OrderByDescending(p => p.Edad).First();
    Console.WriteLine($"Paciente más joven: {masJoven.Nombre} ({masJoven.Edad} años)");
    Console.WriteLine($"Paciente de mayor edad: {masGrande.Nombre} ({masGrande.Edad} años)");

    if (mascotas.Count == 0)
    {
        Console.WriteLine("\nTodavía no hay mascotas registradas para las consultas de mascotas.");
        return;
    }

    // GroupBy para agrupar mascotas por especie
    var porEspecie = mascotas.GroupBy(m => m.Especie);
    Console.WriteLine("\nMascotas agrupadas por especie:");
    foreach (var grupo in porEspecie)
    {
        Console.WriteLine($"  {grupo.Key}: {grupo.Count()}");
    }

    // Any y All
    bool hayGatos = mascotas.Any(m => m.Especie.Equals("Gato", StringComparison.OrdinalIgnoreCase));
    bool todasTienenRaza = mascotas.All(m => !string.IsNullOrWhiteSpace(m.Raza));
    Console.WriteLine($"\n¿Hay algún gato registrado?: {hayGatos}");
    Console.WriteLine($"¿Todas las mascotas tienen raza registrada?: {todasTienenRaza}");

    // Count con condición
    int cantidadPerros = mascotas.Count(m => m.Especie.Equals("Perro", StringComparison.OrdinalIgnoreCase));
    Console.WriteLine($"Cantidad de perros: {cantidadPerros}");

    // Consulta encadenada: filtrar → ordenar → seleccionar
    var mascotasJovenes = mascotas
        .Where(m => m.Edad <= 2)
        .OrderBy(m => m.Nombre)
        .Select(m => m.Nombre)
        .ToList();

    string listaJovenes = mascotasJovenes.Count > 0 ? string.Join(", ", mascotasJovenes) : "ninguna";
    Console.WriteLine("Mascotas de 2 años o menos (ordenadas por nombre): " + listaJovenes);
}

static void DemostrarPolimorfismo(MascotaService mascotaService)
{
    var mascotas = mascotaService.ListarTodas();

    if (mascotas.Count == 0)
    {
        Console.WriteLine("\nNo hay mascotas registradas para demostrar el polimorfismo.");
        return;
    }

    Console.WriteLine("\n--- Cada mascota emite su propio sonido ---");

    // Aunque guardamos las mascotas en una lista de tipo Animal (la clase base),
    // al llamar EmitirSonido() se ejecuta la versión sobrescrita según el tipo
    // real de cada objeto (Mascota). Eso es polimorfismo.
    List<Animal> animales = mascotas.Cast<Animal>().ToList();
    foreach (Animal animal in animales)
    {
        animal.EmitirSonido();
    }
}

static void RegistrarAtencion(MascotaService mascotaService)
{
    var mascotas = mascotaService.ListarTodas();

    if (mascotas.Count == 0)
    {
        Console.WriteLine("\nNo hay mascotas registradas todavía.");
        return;
    }

    Console.Write("\nId de la mascota a atender: ");
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("El Id debe ser un número válido.");
        return;
    }

    var mascota = mascotas.FirstOrDefault(m => m.Id == id);
    if (mascota == null)
    {
        Console.WriteLine("No existe una mascota con ese Id.");
        return;
    }

    Console.WriteLine("\n1. Consulta general");
    Console.WriteLine("2. Vacunación");
    Console.Write("Tipo de servicio: ");
    string tipo = Console.ReadLine() ?? "";

    // ConsultaGeneral y Vacunacion son ServicioVeterinario (clase abstracta) y
    // cada una implementa Atender() a su manera.
    ServicioVeterinario servicio;

    if (tipo == "1")
    {
        Console.Write("Motivo de la consulta: ");
        string motivo = Console.ReadLine() ?? "";
        servicio = new ConsultaGeneral(motivo);
    }
    else if (tipo == "2")
    {
        Console.Write("Vacuna a aplicar: ");
        string vacuna = Console.ReadLine() ?? "";
        servicio = new Vacunacion(vacuna);
    }
    else
    {
        Console.WriteLine("Opción no válida.");
        return;
    }

    Console.WriteLine();
    servicio.Atender(mascota);
}
