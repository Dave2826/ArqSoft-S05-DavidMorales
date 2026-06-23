using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Infrastructure.Repositories
{
    public class LoggingPacienteRepository : IPacienteRepository
    {
        private readonly IPacienteRepository _inner;

        public LoggingPacienteRepository(
            IPacienteRepository inner)
        {
            _inner = inner;
        }

        public List<Paciente> ObtenerTodos()
        {
            Console.WriteLine(
                $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ObtenerTodos - inicio");

            var resultado = _inner.ObtenerTodos();

            Console.WriteLine(
                $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ObtenerTodos - {resultado.Count} registros");

            return resultado;
        }

        public Paciente? ObtenerPorId(int id)
        {
            Console.WriteLine(
                $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ObtenerPorId({id}) - inicio");

            var resultado = _inner.ObtenerPorId(id);

            Console.WriteLine(
                $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ObtenerPorId({id}) - {(resultado != null ? "encontrado" : "no encontrado")}");

            return resultado;
        }

        public void Guardar(List<Paciente> pacientes)
        {
            Console.WriteLine(
                $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Guardar - inicio");

            _inner.Guardar(pacientes);

            Console.WriteLine(
                $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Guardar - completado");
        }

        public void Actualizar(Paciente pacienteActualizado)
        {
            Console.WriteLine(
                $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Actualizar({pacienteActualizado.Id}) - inicio");

            _inner.Actualizar(pacienteActualizado);

            Console.WriteLine(
                $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Actualizar({pacienteActualizado.Id}) - completado");
        }
    }
}