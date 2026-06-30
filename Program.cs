using RentaDepartamentosWeb.Data;
using RentaDepartamentosWeb.Repositories;
using RentaDepartamentosWeb.Services;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------------------------
// Inyección de dependencias (DI nativa de .NET)
// ---------------------------------------------------------------------------

// MVC: Controllers + Views
builder.Services.AddControllersWithViews();

// Data: proveedor de conexiones SQL Server (lee "ConexionSQL" desde appsettings.json).
// Se registra como Scoped: una instancia por cada petición HTTP. Esto evita
// crear una nueva instancia en cada inyección dentro de la misma request
// (a diferencia de Transient), pero también evita compartir estado entre
// peticiones distintas de usuarios distintos (a diferencia de Singleton),
// lo cual sería riesgoso si en el futuro esta clase llegara a guardar estado
// relacionado con una conexión abierta. Como además depende de IConfiguration
// (que vive como Singleton) no hay conflicto de "captive dependency".
builder.Services.AddScoped<ConexionBD>();

// Repositories: acceso a datos (registrados por interfaz, aún sin lógica)
builder.Services.AddScoped<IDepartamentoRepository, DepartamentoRepository>();

// Services: lógica de negocio (registrados por interfaz, aún sin lógica)
builder.Services.AddScoped<IDepartamentoService, DepartamentoService>();

var app = builder.Build();

// ---------------------------------------------------------------------------
// Pipeline HTTP
// ---------------------------------------------------------------------------

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
