using System;
using System.Collections.Generic;
using System.Linq;
using ClinicaVeterinariaLINQ.Models;

namespace ClinicaVeterinariaLINQ
{
    class Program
    {
        static void Main(string[] args)
        {
          
            // Lista que almacena los pacientes (y su mascota embebida)
            List<Paciente> pacientes = new List<Paciente>
            {
                new Paciente { Id = 1, Nombre = "Laura Pérez",   Edad = 28, Sintoma = "Chequeo general", Telefono = "3001112233",
                    Mascota = new Mascota { Nombre = "Rocky",  Especie = "Perro", Raza = "Labrador",     Edad = 3 } },

                new Paciente { Id = 2, Nombre = "Carlos Mena",   Edad = 45, Sintoma = "Vómito",           Telefono = "3002223344",
                    Mascota = new Mascota { Nombre = "Michi",  Especie = "Gato",  Raza = "Criollo",       Edad = 2 } },

                new Paciente { Id = 3, Nombre = "Ana Torres",    Edad = 34, Sintoma = "Vacunación",       Telefono = "3003334455",
                    Mascota = new Mascota { Nombre = "Firulais",Especie = "Perro", Raza = "",              Edad = 5 } }, // sin raza definida

                new Paciente { Id = 4, Nombre = "Pedro Gómez",   Edad = 22, Sintoma = "Herida en la pata", Telefono = "3004445566",
                    Mascota = new Mascota { Nombre = "Kiwi",   Especie = "Ave",   Raza = "Periquito",     Edad = 1 } },

                new Paciente { Id = 5, Nombre = "Marta Ruiz",    Edad = 51, Sintoma = "Control anual",     Telefono = "3005556677",
                    Mascota = new Mascota { Nombre = "Toby",   Especie = "Perro", Raza = "Criollo",       Edad = 7 } },

                new Paciente { Id = 6, Nombre = "Jorge Salas",   Edad = 19, Sintoma = "Alergia cutánea",   Telefono = "3006667788",
                    Mascota = new Mascota { Nombre = "Nube",   Especie = "Gato",  Raza = "Persa",         Edad = 4 } },
            };


            // Agregar un nuevo paciente a la lista
            pacientes.Add(new Paciente
            {
                Id = 7,
                Nombre = "Sofía Vidal",
                Edad = 30,
                Sintoma = "Revisión dental",
                Telefono = "3007778899",
                Mascota = new Mascota { Nombre = "Max", Especie = "Perro", Raza = "Poodle", Edad = 6 }
            });
            Console.WriteLine("Paciente agregado: Sofía Vidal (Id 7)");
            Paciente pacienteAModificar = pacientes.First(p => p.Id == 2);
            pacienteAModificar.Sintoma = "Vómito - en tratamiento";
            Console.WriteLine("Paciente Id 2 actualizado: nuevo síntoma -> " + pacienteAModificar.Sintoma);

            // Eliminar un elemento de la lista (removemos al paciente Id 4)
            Paciente pacienteAEliminar = pacientes.First(p => p.Id == 4);
            pacientes.Remove(pacienteAEliminar);
            Console.WriteLine("Paciente Id 4 eliminado de la lista\n");

            // Diccionario para acceso rápido por Id de paciente
            Dictionary<int, Paciente> pacientesPorId = pacientes.ToDictionary(p => p.Id, p => p);

            Console.WriteLine("Acceso rápido con Dictionary (buscar Id=5):");
            if (pacientesPorId.TryGetValue(5, out Paciente pacienteEncontrado))
            {
                Console.WriteLine(pacienteEncontrado);
            }

            // --- Where: filtrar por especie de mascota (sintaxis de método) ---
            var perrosMetodo = pacientes.Where(p => p.Mascota.Especie == "Perro").ToList();
            Console.ForegroundColor =
            Console.WriteLine("Pacientes con perro (sintaxis de método):");
            foreach (var p in perrosMetodo) Console.WriteLine(" - " + p.Nombre);

            // --- Where: filtrar por especie de mascota (sintaxis de consulta) ---
            var perrosConsulta = from p in pacientes
                                  where p.Mascota.Especie == "Perro"
                                  select p;
            Console.WriteLine("\nPacientes con perro (sintaxis de consulta):");
            foreach (var p in perrosConsulta) Console.WriteLine(" - " + p.Nombre);

            // --- Where: filtrar por edad (mayores de 30 años) ---
            var mayoresDe30 = pacientes.Where(p => p.Edad > 30);
            Console.WriteLine("\nPacientes mayores de 30 años:");
            foreach (var p in mayoresDe30) Console.WriteLine(" - " + p.Nombre + " (" + p.Edad + ")");

            // --- Select: proyectar solo los nombres de los pacientes ---
            var nombresPacientes = pacientes.Select(p => p.Nombre).ToList();
            Console.WriteLine("\nSolo nombres (Select): " + string.Join(", ", nombresPacientes));

            // --- OrderBy / OrderByDescending ---
            var ordenPorNombre = pacientes.OrderBy(p => p.Nombre);
            Console.WriteLine("\nPacientes ordenados por nombre (ascendente):");
            foreach (var p in ordenPorNombre) Console.WriteLine(" - " + p.Nombre);

            var ordenPorEdadDesc = pacientes.OrderByDescending(p => p.Edad);
            Console.WriteLine("\nPacientes ordenados por edad (descendente):");
            foreach (var p in ordenPorEdadDesc) Console.WriteLine(" - " + p.Nombre + " (" + p.Edad + ")");

            // --- GroupBy: agrupar pacientes por especie de mascota ---
            var agrupadosPorEspecie = pacientes.GroupBy(p => p.Mascota.Especie);
            Console.WriteLine("\nPacientes agrupados por especie de mascota:");
            foreach (var grupo in agrupadosPorEspecie)
            {
                Console.WriteLine($" Especie: {grupo.Key}");
                foreach (var p in grupo) Console.WriteLine("   * " + p.Nombre);
            }

            // --- First, FirstOrDefault, Any, All, Count ---
            var primerGato = pacientes.First(p => p.Mascota.Especie == "Gato");
            Console.WriteLine("\nPrimer paciente con gato (First): " + primerGato.Nombre);

            var primerAve = pacientes.FirstOrDefault(p => p.Mascota.Especie == "Ave");
            Console.WriteLine("Primer paciente con ave (FirstOrDefault): " +
                (primerAve != null ? primerAve.Nombre : "No hay ninguno"));

            bool hayMenoresDe20 = pacientes.Any(p => p.Edad < 20);
            Console.WriteLine("¿Hay pacientes menores de 20 años? (Any): " + hayMenoresDe20);

            bool todosTienenTelefono = pacientes.All(p => !string.IsNullOrEmpty(p.Telefono));
            Console.WriteLine("¿Todos los pacientes tienen teléfono? (All): " + todosTienenTelefono);

            int totalPacientes = pacientes.Count();
            Console.WriteLine("Cantidad total de pacientes (Count): " + totalPacientes)

            // Pacientes con perro, ordenados por edad, mostrando solo nombre y teléfono
            var perrosOrdenadosPorEdad = pacientes
                .Where(p => p.Mascota.Especie == "Perro")     // filtro
                .OrderBy(p => p.Edad)                          // orden
                .Select(p => new { p.Nombre, p.Telefono });     // proyección

            Console.WriteLine("Dueños de perros ordenados por edad (nombre y teléfono):");
            foreach (var item in perrosOrdenadosPorEdad)
                Console.WriteLine($" - {item.Nombre} | Tel: {item.Telefono}");


            // 1) Paciente más joven y de mayor edad
            var pacienteMasJoven = pacientes.OrderBy(p => p.Edad).First();
            var pacienteMayorEdad = pacientes.OrderByDescending(p => p.Edad).First();
            Console.WriteLine("Paciente más joven: " + pacienteMasJoven.Nombre + " (" + pacienteMasJoven.Edad + " años)");
            Console.WriteLine("Paciente de mayor edad: " + pacienteMayorEdad.Nombre + " (" + pacienteMayorEdad.Edad + " años)");

            // 2) Cantidad de mascotas por especie
            var conteoPorEspecie = pacientes
                .GroupBy(p => p.Mascota.Especie)
                .Select(g => new { Especie = g.Key, Cantidad = g.Count() });

            Console.WriteLine("\nCantidad de mascotas por especie:");
            foreach (var item in conteoPorEspecie)
                Console.WriteLine($" - {item.Especie}: {item.Cantidad}");

            // 3) ¿Existe al menos un paciente con mascota sin raza definida?
            bool existeSinRaza = pacientes.Any(p => string.IsNullOrEmpty(p.Mascota.Raza));
            Console.WriteLine("\n¿Existe alguna mascota sin raza definida?: " + existeSinRaza);

            // 4) Nombres de pacientes en mayúsculas, ordenados alfabéticamente
            var nombresMayusculasOrdenados = pacientes
                .Select(p => p.Nombre.ToUpper())
                .OrderBy(nombre => nombre)
                .ToList();

            Console.WriteLine("\nNombres en mayúsculas y ordenados alfabéticamente:");
            foreach (var nombre in nombresMayusculasOrdenados)
                Console.WriteLine(" - " + nombre);

            Console.WriteLine("\nFin del programa.");
        }
    }
}
