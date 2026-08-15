namespace ClinicaPets.Models;

// Clase abstracta: no tiene sentido crear un "ServicioVeterinario" suelto,
// siempre es una consulta general, una vacunación, etc. Atender() queda
// como abstracto para que cada tipo de servicio decida cómo se hace.
public abstract class ServicioVeterinario
{
    public DateTime Fecha { get; set; } = DateTime.Now;

    public abstract void Atender(Mascota mascota);
}
