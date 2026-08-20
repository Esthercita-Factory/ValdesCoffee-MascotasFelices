namespace ClinicaPatitasFelices.Interfaces
{
    public interface IRegistrable
    {
        // Obliga a las clases a implementar un ID tipo UUID
        Guid Id { get; } 
        
        // Firma del método sin cuerpo. La clase que herede decidirá cómo registrarse.
        void Registrar(); 
    }
}