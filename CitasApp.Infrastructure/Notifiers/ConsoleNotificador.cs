using CitasApp.Domain.Interfaces;

namespace CitasApp.Infrastructure.Notifiers
{
    public class ConsoleNotificador : IObserver
    {
        public void Update(string mensaje)
        {
            Console.WriteLine(
                $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [Observer ConsoleNotificador] {mensaje}");
        }
    }
}
