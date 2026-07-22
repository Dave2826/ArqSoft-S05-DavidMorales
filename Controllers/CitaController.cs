using Citas.App.Services;
using Microsoft.AspNetCore.Mvc;

namespace Citas.App.Controllers
{
    public class CitaController : Controller
    {
        private readonly ICitaService _citaService;

        public CitaController(ICitaService citaService)
        {
            _citaService = citaService;
        }

        public IActionResult Index()
        {
            return View(_citaService.ObtenerAgenda());
        }

        public IActionResult PorPaciente(int pacienteId)
        {
            return View(_citaService.ObtenerAgendaPorPaciente(pacienteId));
        }

        [HttpPost]
        public IActionResult Confirmar(int id)
        {
            var resultado = _citaService.ConfirmarCita(id);

            if (resultado == null)
                return NotFound();

            return Ok(resultado);
        }
    }
}
