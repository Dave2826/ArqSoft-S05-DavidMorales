using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using CitasApp.Infrastructure.Data;

namespace CitasApp.Infrastructure.Repositories
{
    public class CitaRepository : ICitaRepository
    {
        private readonly CitasAppDbContext _context;

        public CitaRepository(CitasAppDbContext context)
        {
            _context = context;
        }

        public List<Cita> ObtenerTodos()
        {
            return _context.Citas.ToList();
        }

        public Cita? ObtenerPorId(int id)
        {
            return _context.Citas.Find(id);
        }

        public void Guardar(List<Cita> citas)
        {
            _context.Citas.AddRange(citas);
            _context.SaveChanges();
        }

        public void Actualizar(Cita citaActualizada)
        {
            _context.Citas.Update(citaActualizada);
            _context.SaveChanges();
        }
    }
}
