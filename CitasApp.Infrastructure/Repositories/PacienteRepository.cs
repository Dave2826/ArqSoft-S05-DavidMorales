using CitasApp.Domain.Models;
using CitasApp.Domain.Interfaces;

namespace CitasApp.Infrastructure.Repositories
{
    public class PacienteRepository : JsonRepository<Paciente>, IPacienteRepository
    {
        public PacienteRepository(string dataPath)
            : base(dataPath, "pacientes.json")
        {
        }
    }
}
