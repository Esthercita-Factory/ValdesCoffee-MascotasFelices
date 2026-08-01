namespace ClinicaPatitasFelices.Console;

public class Mascota
{ 
    public int id  ; 
    public string name{ get; set; };
    public byte edad { get; set; };
    public int peso { get; set; };
    public string? sintomas { get; set; };
    public string especie { get; set; };
    public Mascota(int id, string name, byte edad, int peso, string especie);
    {
        Id = id;
        Name = name;
        Edad = edad;
        Peso = peso;
        Sintomas = sintomas;
        Especie = especie;

    }

    public void AgregarMascota()
    {
        Console.WriteLine($"");
    }
