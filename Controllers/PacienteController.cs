using Citas.App.Models;
using Citas.App.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Citas.App.Controllers
{
    public class PacienteController : Controller
    {
        private readonly PacienteRepository pacienteRepository;

        public PacienteController(PacienteRepository pacienteRepository)
        {
            this.pacienteRepository = pacienteRepository;
        }

        public IActionResult Index()
        {
            var pacientes = pacienteRepository.ObtenerTodos();
            return View(pacientes);
        }

        public IActionResult Detalle(int id)
        {
            var paciente = pacienteRepository.ObtenerPorId(id);

            if (paciente == null)
                return NotFound();

            return View(paciente);
        }
    }
}
