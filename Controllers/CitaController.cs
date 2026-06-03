using Microsoft.AspNetCore.Mvc;
using Citas.App.Models;
using Citas.App.ViewModels;

namespace Citas.App.Controllers
{
    public class CitaController : Controller
    {
        private static List<Cita> citas = new()
        {
            new Cita
            {
                Id = 1,
                PacienteId = 1,
                MedicoId = 1,
                Fecha = new DateOnly(2026, 6, 1),
                Hora = new TimeOnly(9, 0),
                Motivo = "Consulta general",
                Estado = "Confirmada"
            },
            new Cita
            {
                Id = 2,
                PacienteId = 2,
                MedicoId = 2,
                Fecha = new DateOnly(2026, 6, 1),
                Hora = new TimeOnly(10, 0),
                Motivo = "Revisión de resultados",
                Estado = "Pendiente"
            }
        };

        private static List<Paciente> pacientes = new()
        {
            new Paciente
            {
                Id = 1,
                Nombre = "Juan",
                Apellido = "Pérez",
                Email = "juan@email.com",
                Telefono = "9991112233"
            },
            new Paciente
            {
                Id = 2,
                Nombre = "María",
                Apellido = "López",
                Email = "maria@email.com",
                Telefono = "9994445566"
            }
        };

        private static List<Medico> medicos = new()
        {
            new Medico
            {
                Id = 1,
                Nombre = "Carlos",
                Apellido = "Reyes",
                Especialidad = "Cardiología",
                NumeroLicencia = "MED001"
            },
            new Medico
            {
                Id = 2,
                Nombre = "Patricia",
                Apellido = "Vega",
                Especialidad = "Pediatría",
                NumeroLicencia = "MED002"
            }
        };

        public IActionResult Index()
        {
            return View(CrearAgenda(citas));
        }

        public IActionResult PorPaciente(int pacienteId)
        {
            var resultado = citas
                .Where(c => c.PacienteId == pacienteId)
                .ToList();

            return View(CrearAgenda(resultado));
        }

        private static List<CitaAgendaViewModel> CrearAgenda(IEnumerable<Cita> citasOrigen)
        {
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
