using System.Text.Json;
using System.Collections.Generic;
using CitasApp.Domain.Models;
using CitasApp.Domain.Interfaces;

namespace CitasApp.Infrastructure.Repositories
{
    // Implementation that does not depend on ASP.NET types.
    public class PacienteRepository : IPacienteRepository
    {
        private readonly string archivoJson;
        private readonly JsonSerializerOptions opcionesJson = new()
        {
            WriteIndented = true
        };

        // Accept a data path (directory) where Data files are located.
        public PacienteRepository(string dataPath)
        {
            archivoJson = Path.Combine(dataPath, "pacientes.json");
        }

        public List<Paciente> ObtenerTodos()
        {
            if (!File.Exists(archivoJson))
                return new List<Paciente>();

            var contenido = File.ReadAllText(archivoJson);
            return JsonSerializer.Deserialize<List<Paciente>>(contenido, opcionesJson) ?? new List<Paciente>();
        }

        public Paciente? ObtenerPorId(int id)
        {
            return ObtenerTodos().FirstOrDefault(p => p.Id == id);
        }

        public void Guardar(List<Paciente> pacientes)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(archivoJson)!);
            var contenido = JsonSerializer.Serialize(pacientes, opcionesJson);
            File.WriteAllText(archivoJson, contenido);
        }

        public void Actualizar(Paciente pacienteActualizado)
        {
            var pacientes = ObtenerTodos();
            var indice = pacientes.FindIndex(p => p.Id == pacienteActualizado.Id);

            if (indice >= 0)
            {
                pacientes[indice] = pacienteActualizado;
                Guardar(pacientes);
            }
        }
    }
}
