
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CinemeBooking.Domain.Entities.Cinemas;
using CinemeBooking.Domain.Entities.Movies;
using CinemeBooking.Domain.Enums;

namespace CinemeBooking.Domain.Entities.ShowTimes;

public class ShowtimeSeat
{
    [Key]
    public string ShowtimeSeatUuid { get; set; } = Guid.NewGuid().ToString();

    [ForeignKey("ShowtimeUuid")]
    public virtual Showtime? Showtime { get; set; }
    public string? ShowtimeUuid { get; set; }

    [Required]
    public string? Seat { get; set; } // Khác service

    [Required]
    public decimal Price { get; set; }

    public StatusEnum Status { get; set; }
}