using System.Text.Json;
using Citas.App.Models;

namespace Citas.App.Repositories
{
    public class CitaRepository
    {
        private readonly string archivoJson;
        private readonly JsonSerializerOptions opcionesJson = new()
        {
            WriteIndented = true
        };

        public CitaRepository(IWebHostEnvironment environment)
        {
            archivoJson = Path.Combine(environment.ContentRootPath, "Data", "citas.json");
        }

        public List<Cita> ObtenerTodos()
        {
            if (!File.Exists(archivoJson))
                return new List<Cita>();

            var contenido = File.ReadAllText(archivoJson);
            return JsonSerializer.Deserialize<List<Cita>>(contenido, opcionesJson) ?? new List<Cita>();
        }

        public Cita? ObtenerPorId(int id)
        {
            return ObtenerTodos().FirstOrDefault(c => c.Id == id);
        }

        public void Guardar(List<Cita> citas)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(archivoJson)!);
            var contenido = JsonSerializer.Serialize(citas, opcionesJson);
            File.WriteAllText(archivoJson, contenido);
        }

        public void Actualizar(Cita citaActualizada)
        {
            var citas = ObtenerTodos();
            var indice = citas.FindIndex(c => c.Id == citaActualizada.Id);

            if (indice >= 0)
            {
                citas[indice] = citaActualizada;
                Guardar(citas);
            }
        }
    }
}
