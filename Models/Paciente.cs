namespace ClinicaPets.Models;

// Antes esta clase se llamaba Mascota, pero en realidad representaba a la persona
// que trae a su mascota a la clínica (tenía Id, Nombre, Edad y Sintomas). La
// renombramos a Paciente para que coincida con lo que pide la historia M5.3S1,
// y en M5.3S3 le agregamos Dirección, Teléfono y validaciones en los setters.
public class Paciente : IRegistrable
{
    // Guardamos nombre y edad en campos privados para poder validar antes de
    // asignarlos. Así nadie desde afuera puede dejar el paciente en un estado
    // inválido (por ejemplo, con una edad negativa).
    private string nombre = "";
    private int edad;

    public int Id { get; set; }

    public string Nombre
    {
        get => nombre;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El nombre no puede estar vacío.");
            nombre = value;
        }
    }

    public int Edad
    {
        get => edad;
        set
        {
            if (value < 0)
                throw new ArgumentException("La edad no puede ser negativa.");
            edad = value;
        }
    }

    public string? Sintoma { get; set; }
    public string Direccion { get; set; } = "";
    public string Telefono { get; set; } = "";

    // Lista de mascotas del paciente. El set es privado para que solo se pueda
    // agregar mascotas a través de AgregarMascota (así se mantiene la relación
    // dueño-mascota siempre consistente).
    public List<Mascota> Mascotas { get; private set; } = new List<Mascota>();

    public Paciente(int id, string nombre, int edad, string? sintoma = null)
    {
        Id = id;
        Nombre = nombre;
        Edad = edad;
        Sintoma = sintoma;
    }

    public void AgregarMascota(Mascota mascota)
    {
        mascota.Dueno = this;
        Mascotas.Add(mascota);
    }

    public void MostrarInformacion()
    {
        Console.WriteLine($"ID: {Id}");
        Console.WriteLine($"Nombre: {Nombre}");
        Console.WriteLine($"Edad: {Edad} años");
        if (!string.IsNullOrEmpty(Sintoma))
            Console.WriteLine($"Síntoma: {Sintoma}");
        if (!string.IsNullOrEmpty(Direccion))
            Console.WriteLine($"Dirección: {Direccion}");
        if (!string.IsNullOrEmpty(Telefono))
            Console.WriteLine($"Teléfono: {Telefono}");
        Console.WriteLine();
    }

    public void Registrar()
    {
        Console.WriteLine($"✓ Paciente registrado: {Nombre} (ID {Id})");
    }
}
