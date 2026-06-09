using System.Collections.Generic;
using CitasApp.Domain.Models;

namespace CitasApp.Domain.Interfaces
{
    public interface ICitaRepository
    {
        List<Cita> ObtenerTodos();
        Cita? ObtenerPorId(int id);
        void Guardar(List<Cita> citas);
        void Actualizar(Cita citaActualizada);
    }
}
