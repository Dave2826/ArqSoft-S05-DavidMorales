using CitasApp.Infrastructure.Repositories;
using Citas.App.Models;

namespace Citas.App.Repositories
{
    // Adapter kept in Web project to preserve existing DI registrations and usage.
    public class PacienteRepository
    {
        private readonly CitasApp.Infrastructure.Repositories.PacienteRepository impl;

        public PacienteRepository(IWebHostEnvironment environment)
        {
            var dataPath = Path.Combine(environment.ContentRootPath, "Data");
            impl = new CitasApp.Infrastructure.Repositories.PacienteRepository(dataPath);
        }

        public List<Paciente> ObtenerTodos() => impl.ObtenerTodos();

        public Paciente? ObtenerPorId(int id) => impl.ObtenerPorId(id);

        public void Guardar(List<Paciente> pacientes) => impl.Guardar(pacientes);

        public void Actualizar(Paciente pacienteActualizado) => impl.Actualizar(pacienteActualizado);
    }
}
