using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Infrastructure.Repositories
{
    public class LoggingCitaRepository : ICitaRepository
    {
        private readonly ICitaRepository _inner;

        public LoggingCitaRepository(ICitaRepository inner)
        {
            _inner = inner;
        }

        public List<Cita> ObtenerTodos()
        {
            Console.WriteLine(
                $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [CitaDecorator] ObtenerTodos - inicio");

            var resultado = _inner.ObtenerTodos();

            Console.WriteLine(
                $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [CitaDecorator] ObtenerTodos - {resultado.Count} registros");

            return resultado;
        }

        public Cita? ObtenerPorId(int id)
        {
            Console.WriteLine(
                $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [CitaDecorator] ObtenerPorId({id}) - inicio");

            var resultado = _inner.ObtenerPorId(id);

            Console.WriteLine(
                $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [CitaDecorator] ObtenerPorId({id}) - {(resultado != null ? "encontrado" : "no encontrado")}");

            return resultado;
        }

        public void Guardar(List<Cita> citas)
        {
            Console.WriteLine(
                $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [CitaDecorator] Guardar - inicio");

            _inner.Guardar(citas);

            Console.WriteLine(
                $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [CitaDecorator] Guardar - completado");
        }

        public void Actualizar(Cita citaActualizada)
        {
            Console.WriteLine(
                $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [CitaDecorator] Actualizar({citaActualizada.Id}) - inicio");

            _inner.Actualizar(citaActualizada);

            Console.WriteLine(
                $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [CitaDecorator] Actualizar({citaActualizada.Id}) - completado");
        }
    }
}
