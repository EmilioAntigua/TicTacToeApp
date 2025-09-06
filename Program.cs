using TicTacToeApp.Components;
using Microsoft.EntityFrameworkCore; // Para usar EF Core
using TicTacToeApp.DAL;              // Para usar nuestro DbContext
using TicTacToeApp.Services;         // Para usar JugadoresService

var builder = WebApplication.CreateBuilder(args);
// 1. Obtener la cadena de conexión desde appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// 2. Registrar el DbContext con SQL Server
builder.Services.AddDbContext<TicTacToeContext>(options =>
    options.UseSqlServer(connectionString));
// 3. Registrar el servicio de Jugadores
builder.Services.AddScoped<JugadoresService>();    
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TicTacToeContext>();
    db.Database.Migrate(); // Crea la BD y aplica las migraciones pendientes
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
