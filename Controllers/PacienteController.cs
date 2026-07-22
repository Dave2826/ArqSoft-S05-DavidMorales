using CitasApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Citas.App.Controllers
{
    public class PacienteController : Controller
    {
        private readonly CitasApp.Domain.Interfaces.IPacienteRepository pacienteRepository;

        public PacienteController(CitasApp.Domain.Interfaces.IPacienteRepository pacienteRepository)
        {
            this.pacienteRepository = pacienteRepository;
        }

        public IActionResult Index()
        {
            // Repository now returns domain models; views expect the compatibility wrappers which inherit Domain models,
            // so we can pass the domain models directly.
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
