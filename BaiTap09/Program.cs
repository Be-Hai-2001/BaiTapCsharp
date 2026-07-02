#region Bài 01 - Bắt lỗi định dạng (FormatException)
// class Program
// {
//     static void Main()
//     {
//         try
//         {
//             Console.Write("Nhập vào năm sinh của bạn: ");
//             int namSinh = int.Parse(Console.ReadLine());
//         }
//         catch (FormatException)
//         {
//             Console.WriteLine("Lỗi: Định dạng không hợp lệ. Vui lòng nhập một số nguyên.");
//             // throw;
//         }
//         catch (Exception e)
//         {
//             // Bắt các lỗi khác : Lỗi nhập số quá lớn || hoặc tự conver tại đây
//             Console.WriteLine($"Lỗi: {e.Message}");
//             // throw;
//         }
//     }
// }
#endregion

#region Bài 02 - Sử dụng nhiều khối try-catch
// class Program
// {
//     static void Main()
//     {
//         int[] mang = { 1, 2, 3 };

//         try
//         {
//             Console.Write("Mời nhập ví trị bạn muốn xem: ");
//             int index = int.Parse(Console.ReadLine());

//             Console.WriteLine($"Giá trị tại vị trí {index} là: {mang[index]}");

//             Console.Write("Mời nhập số chia: ");
//             int soChia = int.Parse(Console.ReadLine());

//             Console.WriteLine($"Kết quả chia: {mang[index] / soChia}");
//         }
//         catch (FormatException)
//         {
//             Console.WriteLine("Lỗi: Định dạng không hợp lệ. Vui lòng nhập một số nguyên.");
//             // throw;
//         }
//         catch (IndexOutOfRangeException)
//         {
//             Console.WriteLine("Lỗi: Vị trí bạn nhập vượt quá phạm vi của mảng.");
//             // throw;
//         }
//         catch (DivideByZeroException)
//         {
//             Console.WriteLine("Lỗi: Chia cho 0.");
//             // throw;
//         }
//         catch (Exception e)
//         {
//             Console.WriteLine($"Lỗi: {e.Message}");
//             throw;
//         }
//     }
// }
#endregion

#region Bài 03 - Ứng dụng khối finally
class KetNoiCSDL
{
    public static void MoKetNoi()
    {
        Console.WriteLine("Mở kết nối CSDL");
    }

    public static void DongKetNoi()
    {
        Console.WriteLine("Đóng kết nối CSDL");
    }
}

// class Program
// {
//     static void Main()
//     {
//         KetNoiCSDL.MoKetNoi();

//         try
//         {
//             int a = 1;
//             int b = 0;

//             // Tạo ra lỗi chi cho 0
//             int c = a / b;
//         }
//         catch (DivideByZeroException)
//         {
//             Console.WriteLine("Lỗi: Chia cho 0.");
//             // throw;
//         }
//         finally
//         {
//             KetNoiCSDL.DongKetNoi();
//         }
//     }
// }

#endregion


#region Bài 04 - Sử dụng từ khóa throw
class Program
{
    static double TinhCanBacHai(double so)
    {
        if (so < 0)
        {
            throw new ArgumentException("Không thể tính căn bậc hai của số âm!");
        }
        return Math.Sqrt(so);
    }
    static void Main()
    {
        try
        {
            Console.Write("Nhập vào");
        }
    }
}
#endregion


