
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CinemeBooking.Domain.Enums;

namespace CinemeBooking.Domain.Entities.Bookings;

public class Payment
{
    [Key]
    public string PaymentUuid { get; set; } = Guid.NewGuid().ToString();

    [ForeignKey("BookingUuid")]
    public virtual Booking? Booking { get; set; }
    public string? BookingUuid { get; set; }

    [Required]
    public decimal Amount { get; set; }

    [Required]
    public PaymentMethodEnum PaymentMethod { get; set; }

    public DateTime PaymentTime { get; set; }

    public StatusEnum Status { get; set; }
}