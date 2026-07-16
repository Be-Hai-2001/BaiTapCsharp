
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CinemeBooking.Domain.Enums;

namespace CinemeBooking.Domain.Entities.Cinemas;

public class Seat
{
    [Key]
    public string SeatUuid { get; set; } = Guid.NewGuid().ToString();

    public SeatTypeEnnum SeatType { get; set; }

    public virtual Room? Room { get; set; }
    public string? RoomUuid { get; set; }

    [Required]
    public string? SeatName { get; set; }

    // [Required]
    public int SeatX { get; set; }

    // [Required]
    public int SeatY { get; set; }

    public StatusEnum? Status { get; set; }
}