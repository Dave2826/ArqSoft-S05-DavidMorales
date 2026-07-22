using CitasApp.Domain.Interfaces;

namespace CitasApp.Infrastructure.Notifiers
{
    public class Notificador
    {
        private readonly List<IObserver> _observers = [];

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notificar(string mensaje)
        {
            Console.WriteLine($"[Notificador] Notificando a {_observers.Count} observador(es)...");

            foreach (var observer in _observers)
            {
                observer.Update(mensaje);
            }
        }
    }
}
