using System.Text.Json;
using Citas.App.Models;

namespace Citas.App.Repositories
{
    public class MedicoRepository
    {
        private readonly string archivoJson;
        private readonly JsonSerializerOptions opcionesJson = new()
        {
            WriteIndented = true
        };

        public MedicoRepository(IWebHostEnvironment environment)
        {
            archivoJson = Path.Combine(environment.ContentRootPath, "Data", "medicos.json");
        }

        public List<Medico> ObtenerTodos()
        {
            if (!File.Exists(archivoJson))
                return new List<Medico>();

            var contenido = File.ReadAllText(archivoJson);
            return JsonSerializer.Deserialize<List<Medico>>(contenido, opcionesJson) ?? new List<Medico>();
        }

        public Medico? ObtenerPorId(int id)
        {
            return ObtenerTodos().FirstOrDefault(m => m.Id == id);
        }

        public void Guardar(List<Medico> medicos)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(archivoJson)!);
            var contenido = JsonSerializer.Serialize(medicos, opcionesJson);
            File.WriteAllText(archivoJson, contenido);
        }

        public void Actualizar(Medico medicoActualizado)
        {
            var medicos = ObtenerTodos();
            var indice = medicos.FindIndex(m => m.Id == medicoActualizado.Id);

            if (indice >= 0)
            {
                medicos[indice] = medicoActualizado;
                Guardar(medicos);
            }
        }
    }
}
