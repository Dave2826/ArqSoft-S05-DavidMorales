using System.Collections.Generic;
using CitasApp.Domain.Models;

namespace CitasApp.Domain.Interfaces
{
    public interface IPacienteRepository
    {
        List<Paciente> ObtenerTodos();
        Paciente? ObtenerPorId(int id);
        void Guardar(List<Paciente> pacientes);
        void Actualizar(Paciente pacienteActualizado);
    }
}
