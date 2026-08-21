namespace ClinicaPatitasFelices.Console.Interfaces
{
    public interface IRegistrable
    {
        // Obliga a las clases a exponer un identificador tipo UUID
        Guid Id { get; }

        // Solo la firma. La clase que implemente decide cómo se registra.
        void Registrar();
    }
}
