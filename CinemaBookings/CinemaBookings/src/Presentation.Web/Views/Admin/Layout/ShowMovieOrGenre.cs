
using CinemaBooking.Presentation.Controllers;
using Microsoft.Extensions.DependencyInjection;
using CinemaBooking.Presentation.Views.Layout;
using CinemeBooking.Views;

namespace CinemaBooking.Presentation.Views.Admin.Layout;

public class ShowMovieOrGenre
{
    public static async Task Show(IServiceProvider provider)
    {
        var movieController = provider.GetRequiredService<MovieController>();

        while (true)
        {
            Console.Clear();

            // HEADER
            UILayout.Header("QUẢN LÝ PHIM & THỂ LOẠI");
            // DebugExtensions.dd("Tu nhay vo");

            // BODY
            Console.WriteLine("1. Danh sách Phim");
            Console.WriteLine("2. Danh sách thể loại phim");

            // FOOTER
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("> Mời bạn chọn chức năng: ");
            Console.ResetColor();

            // Lưu lại vị trí con trỏ
            int cursorLeft = Console.CursorLeft;
            int cursorTop = Console.CursorTop;

            Console.WriteLine("\n\n");
            UILayout.Footer("[ESC]: Thoát ra ngoài");

            Console.SetCursorPosition(cursorLeft, cursorTop);
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            if (keyInfo.Key == ConsoleKey.Escape)
            {
                await MainMenuView.ShowAdminMenu(provider);
                return;
            }

            string choice = keyInfo.KeyChar.ToString();

            switch (choice)
            {
                case "1":
                    await movieController.List();
                    return;
                case "2":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Tính năng danh sách thể loại đang được phát triển.");
                    Console.ResetColor();
                    Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
                    Console.WriteLine();
                    // Console.ReadKey(true);
                    continue;
                default:
                    // DebugExtensions.dd(choice);
                    Console.ForegroundColor = ConsoleColor.Red;
                    // Quay lại vị trí nhập liệu ban đầu để chờ người dùng bấm máy
                    Console.SetCursorPosition(cursorLeft, cursorTop);
                    Console.WriteLine("Lựa chọn không hợp lệ!");
                    Console.ResetColor();
                    Console.WriteLine("Nhấn phím bất kỳ để thử lại...");
                    // Lưu lại vị trí con trỏ
                    int defaultLeft = Console.CursorLeft;
                    int defaultTop = Console.CursorTop;
                    Console.SetCursorPosition(defaultLeft, defaultTop);
                    Console.ReadKey(true);
                    continue;
            }
        }
    }
}