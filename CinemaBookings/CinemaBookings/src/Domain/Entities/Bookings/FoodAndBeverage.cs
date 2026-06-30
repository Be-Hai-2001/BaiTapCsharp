
using System.ComponentModel.DataAnnotations;
using CinemeBooking.Domain.Enums;

namespace CinemeBooking.Domain.Entities.Bookings;

public class FoodAndBeverage
{
    [Key]
    public string FaBUuid { get; set; } = Guid.NewGuid().ToString();

    [Required]
    public string? ItemName { get; set; }

    [Required]
    public decimal Price { get; set; }

    public StatusEnum Status { get; set; }
}