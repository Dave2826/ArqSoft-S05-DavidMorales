using Microsoft.AspNetCore.Mvc;
using Citas.App.Models;

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

        public IActionResult Index()
        {
            return View(citas);
        }

        public IActionResult PorPaciente(int pacienteId)
        {
            var resultado = citas
                .Where(c => c.PacienteId == pacienteId)
                .ToList();

            return View(resultado);
        }
    }
}