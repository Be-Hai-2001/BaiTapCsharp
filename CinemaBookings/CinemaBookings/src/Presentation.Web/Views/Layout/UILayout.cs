

namespace CinemaBooking.Presentation.Views.Layout;

// Layout chung header footer cho mỗi trang
public class UILayout
{
    public static void Header(string title, string subtitle, int totalTableWidth = 83)
    {
        string borderLine = new('=', totalTableWidth);

        // Vẽ giao diện Tiêu đề
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(borderLine);
        Console.WriteLine(Padding.PadCenter(title, totalTableWidth));
        Console.WriteLine(subtitle);
        Console.WriteLine(borderLine);
        Console.ResetColor();
    }
    // Npp chồng header không có subtitle
    public static void Header(string title, int totalTableWidth = 83)
    {
        string borderLine = new('=', totalTableWidth);

        // Vẽ giao diện Tiêu đề
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(borderLine);
        Console.WriteLine(Padding.PadCenter(title, totalTableWidth));
        Console.WriteLine(borderLine);
        Console.ResetColor();
    }

    public static void Footer(string title = "[F8]: Bấm để Thêm Mới | [ESC]: Thoát ra ngoài", int totalTableWidth = 83)
    {
        string borderLine = new('=', totalTableWidth);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\n{borderLine}");
        Console.WriteLine(Padding.PadCenter(title, totalTableWidth));
        Console.WriteLine(borderLine);
        Console.ResetColor();
    }

    // Nạp chồng funtion footer khi người ta không muốn truyền title
    public static void Footer(int totalTableWidth = 83)
    {
        string borderLine = new('=', totalTableWidth);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\n{borderLine}");
        Console.WriteLine(Padding.PadCenter(" [F8]: Bấm để Thêm Mới | [ESC]: Thoát ra ngoài", totalTableWidth));
        Console.WriteLine(borderLine);
        Console.ResetColor();
    }
}