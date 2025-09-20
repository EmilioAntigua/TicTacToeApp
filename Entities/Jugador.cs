using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TicTacToeApp.Entities;

[Index(nameof(Nombres), IsUnique = true)] 
public class Jugador
{
    [Key] 
    public int JugadorId { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres")]
    public string Nombres { get; set; } = string.Empty;

    [Required(ErrorMessage = "La cantidad de partidas es obligatoria")]
    [Range(0, int.MaxValue, ErrorMessage = "Las partidas deben ser 0 o más")]
    public int Partidas { get; set; }
}
