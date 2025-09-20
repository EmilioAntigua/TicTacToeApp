using BlazorBootstrap;
using Microsoft.EntityFrameworkCore;
using TicTacToeApp.Components;
using TicTacToeApp.DAL;
using TicTacToeApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Cadena de conexión desde appsettings.json
var connectionString = builder.Configuration.GetConnectionString("SqlConStr");

// Inyección del contexto usando DbContextFactory
builder.Services.AddDbContextFactory<TicTacToeContext>(options =>
    options.UseSqlServer(connectionString));

// Inyección de servicios
builder.Services.AddScoped<JugadoresService>();

// Inyección de BlazorBootstrap
builder.Services.AddBlazorBootstrap();
builder.Services.AddScoped<ToastService>();
// Configuración de Razor Components
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Aplicar migraciones automáticamente usando DbContextFactory
using (var scope = app.Services.CreateScope())
{
    var dbFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<TicTacToeContext>>();
    using var db = dbFactory.CreateDbContext();
    db.Database.Migrate();
}

// Configuración de pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
