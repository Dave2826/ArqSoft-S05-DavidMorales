using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using CitasApp.Domain.Interfaces;

namespace CitasApp.Infrastructure.Repositories
{
    public class JsonRepository<T> : IRepository<T> where T : IEntity
    {
        private readonly string archivoJson;
        private readonly JsonSerializerOptions opcionesJson = new()
        {
            WriteIndented = true
        };

        public JsonRepository(string dataPath, string nombreArchivo)
        {
            archivoJson = Path.Combine(dataPath, nombreArchivo);
        }

        public List<T> ObtenerTodos()
        {
            if (!File.Exists(archivoJson))
                return new List<T>();

            var contenido = File.ReadAllText(archivoJson);
            return JsonSerializer.Deserialize<List<T>>(contenido, opcionesJson) ?? new List<T>();
        }

        public T? ObtenerPorId(int id)
        {
            return ObtenerTodos().FirstOrDefault(e => e.Id == id);
        }

        public void Guardar(List<T> entidades)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(archivoJson)!);
            var contenido = JsonSerializer.Serialize(entidades, opcionesJson);
            File.WriteAllText(archivoJson, contenido);
        }

        public void Actualizar(T entidadActualizada)
        {
            var entidades = ObtenerTodos();
            var indice = entidades.FindIndex(e => e.Id == entidadActualizada.Id);

            if (indice >= 0)
            {
                entidades[indice] = entidadActualizada;
                Guardar(entidades);
            }
        }
    }
}
