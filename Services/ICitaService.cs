using Citas.App.ViewModels;

namespace Citas.App.Services
{
    public interface ICitaService
    {
        List<CitaAgendaViewModel> ObtenerAgenda();
        List<CitaAgendaViewModel> ObtenerAgendaPorPaciente(int pacienteId);
        string? ConfirmarCita(int id);
    }
}
