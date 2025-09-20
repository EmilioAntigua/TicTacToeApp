using Microsoft.EntityFrameworkCore;
using TicTacToeApp.Entities;

namespace TicTacToeApp.DAL;

public class TicTacToeContext : DbContext
{
    public TicTacToeContext(DbContextOptions<TicTacToeContext> options)
        : base(options)
    {
    }

    public DbSet<Jugador> Jugadores => Set<Jugador>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Jugador>()
            .HasIndex(j => j.Nombres)
            .IsUnique();
    }
}
