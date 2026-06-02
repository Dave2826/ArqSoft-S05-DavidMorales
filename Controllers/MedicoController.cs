using Citas.App.Models;
using CitasApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Citas.App.Controllers
{
    public class MedicoController : Controller
    {
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
            return View(medicos);
        }

        public IActionResult Detalle(int id)
        {
            var medico = medicos.FirstOrDefault(m => m.Id == id);

            if (medico == null)
                return NotFound();

            return View(medico);
        }
    }
}