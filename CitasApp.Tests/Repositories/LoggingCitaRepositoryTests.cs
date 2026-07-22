using Xunit;
using Moq;
using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using CitasApp.Infrastructure.Repositories;

namespace CitasApp.Tests.Repositories;

public class LoggingCitaRepositoryTests
{
    [Fact]
    public void ObtenerTodos_DelegaAlRepositorioInterno()
    {
        var listaEsperada = new List<Cita> { new() { Id = 1, Motivo = "Consulta" } };
        var innerMock = new Mock<ICitaRepository>();
        innerMock.Setup(r => r.ObtenerTodos()).Returns(listaEsperada);
        var decorator = new LoggingCitaRepository(innerMock.Object);

        var resultado = decorator.ObtenerTodos();

        innerMock.Verify(r => r.ObtenerTodos(), Times.Once);
        Assert.Same(listaEsperada, resultado);
    }

    [Fact]
    public void ObtenerTodos_CuandoInnerRetornaVacio_DevuelveListaVacia()
    {
        var innerMock = new Mock<ICitaRepository>();
        innerMock.Setup(r => r.ObtenerTodos()).Returns(new List<Cita>());
        var decorator = new LoggingCitaRepository(innerMock.Object);

        var resultado = decorator.ObtenerTodos();

        Assert.Empty(resultado);
    }

    [Fact]
    public void ObtenerPorId_DelegaAlRepositorioInterno()
    {
        var citaEsperada = new Cita { Id = 5, Motivo = "Control" };
        var innerMock = new Mock<ICitaRepository>();
        innerMock.Setup(r => r.ObtenerPorId(5)).Returns(citaEsperada);
        var decorator = new LoggingCitaRepository(innerMock.Object);

        var resultado = decorator.ObtenerPorId(5);

        innerMock.Verify(r => r.ObtenerPorId(5), Times.Once);
        Assert.Same(citaEsperada, resultado);
    }

    [Fact]
    public void ObtenerPorId_CuandoInnerRetornaNull_DevuelveNull()
    {
        var innerMock = new Mock<ICitaRepository>();
        innerMock.Setup(r => r.ObtenerPorId(99)).Returns((Cita?)null);
        var decorator = new LoggingCitaRepository(innerMock.Object);

        var resultado = decorator.ObtenerPorId(99);

        Assert.Null(resultado);
    }

    [Fact]
    public void Guardar_DelegaAlRepositorioInterno()
    {
        var citas = new List<Cita> { new() { Id = 1, Motivo = "Revision" } };
        var innerMock = new Mock<ICitaRepository>();
        var decorator = new LoggingCitaRepository(innerMock.Object);

        decorator.Guardar(citas);

        innerMock.Verify(r => r.Guardar(citas), Times.Once);
    }

    [Fact]
    public void Actualizar_DelegaAlRepositorioInterno()
    {
        var cita = new Cita { Id = 3, Motivo = "Urgencia" };
        var innerMock = new Mock<ICitaRepository>();
        var decorator = new LoggingCitaRepository(innerMock.Object);

        var exception = Record.Exception(() => decorator.Actualizar(cita));

        Assert.Null(exception);
        innerMock.Verify(r => r.Actualizar(cita), Times.Once);
    }
}
