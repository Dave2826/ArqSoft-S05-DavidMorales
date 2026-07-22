using Xunit;
using Moq;
using CitasApp.Domain.Interfaces;
using CitasApp.Infrastructure.Notifiers;

namespace CitasApp.Tests.Notifiers;

public class NotificadorTests
{
    [Fact]
    public void Notificar_ConUnObserver_LlamaUpdateConElMensaje()
    {
        var observerMock = new Mock<IObserver>();
        var notificador = new Notificador();
        notificador.Attach(observerMock.Object);

        notificador.Notificar("Hola");

        observerMock.Verify(o => o.Update("Hola"), Times.Once);
    }

    [Fact]
    public void Notificar_ConMultiplesObservers_LlamaUpdateATodos()
    {
        var mock1 = new Mock<IObserver>();
        var mock2 = new Mock<IObserver>();
        var mock3 = new Mock<IObserver>();
        var notificador = new Notificador();
        notificador.Attach(mock1.Object);
        notificador.Attach(mock2.Object);
        notificador.Attach(mock3.Object);

        notificador.Notificar("test");

        mock1.Verify(o => o.Update("test"), Times.Once);
        mock2.Verify(o => o.Update("test"), Times.Once);
        mock3.Verify(o => o.Update("test"), Times.Once);
    }

    [Fact]
    public void Detach_DespuesDeAttach_NoRecibeNotificacion()
    {
        var observerMock = new Mock<IObserver>();
        var notificador = new Notificador();
        notificador.Attach(observerMock.Object);
        notificador.Detach(observerMock.Object);

        notificador.Notificar("mensaje");

        observerMock.Verify(o => o.Update(It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public void Detach_ConObserverNoRegistrado_NoLanzaExcepcion()
    {
        var observerMock = new Mock<IObserver>();
        var notificador = new Notificador();

        var exception = Record.Exception(() => notificador.Detach(observerMock.Object));

        Assert.Null(exception);
    }

    [Fact]
    public void Notificar_SinObservers_NoLanzaExcepcion()
    {
        var notificador = new Notificador();

        var exception = Record.Exception(() => notificador.Notificar("test"));

        Assert.Null(exception);
    }

    [Fact]
    public void Attach_MismoObserverDosVeces_UpdateLlamadoDosVeces()
    {
        var observerMock = new Mock<IObserver>();
        var notificador = new Notificador();
        notificador.Attach(observerMock.Object);
        notificador.Attach(observerMock.Object);

        notificador.Notificar("duplicado");

        observerMock.Verify(o => o.Update("duplicado"), Times.Exactly(2));
    }
}
