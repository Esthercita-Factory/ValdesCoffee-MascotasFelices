namespace ClinicaPets.Models;

public class ConsultaGeneral : ServicioVeterinario
{
    public string Motivo { get; set; }

    public ConsultaGeneral(string motivo)
    {
        Motivo = motivo;
    }

    public override void Atender(Mascota mascota)
    {
        Console.WriteLine($"Consulta general para {mascota.Nombre}: se revisan signos vitales y estado general.");
        Console.WriteLine($"Motivo de la consulta: {Motivo}");
    }
}
