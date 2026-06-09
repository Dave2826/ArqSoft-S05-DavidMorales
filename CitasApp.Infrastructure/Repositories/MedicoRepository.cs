using System.Text.Json;
using System.Collections.Generic;
using CitasApp.Domain.Models;
using CitasApp.Domain.Interfaces;

namespace CitasApp.Infrastructure.Repositories
{
    public class MedicoRepository : IMedicoRepository
    {
        private readonly string archivoJson;
        private readonly JsonSerializerOptions opcionesJson = new()
        {
            WriteIndented = true
        };

        public MedicoRepository(string dataPath)
        {
            archivoJson = Path.Combine(dataPath, "medicos.json");
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
