using CitasApp.Infrastructure.Repositories;
using Citas.App.Models;

namespace Citas.App.Repositories
{
    public class CitaRepository
    {
        private readonly CitasApp.Infrastructure.Repositories.CitaRepository impl;

        public CitaRepository(IWebHostEnvironment environment)
        {
            var dataPath = Path.Combine(environment.ContentRootPath, "Data");
            impl = new CitasApp.Infrastructure.Repositories.CitaRepository(dataPath);
        }

        public List<Cita> ObtenerTodos() => impl.ObtenerTodos();

        public Cita? ObtenerPorId(int id) => impl.ObtenerPorId(id);

        public void Guardar(List<Cita> citas) => impl.Guardar(citas);

        public void Actualizar(Cita citaActualizada) => impl.Actualizar(citaActualizada);
    }
}
