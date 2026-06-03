namespace Citas.App.ViewModels
{
    public class CitaAgendaViewModel
    {
        public string NombrePaciente { get; set; } = string.Empty;

        public string NombreMedico { get; set; } = string.Empty;

        public DateOnly Fecha { get; set; }

        public TimeOnly Hora { get; set; }

        public string Motivo { get; set; } = string.Empty;

        public string Estado { get; set; } = string.Empty;
    }
}
