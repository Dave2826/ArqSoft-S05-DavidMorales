using CitasApp.Domain.Models;
using Citas.App.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Citas.App.Controllers
{
    public class CitaController : Controller
    {
        private readonly CitasApp.Domain.Interfaces.ICitaRepository citaRepository;
        private readonly CitasApp.Domain.Interfaces.IPacienteRepository pacienteRepository;
        private readonly CitasApp.Domain.Interfaces.IMedicoRepository medicoRepository;

        public CitaController(
            CitasApp.Domain.Interfaces.ICitaRepository citaRepository,
            CitasApp.Domain.Interfaces.IPacienteRepository pacienteRepository,
            CitasApp.Domain.Interfaces.IMedicoRepository medicoRepository)
        {
            this.citaRepository = citaRepository;
            this.pacienteRepository = pacienteRepository;
            this.medicoRepository = medicoRepository;
        }

        public IActionResult Index()
        {
            var citas = citaRepository.ObtenerTodos();
            return View(CrearAgenda(citas));
        }

        public IActionResult PorPaciente(int pacienteId)
        {
            var resultado = citaRepository.ObtenerTodos()
                .Where(c => c.PacienteId == pacienteId)
                .ToList();

            return View(CrearAgenda(resultado));
        }

        private List<CitaAgendaViewModel> CrearAgenda(IEnumerable<CitasApp.Domain.Models.Cita> citasOrigen)
        {
            var pacientes = pacienteRepository.ObtenerTodos();
            var medicos = medicoRepository.ObtenerTodos();

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
    }
}
