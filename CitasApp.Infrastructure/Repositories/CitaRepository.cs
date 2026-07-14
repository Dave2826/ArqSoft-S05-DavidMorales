using CitasApp.Domain.Models;
using CitasApp.Domain.Interfaces;

namespace CitasApp.Infrastructure.Repositories
{
    public class CitaRepository : JsonRepository<Cita>, ICitaRepository
    {
        public CitaRepository(string dataPath)
            : base(dataPath, "citas.json")
        {
        }
    }
}
