using Citas.App.Models;
using Microsoft.AspNetCore.Mvc;

namespace Citas.App.Controllers
{
    public class PacienteController : Controller
    {
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

        public IActionResult Index()
        {
            return View(pacientes);
        }

        public IActionResult Detalle(int id)
        {
            var paciente = pacientes.FirstOrDefault(p => p.Id == id);

            if (paciente == null)
                return NotFound();

            return View(paciente);
        }
    }
}
