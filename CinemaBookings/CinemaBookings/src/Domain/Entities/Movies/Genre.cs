using System.ComponentModel.DataAnnotations;
using CinemeBooking.Domain.Enums;

namespace CinemeBooking.Domain.Entities.Movies;

public class Genre
{
    [Key]
    public string GenreUuid { get; set; } = Guid.NewGuid().ToString();

    [Required]
    public string? GenreName { get; set; }

    public StatusEnum Status { get; set; }
}