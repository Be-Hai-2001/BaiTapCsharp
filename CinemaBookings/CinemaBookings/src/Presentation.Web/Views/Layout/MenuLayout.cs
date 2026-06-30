namespace CinemeBooking.Views.Layout;

// class là layout chung của các trang menu
// Mục đích: Sau này có đổi ngôn ngữ || Nâng cấp khác muốn chỉnh sửa các layout như nhau thì vô đây chỉnh 1 lần không cần chỉnh sửa nhiều chỗ
// Code thể hiện tính đa hình

/// <summary>
/// Props :
/// foregroundColor : Màu của thanh tiêu đề 
/// title : Nội dung tiêu đề
/// List : Danh sách chức năng
/// </summary>
public class MenuLayout
{
    public static void FrameMenu(ConsoleColor foregroundColor, string title, List<string> dsChucNang)
    {
        // tổng số dấu bằng
        int totalWidth = 55;
        // Cách tính toán để căn giữa chuỗi

        Console.Clear();
        Console.ForegroundColor = foregroundColor;
        Console.WriteLine("====================================================");
        Console.WriteLine(title.PadLeft((totalWidth + title.Length) / 2).PadRight(totalWidth));
        Console.WriteLine("====================================================");
        Console.ResetColor();

        foreach (string item in dsChucNang)
            Console.WriteLine(item);

        Console.WriteLine("----------------------------------------------------");
        Console.Write("Mời bạn chọn chức năng: ");
    }
}