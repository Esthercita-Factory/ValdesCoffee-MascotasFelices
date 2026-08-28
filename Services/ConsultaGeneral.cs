using ClinicaPatitasFelices.Console.Models;

namespace ClinicaPatitasFelices.Console.Services
{
    public class ConsultaGeneral : ServicioVeterinario
    {
        public string Motivo { get; set; }
        public string Diagnostico { get; set; } = string.Empty;

        public ConsultaGeneral(string motivo)
        {
            Motivo = motivo;
        }

        // Implementación de IAtendible (heredada vía ServicioVeterinario).
        public override void Atender(Mascota mascota)
        {
            Diagnostico = $"Consulta general realizada a {mascota.Nombre} por: {Motivo}";
        }
    }
}
