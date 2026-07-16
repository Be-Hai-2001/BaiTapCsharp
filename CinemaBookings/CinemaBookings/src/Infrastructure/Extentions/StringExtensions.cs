using System.Globalization;

// namespace CinemaBooking.Infrastructure.Extensions;

public static class StringExtensions
{
    // Kiểm tra định dạng ngày bất kỳ dựa trên văn hóa hệ thống
    public static bool IsValidDate(this string input)
    {
        return DateTime.TryParse(input, out _);
    }

    // Kiểm tra chính xác theo một hoặc nhiều định dạng cụ thể (An toàn hơn)
    public static bool IsValidDateExact(this string input, string format = "dd/MM/yyyy")
    {
        return DateTime.TryParseExact(input, format,
                                      CultureInfo.InvariantCulture,
                                      DateTimeStyles.None,
                                      out _);
    }
}