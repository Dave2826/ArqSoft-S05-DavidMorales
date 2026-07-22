using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Infrastructure.Repositories
{
    public class MemoriaPacienteRepository : IPacienteRepository
    {
        private readonly List<Paciente> _pacientes =
        [
            new Paciente
            {
                Id = 1,
                Nombre = "Paciente Memoria",
                Apellido = "Demo",
                Email = "memoria@test.com",
                Telefono = "9990000000"
            }
        ];

        public List<Paciente> ObtenerTodos()
        {
            return _pacientes;
        }

        public Paciente? ObtenerPorId(int id)
        {
            return _pacientes.FirstOrDefault(p => p.Id == id);
        }

        public void Guardar(List<Paciente> pacientes)
        {
        }

        public void Actualizar(Paciente pacienteActualizado)
        {
        }
    }
}