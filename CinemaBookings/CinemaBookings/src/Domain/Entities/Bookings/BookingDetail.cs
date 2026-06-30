
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CinemeBooking.Domain.Enums;

namespace CinemeBooking.Domain.Entities.Bookings;

public class BookingDetail
{
    [Key]
    public string BookingDetailUuid { get; set; } = Guid.NewGuid().ToString();

    [ForeignKey("BookingUuid")]
    public virtual Booking? Booking { get; set; }
    public string? BookingUuid { get; set; }

    [Required]
    public string? ItemUuid { get; set; } // [FK] (Linh hoạt: Nếu là vé thì trỏ tới ShowtimeSeatUuid, nếu là đồ ăn thì trỏ tới FaBUuid)

    [Required]
    public int Quantity { get; set; }

    [Required]
    public decimal UnitPrice { get; set; } // Giá theo ItemUuid

    [Required]
    public decimal SubTotal { get; set; } // Tổng giá

    public StatusEnum Status { get; set; }
}