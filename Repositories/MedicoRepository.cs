using CitasApp.Infrastructure.Repositories;
using Citas.App.Models;

namespace Citas.App.Repositories
{
    public class MedicoRepository
    {
        private readonly CitasApp.Infrastructure.Repositories.MedicoRepository impl;

        public MedicoRepository(IWebHostEnvironment environment)
        {
            var dataPath = Path.Combine(environment.ContentRootPath, "Data");
            impl = new CitasApp.Infrastructure.Repositories.MedicoRepository(dataPath);
        }

        public List<Medico> ObtenerTodos() => impl.ObtenerTodos();

        public Medico? ObtenerPorId(int id) => impl.ObtenerPorId(id);

        public void Guardar(List<Medico> medicos) => impl.Guardar(medicos);

        public void Actualizar(Medico medicoActualizado) => impl.Actualizar(medicoActualizado);
    }
}
