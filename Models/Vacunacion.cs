namespace ClinicaPets.Models;

public class Vacunacion : ServicioVeterinario
{
    public string VacunaAplicada { get; set; }

    public Vacunacion(string vacunaAplicada)
    {
        VacunaAplicada = vacunaAplicada;
    }

    public override void Atender(Mascota mascota)
    {
        Console.WriteLine($"Vacunación para {mascota.Nombre}: se aplica la vacuna {VacunaAplicada}.");
    }
}
