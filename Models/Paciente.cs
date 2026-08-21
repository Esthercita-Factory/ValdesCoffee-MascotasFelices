using ClinicaPatitasFelices.Console.Interfaces;

namespace ClinicaPatitasFelices.Console.Models
{
    // Paciente = el tutor/dueño que registra y da seguimiento a sus mascotas.
    // Implementa dos interfaces a la vez: se puede Registrar() (IRegistrable)
    // y además puede recibir avisos EnviarNotificacion() (INotificable).
    public class Paciente : IRegistrable, INotificable
    {
        public Guid Id { get; private set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }

        private readonly List<Mascota> _mascotas = new();
        public IReadOnlyList<Mascota> Mascotas => _mascotas;

        public Paciente(string nombre, string telefono, string email)
        {
            Id = Guid.NewGuid();
            Nombre = nombre;
            Telefono = telefono;
            Email = email;
        }

        public void AgregarMascota(Mascota mascota)
        {
            mascota.Dueno = this;
            _mascotas.Add(mascota);
        }

        // Implementación de IRegistrable
        public void Registrar()
        {
            // Lógica de dominio (ej. marcar como "Activo" en el sistema).
        }

        // Implementación de INotificable: simula el envío de un recordatorio de cita.
        public void EnviarNotificacion()
        {
            System.Console.WriteLine(
                $"[Notificación] Hola {Nombre}, te recordamos tu próxima cita en la Clínica Patitas Felices.");
        }

        public string ObtenerInformacion()
        {
            return $"ID: {Id}\nNombre: {Nombre}\nTeléfono: {Telefono}\nEmail: {Email}\nMascotas registradas: {_mascotas.Count}";
        }
    }
}
