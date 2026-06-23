using CitasApp.Domain.Interfaces;

namespace CitasApp.Infrastructure.Repositories
{
    public static class RepositoryFactory
    {
        public static IPacienteRepository CrearPacienteRepository(
            string entorno,
            string dataPath)
        {
            Console.WriteLine(
                $"[Factory] Entorno detectado: {entorno}");

            return entorno switch
            {
                "Production" =>
                    new MemoriaPacienteRepository(),

                _ =>
                    new PacienteRepository(dataPath)
            };
        }

        public static ICitaRepository CrearCitaRepository(
            string entorno,
            string dataPath)
        {
            Console.WriteLine(
                $"[Factory Cita] Entorno detectado: {entorno}");

            return new CitaRepository(dataPath);
        }
    }
}