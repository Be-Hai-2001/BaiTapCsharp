
using CinemeBooking.Views.Layout;

namespace CinemeBooking.Views;

// Class hiển thị ra thanh menu chọn lựa các chức năng của ứng dụng
public class MainMenuView
{
    public static void Show()
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
                    ShowAdminMenu();
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
    private static void ShowAdminMenu()
    {
        while (true)
        {
            Console.Clear();
            // Gọi class hiển thị layout menu Admin
            MenuLayout.FrameMenu(
                ConsoleColor.Yellow,
                "HỆ THỐNG QUẢN TRỊ (ADMIN)",
                [
                    "1. Quản lý Phim & Thể loại (CRUD Movie/Genre)",
                    "2. Quản lý Lịch chiếu & Phòng chiếu (Showtime/Room)",
                    "3. Quản lý Ghế ngồi (Seat/SeatType)",
                    "4. Quản lý Đồ ăn & Thức uống (Food & Beverage)",
                    "5. Xem danh sách đặt vé & Doanh thu",
                    "0. Quay lại Menu chính"
                ]
            );
            string choice = Console.ReadLine() ?? string.Empty;
            if (choice == "0") break;

            switch (choice)
            {
                case "1":
                    Console.WriteLine("\n[Admin] Điều hướng sang trang quản lý Phim...");
                    break;
                case "2":
                    Console.WriteLine("\n[Admin] Điều hướng sang trang quản lý Lịch chiếu...");
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
                    break;
            }
            Console.WriteLine("\nNhấn phím bất kỳ để tiếp tục...");
            Console.ReadKey();
        }
    }
}