
using System;
using CinemaBooking.Presentation.Controllers;
using CinemaBooking.Presentation.Views.Layout;
using CinemaBooking.Services.Implementations;

namespace CinemaBooking.Presentation.Views.Layout;

class ShowCinema
{
    // Khởi tạo service cho controller
    private static RoomController _roomController = new(new RoomService());
    public static async Task Show()
    {
        while (true)
        {
            Console.Clear();
            // VẼ HEADER
            UILayout.Header("QUẢN LÝ LỊCH CHIẾU & PHÒNG CHIẾU");

            // VẼ BODY
            Console.WriteLine("1. Danh sách lịch chiếu");
            Console.WriteLine("2. Danh sách phòng ");
            Console.WriteLine("3. Danh sách ghế");

            Console.WriteLine("");

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("> Mời bạn chọn chức năng: ");
            Console.ResetColor();

            // Lấy vị trí con trỏ
            int cursorLeft = Console.CursorLeft;
            int cursorTop = Console.CursorTop;
            Console.SetCursorPosition(cursorLeft, cursorTop);
            Console.WriteLine("\n\n");

            // VẼ FOOTER
            UILayout.Footer("[ESC] Thoát ra ngoài");


            Console.SetCursorPosition(cursorLeft, cursorTop);
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            if (keyInfo.Key == ConsoleKey.Escape)
            {
                return;
            }

            switch (keyInfo.KeyChar.ToString())
            {
                case "1":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Tính năng danh sách thể loại đang được phát triển.");
                    Console.ResetColor();
                    Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
                    Console.WriteLine();
                    Console.ReadKey(true);
                    continue;
                case "2":
                    // Console.ForegroundColor = ConsoleColor.Yellow;
                    // Console.WriteLine("Tính năng danh sách thể loại đang được phát triển.");
                    // Console.ResetColor();
                    // Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
                    // Console.WriteLine();
                    // Console.ReadKey(true);
                    // continue;
                    // RoomController.List();
                    await _roomController.List();

                    break;
                case "3":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Tính năng danh sách thể loại đang được phát triển.");
                    Console.ResetColor();
                    Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
                    Console.WriteLine();
                    Console.ReadKey(true);
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