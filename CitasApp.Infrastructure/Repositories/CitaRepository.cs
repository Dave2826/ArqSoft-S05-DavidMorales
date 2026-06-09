using System.Text.Json;
using System.Collections.Generic;
using CitasApp.Domain.Models;
using CitasApp.Domain.Interfaces;

namespace CitasApp.Infrastructure.Repositories
{
    public class CitaRepository : ICitaRepository
    {
        private readonly string archivoJson;
        private readonly JsonSerializerOptions opcionesJson = new()
        {
            WriteIndented = true
        };

        public CitaRepository(string dataPath)
        {
            archivoJson = Path.Combine(dataPath, "citas.json");
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
