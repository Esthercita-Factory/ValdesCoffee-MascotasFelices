namespace ClinicaPets.Models;

// Clase base con la información y el comportamiento que comparten todos los
// animales de la clínica. Mascota hereda de aquí.
public class Animal
{
    public string Nombre { get; set; }
    public int Edad { get; set; }

    // La especie se guarda en un campo protected: solo Animal y sus clases hijas
    // (como Mascota) pueden usarla directamente. Hacia afuera del proyecto se
    // expone de forma controlada a través de la propiedad pública Especie.
    protected string especie;

    public string Especie => especie;

    public Animal(string nombre, int edad, string especie)
    {
        Nombre = nombre;
        Edad = edad;
        this.especie = especie;
    }

    // Método virtual: las clases hijas pueden sobrescribirlo para dar un
    // comportamiento distinto según el tipo de animal (polimorfismo).
    public virtual void EmitirSonido()
    {
        Console.WriteLine($"{Nombre} hace un sonido de animal.");
    }
}
