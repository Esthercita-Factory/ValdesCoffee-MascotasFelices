namespace ClinicaPatitasFelices.Console.Interfaces
{
    public interface INotificable
    {
        // Solo la firma. Cada clase decide qué significa "notificar" para ella.
        void EnviarNotificacion();
    }
}
