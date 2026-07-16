// namespace CinemaBooking.Infrastructure.Extentions;

public static class Display
{
    public static void ClearDisplay()
    {
        try
        {
            Console.Write("\u001b[3J\u001b[H");
        }
        catch
        {
            // Bỏ qua nếu terminal không hỗ trợ escape sequence
        }

        Console.Clear();
        Console.SetCursorPosition(0, 0);
    }
}
