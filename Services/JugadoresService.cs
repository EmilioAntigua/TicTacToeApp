using TicTacToeApp.DAL;
using TicTacToeApp.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace TicTacToeApp.Services;

public class JugadoresService(IDbContextFactory<TicTacToeContext> DbFactory)
{
    private async Task<bool> Existe(int jugadorId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Jugadores.AnyAsync(j => j.JugadorId == jugadorId);
    }

    private async Task<bool> Insertar(Jugador jugador)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Jugadores.Add(jugador);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Jugador jugador)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(jugador);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Jugador jugador)
    {
        if (!await Existe(jugador.JugadorId))
            return await Insertar(jugador);
        else
            return await Modificar(jugador);
    }

    public async Task<Jugador?> Buscar(int jugadorId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Jugadores.FirstOrDefaultAsync(j => j.JugadorId == jugadorId);
    }

    public async Task<bool> Eliminar(int jugadorId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Jugadores
            .Where(j => j.JugadorId == jugadorId)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<List<Jugador>> GetList(Expression<Func<Jugador, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Jugadores
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> ExisteNombre(string nombre, int? excluirId = null)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Jugadores
            .AnyAsync(j => j.Nombres.ToLower().Contains(nombre.ToLower())
                           && (!excluirId.HasValue || j.JugadorId != excluirId.Value));
    }
}

