using CitasApp.Domain.Models;
using CitasApp.Domain.Interfaces;

namespace CitasApp.Infrastructure.Repositories
{
    public class MedicoRepository : JsonRepository<Medico>, IMedicoRepository
    {
        public MedicoRepository(string dataPath)
            : base(dataPath, "medicos.json")
        {
        }
    }
}
