namespace ClinicaPets.Models;

// Cualquier entidad que pueda "registrarse" en el sistema implementa esto.
// Paciente y Mascota la implementan cada una a su manera.
public interface IRegistrable
{
    void Registrar();
}
