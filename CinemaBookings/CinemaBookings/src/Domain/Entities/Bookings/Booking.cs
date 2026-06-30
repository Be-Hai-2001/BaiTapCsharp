
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CinemeBooking.Domain.Enums;

namespace CinemeBooking.Domain.Entities.Bookings;

public class Booking
{
    [Key]
    public string BookingUuid { get; set; } = Guid.NewGuid().ToString();

    [ForeignKey("CustomerUuid")]
    public virtual Customer? Customer { get; set; }

    public string? RoomUuid { get; set; } // Khác service

    [Required]
    public DateTime BookingTime { get; set; }

    [Required]
    public decimal TotalAmount { get; set; }

    public StatusEnum Status { get; set; }
}