using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicTacToeApp.DAL;
using TicTacToeApp.Entities;

namespace TicTacToeApp.Services
{
    public class JugadoresService
    {
        private readonly TicTacToeContext _ctx;

        public JugadoresService(TicTacToeContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<List<Jugador>> ListarAsync()
        {
            return await _ctx.Jugadores
                             .OrderBy(j => j.JugadorId)
                             .ToListAsync();
        }

        public async Task<Jugador?> ObtenerPorIdAsync(int id)
        {
            return await _ctx.Jugadores.FindAsync(id);
        }

        // Guarda tanto para insertar como para actualizar.
        public async Task<(bool ok, string? error)> GuardarAsync(Jugador jugador)
        {
            // Validar duplicado. Si es edición, excluye el mismo id.
            if (await ExisteNombreAsync(jugador.Nombres, jugador.JugadorId == 0 ? (int?)null : jugador.JugadorId))
            {
                return (false, "Ya existe un jugador con ese nombre.");
            }

            if (jugador.JugadorId == 0)
            {
                _ctx.Jugadores.Add(jugador);
            }
            else
            {
                _ctx.Jugadores.Update(jugador);
            }

            try
            {
                await _ctx.SaveChangesAsync();
                return (true, null);
            }
            catch (DbUpdateException ex)
            {
                return (false, "Error al guardar en la base de datos: " + ex.Message);
            }
            catch (Exception ex)
            {
                return (false, "Error inesperado: " + ex.Message);
            }
        }

        public async Task<bool> ExisteNombreAsync(string nombres, int? excluirId = null)
        {
            if (string.IsNullOrWhiteSpace(nombres)) return false;
            var nombresLower = nombres.Trim().ToLowerInvariant();

            return await _ctx.Jugadores.AnyAsync(j =>
                j.Nombres.ToLower() == nombresLower &&
                (!excluirId.HasValue || j.JugadorId != excluirId.Value));
        }

        public async Task EliminarAsync(int id)
        {
            var ent = await _ctx.Jugadores.FindAsync(id);
            if (ent is not null)
            {
                _ctx.Jugadores.Remove(ent);
                await _ctx.SaveChangesAsync();
            }
        }
    }
}
