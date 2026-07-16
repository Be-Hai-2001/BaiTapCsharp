
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CinemeBooking.Domain.Enums;

namespace CinemeBooking.Domain.Entities.Movies;

public class Movie
{
    [Key]
    public string MovieUuid { get; set; } = Guid.NewGuid().ToString();

    [ForeignKey("GenreUuid")]
    public virtual Genre? Genre { get; set; }
    public string? GenreUuid { get; set; }

    [Required]
    public string? MovieName { get; set; }

    // [Required]
    public int AgeCustomer { get; set; }

    [Required]
    public int Duration { get; set; } // Thời lượng

    [Required]
    public LanguageEnum? Language { get; set; }

    [Required]
    public string? ReleaseDate { get; set; } // Ngày phát hành

    [Required]
    public StatusEnum? Status { get; set; }
}