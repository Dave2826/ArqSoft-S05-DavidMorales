using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using CitasApp.Infrastructure.Data;

namespace CitasApp.Infrastructure.Repositories
{
    public class MedicoRepository : IMedicoRepository
    {
        private readonly CitasAppDbContext _context;

        public MedicoRepository(CitasAppDbContext context)
        {
            _context = context;
        }

        public List<Medico> ObtenerTodos()
        {
            return _context.Medicos.ToList();
        }

        public Medico? ObtenerPorId(int id)
        {
            return _context.Medicos.Find(id);
        }

        public void Guardar(List<Medico> medicos)
        {
            _context.Medicos.AddRange(medicos);
            _context.SaveChanges();
        }

        public void Actualizar(Medico medicoActualizado)
        {
            _context.Medicos.Update(medicoActualizado);
            _context.SaveChanges();
        }
    }
}
