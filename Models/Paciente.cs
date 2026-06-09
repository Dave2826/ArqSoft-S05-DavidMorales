using CitasApp.Domain.Models;

namespace Citas.App.Models
{
    // Compatibility wrapper to keep the original namespace for Views/Controllers/Repositories
    // This class intentionally inherits the domain model so existing code keeps working
    // until we refactor consumers to use the Domain models and interfaces.
    public class Paciente : CitasApp.Domain.Models.Paciente { }
}
