using CitasApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Citas.App.Controllers
{
    public class MedicoController : Controller
    {
        private readonly CitasApp.Domain.Interfaces.IMedicoRepository medicoRepository;

        public MedicoController(CitasApp.Domain.Interfaces.IMedicoRepository medicoRepository)
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
