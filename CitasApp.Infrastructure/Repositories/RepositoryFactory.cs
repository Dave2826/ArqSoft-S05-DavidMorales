using CitasApp.Domain.Interfaces;
using CitasApp.Infrastructure.Data;

namespace CitasApp.Infrastructure.Repositories
{
    public static class RepositoryFactory
    {
        public static IPacienteRepository CrearPacienteRepository(
            string entorno,
            CitasAppDbContext context)
        {
            Console.WriteLine(
                $"[Factory] Entorno detectado: {entorno}");

            return entorno switch
            {
                "Production" =>
                    new MemoriaPacienteRepository(),

                _ =>
                    new PacienteRepository(context)
            };
        }

        public static ICitaRepository CrearCitaRepository(
            string entorno,
            CitasAppDbContext context)
        {
            Console.WriteLine(
                $"[Factory Cita] Entorno detectado: {entorno}");

            return new CitaRepository(context);
        }
    }
}
