using Microsoft.EntityFrameworkCore;
using TicTacToeApp.Entities;

namespace TicTacToeApp.DAL;

public class TicTacToeContext : DbContext
{
    public TicTacToeContext(DbContextOptions<TicTacToeContext> options)
        : base(options)
    {
    }

    // Representa la tabla Jugadores en la Base de Datos
    public DbSet<Jugador> Jugadores => Set<Jugador>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Refuerzo del índice único en Nombres
        modelBuilder.Entity<Jugador>()
            .HasIndex(j => j.Nombres)
            .IsUnique();
    }
}
