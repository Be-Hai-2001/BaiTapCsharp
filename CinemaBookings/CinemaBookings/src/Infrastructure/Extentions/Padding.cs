public static class Padding
{
    // Kiểm tra định dạng ngày bất kỳ dựa trên văn hóa hệ thống
    // public static bool IsValidDate(this string input)
    // {
    //     return DateTime.TryParse(input, out _);
    // }

    // Kiểm tra chính xác theo một hoặc nhiều định dạng cụ thể (An toàn hơn)
    public static string PadCenter(string text, int width)
    {
        // Nếu chuỗi dài hơn hoặc bằng độ rộng màn hình thì giữ nguyên
        if (string.IsNullOrEmpty(text) || text.Length >= width)
            return text;

        int totalSpaces = width - text.Length;
        int padLeft = totalSpaces / 2 + text.Length;

        // Đẩy khoảng trắng đều sang 2 bên
        return text.PadLeft(padLeft).PadRight(width);
    }
}