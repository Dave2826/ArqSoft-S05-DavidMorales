using Citas.App.Models;
using Citas.App.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Citas.App.Controllers
{
    public class MedicoController : Controller
    {
        private readonly MedicoRepository medicoRepository;

        public MedicoController(MedicoRepository medicoRepository)
        {
            this.medicoRepository = medicoRepository;
        }

        public IActionResult Index()
        {
            var medicos = medicoRepository.ObtenerTodos();
            return View(medicos);
        }

        public IActionResult Detalle(int id)
        {
            var medico = medicoRepository.ObtenerPorId(id);

            if (medico == null)
                return NotFound();

            return View(medico);
        }
    }
}
