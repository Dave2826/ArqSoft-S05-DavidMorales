using System.Collections.Generic;

namespace CitasApp.Domain.Interfaces
{
    public interface IRepository<T> where T : IEntity
    {
        List<T> ObtenerTodos();
        T? ObtenerPorId(int id);
        void Guardar(List<T> entidades);
        void Actualizar(T entidadActualizada);
    }
}
