
using System.ComponentModel.DataAnnotations;
using CinemeBooking.Domain.Enums;

namespace CinemeBooking.Domain.Entities.Bookings;

public class Customer
{
    [Key]
    public string CustomerUuid { get; set; } = Guid.NewGuid().ToString();

    [Required]
    public string? CustomerName { get; set; }

    // [Required]
    public string? Email { get; set; }

    [Required]
    public string? Phone { get; set; }

    // [Required]
    public int Age { get; set; }

    public StatusEnum Status { get; set; }
}