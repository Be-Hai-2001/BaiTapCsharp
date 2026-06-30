
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CinemeBooking.Domain.Entities.Movies;
using CinemeBooking.Domain.Enums;

namespace CinemeBooking.Domain.Entities.ShowTimes;

public class Showtime
{
    [Key]
    public string ShowtimeUuid { get; set; } = Guid.NewGuid().ToString();

    [Required]
    public string? MovieUuid { get; set; } // Khác service

    [Required]
    public DateTime StartTime { get; set; } // thời gian bắt đầu chiếu phim

    [Required]
    public DateTime EndTime { get; set; } // Thời gian kết thúc

    [Required]
    public DateTime Date { get; set; } // Ngày chiếu phim

    public StatusEnum? Status { get; set; }
}