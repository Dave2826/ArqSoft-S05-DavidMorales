using Microsoft.EntityFrameworkCore;
using CitasApp.Domain.Interfaces;
using CitasApp.Infrastructure.Notifiers;
using CitasApp.Infrastructure.Repositories;
using CitasApp.Infrastructure.Data;
using Citas.App.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// GOF Observer: create the subject and attach the concrete observer
var notificador = new Notificador();
notificador.Attach(new ConsoleNotificador());
builder.Services.AddSingleton(notificador);

// Entity Framework Core with PostgreSQL
builder.Services.AddDbContext<CitasAppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register domain repository interfaces to their Infrastructure implementations.
// Uses RepositoryFactory (GOF Factory) to create the appropriate implementation,
// then wraps it with LoggingPacienteRepository (GOF Decorator).
builder.Services.AddScoped<IPacienteRepository>(sp =>
{
    var env = sp.GetRequiredService<IWebHostEnvironment>();
    var context = sp.GetRequiredService<CitasAppDbContext>();

    var repositorio = RepositoryFactory.CrearPacienteRepository(
        env.EnvironmentName, context);

    return new LoggingPacienteRepository(repositorio);
});

builder.Services.AddScoped<IMedicoRepository>(sp =>
{
    var context = sp.GetRequiredService<CitasAppDbContext>();
    return new MedicoRepository(context);
});

builder.Services.AddScoped<ICitaRepository>(sp =>
{
    var env = sp.GetRequiredService<IWebHostEnvironment>();
    var context = sp.GetRequiredService<CitasAppDbContext>();

    var repositorio = RepositoryFactory.CrearCitaRepository(
        env.EnvironmentName, context);

    return new LoggingCitaRepository(repositorio);
});

builder.Services.AddScoped<ICitaService, CitaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
