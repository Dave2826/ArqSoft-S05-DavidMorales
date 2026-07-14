using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using Citas.App.ViewModels;
using CitasApp.Infrastructure.Notifiers;

namespace Citas.App.Services
{
    public class CitaService : ICitaService
    {
        private readonly ICitaRepository _citaRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IMedicoRepository _medicoRepository;
        private readonly Notificador _notificador;

        public CitaService(
            ICitaRepository citaRepository,
            IPacienteRepository pacienteRepository,
            IMedicoRepository medicoRepository,
            Notificador notificador)
        {
            _citaRepository = citaRepository;
            _pacienteRepository = pacienteRepository;
            _medicoRepository = medicoRepository;
            _notificador = notificador;
        }

        public List<CitaAgendaViewModel> ObtenerAgenda()
        {
            var citas = _citaRepository.ObtenerTodos();
            return CrearAgenda(citas);
        }

        public List<CitaAgendaViewModel> ObtenerAgendaPorPaciente(int pacienteId)
        {
            var resultado = _citaRepository.ObtenerTodos()
                .Where(c => c.PacienteId == pacienteId)
                .ToList();

            return CrearAgenda(resultado);
        }

        private List<CitaAgendaViewModel> CrearAgenda(IEnumerable<Cita> citasOrigen)
        {
            var pacientes = _pacienteRepository.ObtenerTodos();
            var medicos = _medicoRepository.ObtenerTodos();

            return citasOrigen
                .Select(cita =>
                {
                    var paciente = pacientes.FirstOrDefault(p => p.Id == cita.PacienteId);
                    var medico = medicos.FirstOrDefault(m => m.Id == cita.MedicoId);

                    return new CitaAgendaViewModel
                    {
                        NombrePaciente = paciente == null
                            ? $"Paciente #{cita.PacienteId}"
                            : $"{paciente.Nombre} {paciente.Apellido}",
                        NombreMedico = medico == null
                            ? $"Médico #{cita.MedicoId}"
                            : $"{medico.Nombre} {medico.Apellido}",
                        Fecha = cita.Fecha,
                        Hora = cita.Hora,
                        Motivo = cita.Motivo,
                        Estado = cita.Estado
                    };
                })
                .ToList();
        }

        public string? ConfirmarCita(int id)
        {
            var cita = _citaRepository.ObtenerPorId(id);

            if (cita == null)
                return null;

            Console.WriteLine(
                $"[CitaService] Confirmando cita #{id}...");

            cita.Estado = "Confirmada";
            _citaRepository.Actualizar(cita);

            Console.WriteLine(
                $"[CitaService] Estado actualizado. Notificando observers...");

            _notificador.Notificar(
                $"Cita #{id} confirmada - Motivo: {cita.Motivo}");

            Console.WriteLine(
                $"[CitaService] Proceso completado.");

            return $"Cita #{id} confirmada exitosamente.";
        }
    }
}
