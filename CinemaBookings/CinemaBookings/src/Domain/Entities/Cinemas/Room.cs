
using System.ComponentModel.DataAnnotations;
using CinemeBooking.Domain.Enums;

namespace CinemeBooking.Domain.Entities.Cinemas;

public class Room
{
    [Key]
    public string RoomUuid { get; set; } = Guid.NewGuid().ToString();

    [Required]
    public string? RoomName { get; set; }

    // [Required]
    public int TotalSeats { get; set; }

    public StatusEnum? Status { get; set; }
}