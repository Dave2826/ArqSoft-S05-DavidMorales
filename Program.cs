using CitasApp.Domain.Interfaces;
using CitasApp.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Register domain repository interfaces to their Infrastructure implementations.
// We construct the implementations with the Data directory path from the host environment.
// Uses RepositoryFactory (GOF Factory) to create the appropriate implementation,
// then wraps it with LoggingPacienteRepository (GOF Decorator).
builder.Services.AddSingleton<IPacienteRepository>(sp =>
{
    var env = sp.GetRequiredService<IWebHostEnvironment>();
    var dataPath = Path.Combine(env.ContentRootPath, "Data");

    // Factory: decide qué implementación concreta crear según el entorno
    var repositorio = RepositoryFactory.CrearPacienteRepository(
        env.EnvironmentName, dataPath);

    // Decorator: envuelve el repositorio con logging
    return new LoggingPacienteRepository(repositorio);
});

builder.Services.AddSingleton<IMedicoRepository>(sp =>
{
    var env = sp.GetRequiredService<IWebHostEnvironment>();
    var dataPath = Path.Combine(env.ContentRootPath, "Data");
    return new MedicoRepository(dataPath);
});

builder.Services.AddSingleton<ICitaRepository>(sp =>
{
    var env = sp.GetRequiredService<IWebHostEnvironment>();
    var dataPath = Path.Combine(env.ContentRootPath, "Data");
    return new CitaRepository(dataPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
