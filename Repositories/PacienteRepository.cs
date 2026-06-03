using System.Text.Json;
using Citas.App.Models;

namespace Citas.App.Repositories
{
    public class PacienteRepository
    {
        private readonly string archivoJson;
        private readonly JsonSerializerOptions opcionesJson = new()
        {
            WriteIndented = true
        };

        public PacienteRepository(IWebHostEnvironment environment)
        {
            archivoJson = Path.Combine(environment.ContentRootPath, "Data", "pacientes.json");
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
