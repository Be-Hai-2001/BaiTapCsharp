
using System.ComponentModel.DataAnnotations;
using CinemeBooking.Domain.Enums;

namespace CinemeBooking.Domain.Entities.Cinemas;

public class SeatType
{
    [Key]
    public string SeatTypeUuid { get; set; } = Guid.NewGuid().ToString();

    [Required]
    public string? SeatTypeName { get; set; }

    public StatusEnum? Status { get; set; }
}