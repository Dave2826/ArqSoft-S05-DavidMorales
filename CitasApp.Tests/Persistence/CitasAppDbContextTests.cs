using Xunit;
using Microsoft.EntityFrameworkCore;
using CitasApp.Domain.Models;
using CitasApp.Infrastructure.Data;

namespace CitasApp.Tests.Persistence;

public class CitasAppDbContextTests
{
    private static CitasAppDbContext CrearContexto(string nombreDb)
    {
        var options = new DbContextOptionsBuilder<CitasAppDbContext>()
            .UseInMemoryDatabase(nombreDb)
            .Options;

        return new CitasAppDbContext(options);
    }

    [Fact]
    public void DbContext_ExposeDbSetPaciente()
    {
        using var context = CrearContexto(nameof(DbContext_ExposeDbSetPaciente));

        var pacientes = context.Pacientes;

        Assert.NotNull(pacientes);
    }

    [Fact]
    public void DbContext_ExposeDbSetMedico()
    {
        using var context = CrearContexto(nameof(DbContext_ExposeDbSetMedico));

        var medicos = context.Medicos;

        Assert.NotNull(medicos);
    }

    [Fact]
    public void DbContext_ExposeDbSetCita()
    {
        using var context = CrearContexto(nameof(DbContext_ExposeDbSetCita));

        var citas = context.Citas;

        Assert.NotNull(citas);
    }

    [Fact]
    public void GuardarPaciente_PersisteCorrectamente()
    {
        using var context = CrearContexto(nameof(GuardarPaciente_PersisteCorrectamente));
        var paciente = new Paciente
        {
            Id = 1,
            Nombre = "Juan",
            Apellido = "Perez",
            Email = "juan@test.com",
            Telefono = "123456789"
        };

        context.Pacientes.Add(paciente);
        context.SaveChanges();

        var recuperado = context.Pacientes.Find(1);
        Assert.NotNull(recuperado);
        Assert.Equal("Juan", recuperado.Nombre);
        Assert.Equal("Perez", recuperado.Apellido);
        Assert.Equal("juan@test.com", recuperado.Email);
        Assert.Equal("123456789", recuperado.Telefono);
    }

    [Fact]
    public void GuardarMedico_PersisteCorrectamente()
    {
        using var context = CrearContexto(nameof(GuardarMedico_PersisteCorrectamente));
        var medico = new Medico
        {
            Id = 1,
            Nombre = "Maria",
            Apellido = "Lopez",
            Especialidad = "Cardiologia",
            NumeroLicencia = "LIC-001"
        };

        context.Medicos.Add(medico);
        context.SaveChanges();

        var recuperado = context.Medicos.Find(1);
        Assert.NotNull(recuperado);
        Assert.Equal("Maria", recuperado.Nombre);
        Assert.Equal("Lopez", recuperado.Apellido);
        Assert.Equal("Cardiologia", recuperado.Especialidad);
        Assert.Equal("LIC-001", recuperado.NumeroLicencia);
    }

    [Fact]
    public void GuardarCita_PersisteCorrectamente()
    {
        using var context = CrearContexto(nameof(GuardarCita_PersisteCorrectamente));
        var paciente = new Paciente { Id = 1, Nombre = "Ana", Apellido = "Rios" };
        var medico = new Medico { Id = 1, Nombre = "Luis", Apellido = "Torres", Especialidad = "Pediatria" };
        context.Pacientes.Add(paciente);
        context.Medicos.Add(medico);
        context.SaveChanges();

        var cita = new Cita
        {
            Id = 1,
            PacienteId = 1,
            MedicoId = 1,
            Fecha = new DateOnly(2026, 8, 15),
            Hora = new TimeOnly(10, 30),
            Motivo = "Control general",
            Estado = "Pendiente"
        };

        context.Citas.Add(cita);
        context.SaveChanges();

        var recuperada = context.Citas.Find(1);
        Assert.NotNull(recuperada);
        Assert.Equal(1, recuperada.PacienteId);
        Assert.Equal(1, recuperada.MedicoId);
        Assert.Equal(new DateOnly(2026, 8, 15), recuperada.Fecha);
        Assert.Equal(new TimeOnly(10, 30), recuperada.Hora);
        Assert.Equal("Control general", recuperada.Motivo);
        Assert.Equal("Pendiente", recuperada.Estado);
    }

    [Fact]
    public void RelacionesEntreEntidades_FuncionanCorrectamente()
    {
        using var context = CrearContexto(nameof(RelacionesEntreEntidades_FuncionanCorrectamente));
        var paciente = new Paciente { Id = 10, Nombre = "Carlos", Apellido = "Mendez" };
        var medico = new Medico { Id = 20, Nombre = "Sofia", Apellido = "Vega", Especialidad = "Dermatologia" };
        context.Pacientes.Add(paciente);
        context.Medicos.Add(medico);
        context.SaveChanges();

        var cita = new Cita
        {
            Id = 100,
            PacienteId = 10,
            MedicoId = 20,
            Fecha = new DateOnly(2026, 9, 1),
            Hora = new TimeOnly(15, 0),
            Motivo = "Consulta",
            Estado = "Confirmada"
        };
        context.Citas.Add(cita);
        context.SaveChanges();

        var citaRecuperada = context.Citas.Find(100);
        Assert.NotNull(citaRecuperada);
        Assert.Equal(10, citaRecuperada.PacienteId);
        Assert.Equal(20, citaRecuperada.MedicoId);

        var pacienteRecuperado = context.Pacientes.Find(10);
        Assert.NotNull(pacienteRecuperado);
        Assert.Equal("Carlos", pacienteRecuperado.Nombre);

        var medicoRecuperado = context.Medicos.Find(20);
        Assert.NotNull(medicoRecuperado);
        Assert.Equal("Sofia", medicoRecuperado.Nombre);
    }

    [Fact]
    public void MultiplesRegistros_SeAlmacenanYRecuperanCorrectamente()
    {
        using var context = CrearContexto(nameof(MultiplesRegistros_SeAlmacenanYRecuperanCorrectamente));
        var pacientes = new List<Paciente>
        {
            new() { Id = 1, Nombre = "Uno", Apellido = "A" },
            new() { Id = 2, Nombre = "Dos", Apellido = "B" },
            new() { Id = 3, Nombre = "Tres", Apellido = "C" }
        };

        context.Pacientes.AddRange(pacientes);
        context.SaveChanges();

        var todos = context.Pacientes.ToList();

        Assert.Equal(3, todos.Count);
        Assert.Contains(todos, p => p.Id == 1 && p.Nombre == "Uno");
        Assert.Contains(todos, p => p.Id == 2 && p.Nombre == "Dos");
        Assert.Contains(todos, p => p.Id == 3 && p.Nombre == "Tres");
    }
}
