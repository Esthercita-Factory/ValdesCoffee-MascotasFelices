namespace ClinicaPets.Models;

// Esta es la mascota "de verdad" (el animal), separada del Paciente (el dueño).
// Hereda Nombre, Edad y Especie de Animal, y agrega lo propio de una mascota.
public class Mascota : Animal, IRegistrable
{
    public int Id { get; set; }
    public string Raza { get; set; }
    public Paciente? Dueno { get; set; }

    public Mascota(int id, string nombre, string especie, string raza, int edad)
        : base(nombre, edad, especie)
    {
        Id = id;
        Raza = raza;
    }

    // Sobrescribimos EmitirSonido: mismo método, pero cada especie "suena"
    // distinto. Esto es polimorfismo en acción.
    public override void EmitirSonido()
    {
        switch (especie.ToLower())
        {
            case "perro":
                Console.WriteLine($"{Nombre} dice: ¡Guau guau!");
                break;
            case "gato":
                Console.WriteLine($"{Nombre} dice: ¡Miau!");
                break;
            default:
                base.EmitirSonido();
                break;
        }
    }

    public void MostrarInformacion()
    {
        Console.WriteLine($"ID: {Id}");
        Console.WriteLine($"Nombre: {Nombre}");
        Console.WriteLine($"Especie: {Especie}");
        Console.WriteLine($"Raza: {Raza}");
        Console.WriteLine($"Edad: {Edad} años");
        if (Dueno != null)
            Console.WriteLine($"Dueño: {Dueno.Nombre}");
        Console.WriteLine();
    }

    public void Registrar()
    {
        Console.WriteLine($"✓ Mascota registrada: {Nombre} ({Especie})");
    }
}
