using System.Collections.Generic;
using CitasApp.Domain.Models;

namespace CitasApp.Domain.Interfaces
{
    public interface IMedicoRepository
    {
        List<Medico> ObtenerTodos();
        Medico? ObtenerPorId(int id);
        void Guardar(List<Medico> medicos);
        void Actualizar(Medico medicoActualizado);
    }
}
