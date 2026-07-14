using CitasApp.Domain.Interfaces;

namespace CitasApp.Domain.Models
{
    public class Medico : IEntity
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Especialidad { get; set; } = string.Empty;
        public string NumeroLicencia { get; set; } = string.Empty;
    }
}
