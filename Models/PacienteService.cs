using ClinicaPets.Models;

namespace ClinicaPets.Services;

// Se encarga de todo lo relacionado con los pacientes: registrar, listar, buscar,
// modificar y eliminar. Así el Program.cs solo se ocupa del menú y no de la lógica.
public class PacienteService
{
    // Guardamos los pacientes en un diccionario usando el Id como clave. Esto nos
    // permite buscar, modificar y eliminar directamente por Id sin recorrer toda
    // la colección cada vez.
    private Dictionary<int, Paciente> pacientes = new Dictionary<int, Paciente>();
    private int siguienteId = 1;

    public Paciente Registrar(string nombre, int edad, string? sintoma, string direccion = "", string telefono = "")
    {
        var paciente = new Paciente(siguienteId, nombre, edad, sintoma)
        {
            Direccion = direccion,
            Telefono = telefono
        };
        pacientes.Add(paciente.Id, paciente);
        siguienteId++;
        return paciente;
    }

    // Para listar o recorrer con LINQ seguimos trabajando con una lista normal.
    public List<Paciente> ListarTodos()
    {
        return pacientes.Values.ToList();
    }

    public Paciente? BuscarPorId(int id)
    {
        pacientes.TryGetValue(id, out var paciente);
        return paciente;
    }

    public Paciente? BuscarPorNombre(string nombre)
    {
        return pacientes.Values.FirstOrDefault(p => p.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
    }

    public bool ModificarEdad(int id, int nuevaEdad)
    {
        if (!pacientes.ContainsKey(id))
            return false;

        pacientes[id].Edad = nuevaEdad;
        return true;
    }

    public bool Eliminar(int id)
    {
        return pacientes.Remove(id);
    }
}
