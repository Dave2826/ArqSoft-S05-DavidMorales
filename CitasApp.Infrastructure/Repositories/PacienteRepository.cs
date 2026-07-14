using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using CitasApp.Infrastructure.Data;

namespace CitasApp.Infrastructure.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly CitasAppDbContext _context;

        public PacienteRepository(CitasAppDbContext context)
        {
            _context = context;
        }

        public List<Paciente> ObtenerTodos()
        {
            return _context.Pacientes.ToList();
        }

        public Paciente? ObtenerPorId(int id)
        {
            return _context.Pacientes.Find(id);
        }

        public void Guardar(List<Paciente> pacientes)
        {
            _context.Pacientes.AddRange(pacientes);
            _context.SaveChanges();
        }

        public void Actualizar(Paciente pacienteActualizado)
        {
            _context.Pacientes.Update(pacienteActualizado);
            _context.SaveChanges();
        }
    }
}
