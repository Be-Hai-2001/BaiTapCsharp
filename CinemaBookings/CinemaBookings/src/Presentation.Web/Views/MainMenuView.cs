
using CinemaBooking.Presentation.Views.Admin.Layout;
using CinemaBooking.Presentation.Views.Layout;
using CinemaBookings.Views.Admin.Layout;
using CinemeBooking.Views.Layout;

namespace CinemeBooking.Views;

// Class hiển thị ra thanh menu chọn lựa các chức năng của ứng dụng
public class MainMenuView
{
    public static async Task Show(IServiceProvider provider)
    {
        while (true)
        {

            // Làm mới console mỗi khi new page
            Console.Clear();
            // Gọi class hiển thị layout menu lựa chọn chức năng
            MenuLayout.FrameMenu(
                ConsoleColor.Cyan,
                "HỆ THỐNG ĐẶT VÉ XEM PHIM - CINEMA",
                [
                    "1. Kênh Khách Hàng (Đặt vé, xem phim)",
                    "2. Kênh Quản Trị Viên (Admin)",
                    "0. Thoát chương trình"
                ]
            );
            string choice = Console.ReadLine() ?? string.Empty;
            switch (choice)
            {
                case "1":
                    ShowCustomerMenu();
                    break;
                case "2":
                    await ShowAdminMenu(provider);
                    break;
                case "0":
                    Console.WriteLine("Cảm ơn bạn đã sử dụng dịch vụ!");
                    return;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Lựa chọn không hợp lệ! Nhấn phím bất kỳ để thử lại.");
                    Console.ResetColor();
                    // Dùng để dừng chương trình không chạy xuống break
                    Console.ReadKey();
                    break;
            }
        }
    }

    // ================= MENU KHÁCH HÀNG =================
    private static void ShowCustomerMenu()
    {
        while (true)
        {
            Console.Clear();
            // Gọi class hiển thị layout menu khách hàng
            MenuLayout.FrameMenu(
                ConsoleColor.Green,
                "MENU KHÁCH HÀNG",
                [
                    "1. Xem danh sách phim đang chiếu",
                    "2. Xem lịch chiếu theo phim",
                    "3. Đặt vé xem phim (Chọn ghế & Đồ ăn thức uống)",
                    "4. Xem lịch sử đặt vé / Thanh toán",
                    "0. Quay lại Menu chính"
                ]
            );
            string choice = Console.ReadLine() ?? string.Empty;
            if (choice == "0") break;

            switch (choice)
            {
                case "1":
                    Console.WriteLine("\n[Chức năng] Hiển thị danh sách phim...");
                    // Gọi tới MovieController.ListMovies();
                    break;
                case "2":
                    Console.WriteLine("\n[Chức năng] Hiển thị lịch chiếu...");
                    // Gọi tới ShowtimeController.ViewShowtimes();
                    break;
                case "3":
                    Console.WriteLine("\n[Chức năng] Tiến hành đặt vé...");
                    // Gọi tới BookingController.CreateBooking();
                    break;
                case "4":
                    Console.WriteLine("\n[Chức năng] Xem lịch sử đặt vé...");
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Lựa chọn không hợp lệ!");
                    Console.ResetColor();
                    break;
            }
            Console.WriteLine("\nNhấn phím bất kỳ để tiếp tục...");
            Console.ReadKey();
        }
    }

    // ================= MENU ADMIN =================
    public static async Task ShowAdminMenu(IServiceProvider provider)
    {

        while (true)
        {
            Console.Clear();

            // HEADER
            UILayout.Header("HỆ THỐNG QUẢN TRỊ (ADMIN)");

            // BODY
            Console.WriteLine("1. Quản lý Phim & Thể loại (CRUD Movie/Genre)");
            Console.WriteLine("2. Quản lý Lịch chiếu & Phòng chiếu (Showtime/Room/Seat)");
            Console.WriteLine("3. Xem danh sách đặt vé & Doanh thu");

            // FOOTER

            Console.WriteLine("");
            // Lưu lại vị trí con trỏ để nhập
            int cursorLeft = Console.CursorLeft;
            int cursorTop = Console.CursorTop;

            Console.WriteLine("");
            Console.WriteLine("");
            UILayout.Footer("[ESC]: Quay lại Menu chính");

            // Quay lại vị trí nhập liệu ban đầu để chờ người dùng bấm máy
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.SetCursorPosition(cursorLeft, cursorTop);
            Console.Write("> Mời bạn chọn chức năng: ");
            Console.ResetColor();

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            if (keyInfo.Key == ConsoleKey.Escape)
            {
                await Show(provider);
            }
            else
            {
                // Quay lại vị trí nhập liệu ban đầu để chờ người dùng bấm máy
                // string choice = keyInfo.KeyChar.ToString();
                switch (keyInfo.KeyChar.ToString())
                {
                    case "1":
                        await ShowMovieOrGenre.Show(provider);
                        DebugExtensions.dd(keyInfo.KeyChar.ToString());
                        break;
                    case "2":
                        await ShowCinema.Show();
                        // Console.WriteLine("\n[Admin] Điều hướng sang trang quản lý Lịch chiếu...");
                        break;
                    case "3":
                        Console.WriteLine("\n[Admin] Điều hướng sang trang quản lý Ghế...");
                        break;
                    case "4":
                        Console.WriteLine("\n[Admin] Điều hướng sang trang quản lý Đồ ăn...");
                        break;
                    case "5":
                        Console.WriteLine("\n[Admin] Thống kê doanh thu...");
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Lựa chọn không hợp lệ!");
                        Console.ResetColor();
                        Console.WriteLine("Nhấn bất kì để thử lại...");
                        Console.ReadKey();
                        break;
                }
            }

        }
    }
}