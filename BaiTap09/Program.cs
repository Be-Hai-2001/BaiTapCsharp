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
// class Program
// {
//     static int TinhCanBacHai(int so)
//     {
//         if (so < 0)
//             throw new ArgumentException("Không thể tính căn bậc hai của số âm!");
//         return (int)Math.Sqrt(so);
//     }
//     static void Main()
//     {
//         Console.WriteLine("---- Tính căn bậc hai của một số -----");
//         try
//         {
//             Console.Write("Nhập vào giá trị: ");
//             int giaTri = int.Parse(Console.ReadLine());
//             int ketQua = TinhCanBacHai(giaTri);
//             Console.WriteLine($"Căn bậc hai của {giaTri} là: {ketQua}");
//         }
//         catch (FormatException)
//         {
//             Console.WriteLine("Lỗi: Định dạng không hợp lệ. Vui lòng nhập một số nguyên.");
//         }
//         catch (Exception e)
//         {
//             Console.WriteLine($"Lỗi: {e.Message}");
//         }
//     }
// }
#endregion


#region Bài 05 - Tạo và sử dụng Custom Exception
public class SoDuKhongDuException : Exception
{
    public SoDuKhongDuException(string message) : base(message) { }
}

public class TaiKhoanNganHang
{
    public static double SoDu { get; private set; }

    public static double RutTien(double soTien)
    {
        SoDu = 120000;

        if (soTien > SoDu)
        {
            throw new SoDuKhongDuException("Số dư không đủ để thực hiện giao dịch.");
        }
        SoDu -= soTien;
        return SoDu;
    }
}

public class Program
{
    static void Main()
    {
        try
        {
            Console.Write("Nhập số tiền bạn muốn rút: ");
            double soTien = double.Parse(Console.ReadLine());
            TaiKhoanNganHang.RutTien(soTien);

        }
        catch (SoDuKhongDuException e)
        {
            Console.WriteLine($"Lỗi: {e.Message}");
        }
        catch (FormatException)
        {
            Console.WriteLine("Lỗi: Định dạng không hợp lệ. Vui lòng nhập một số.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Lỗi: {e.Message}");
        }
    }
}
#endregion
