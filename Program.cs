using BlazorBootstrap;
using Microsoft.EntityFrameworkCore;
using TicTacToeApp.Components;
using TicTacToeApp.DAL;
using TicTacToeApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Cadena de conexi�n desde appsettings.json
var connectionString = builder.Configuration.GetConnectionString("SqlConStr");

// Inyecci�n del contexto usando DbContextFactory
builder.Services.AddDbContextFactory<TicTacToeContext>(options =>
    options.UseSqlServer(connectionString));

// Inyecci�n de servicios
builder.Services.AddScoped<JugadoresService>();

// Inyecci�n de BlazorBootstrap
builder.Services.AddBlazorBootstrap();
builder.Services.AddScoped<ToastService>();
// Configuraci�n de Razor Components
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Aplicar migraciones autom�ticamente usando DbContextFactory
using (var scope = app.Services.CreateScope())
{
    var dbFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<TicTacToeContext>>();
    using var db = dbFactory.CreateDbContext();
    db.Database.Migrate();
}

// Configuraci�n de pipeline
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
