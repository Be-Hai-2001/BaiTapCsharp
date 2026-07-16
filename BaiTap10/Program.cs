// ------- Độ phức tạp của thuật toán ------- //

using System.Diagnostics;
using System.Text;

public class Program
{

    #region Bài 1  - Nhận diện Big O
    /// <summary>
    /// Độ phức tạp: O(1) - Thời gian hằng số (Constant Time)
    /// Dù mảng có 5 phần tử hay 5 triệu phần tử, hàm này vẫn chỉ tốn đúng 1 bước máy 
    /// để truy cập trực tiếp vào ô nhớ đầu tiên và in ra.
    /// </summary>
    static void InPhanTuDauTien(int[] arr)
    {
        if (arr.Length > 0)
        {
            Console.WriteLine($"Phần tử đầu tiên (index 0) là: {arr[0]}");
        }
        else
        {
            Console.WriteLine("Mảng rỗng, không có phần tử đầu tiên!");
        }
    }

    /// <summary>
    /// Độ phức tạp: O(n) - Thời gian tuyến tính (Linear Time)
    /// Thời gian chạy tỷ lệ thuận với số lượng phần tử (n) của mảng.
    /// Mảng có 5 phần tử -> lặp 5 lần. Mảng có n phần tử -> lặp n lần.
    /// </summary>
    static void InToanBoMang(int[] arr)
    {
        Console.Write("Toàn bộ các phần tử trong mảng: ");
        for (int i = 0; i < arr.Length; i++)
        {
            Console.Write(arr[i] + " ");
        }
    }
    #endregion

    #region Bài 2  - Thực hành dùng Stopwatch
    static void ThuatToan01()
    {
        // Dùng phép + để cộng dồn chuỗi "*"  10,000 lần.
        string vd1 = "";
        for (int i = 0; i < 10000; i++)
            vd1 += "*";
    }

    static void ThuatToan02()
    {
        // Dùng StringBuilder.Append("*") 10,000 lần.
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < 10000; i++)
            sb.Append("*");

    }

    static void ThuatToan03()
    {
        double n = 5000000000;

        for (double x = 0; x <= n; x++)
        {
            // Vòng lặp n (Ở giữa): Đại diện cho trục Y
            for (double y = 0; y <= n; y++)
            {
                // Vòng lặp 3 (Trong cùng): Đại diện cho trục Z
                for (double z = 0; z <= n; z++)
                {
                    Console.WriteLine($"Toạ độ: X = {x}, Y = {y}, Z = {z}");
                }
            }
            Console.WriteLine("------------------"); // Phân tách khi hết một tầng X
        }

    }
    #endregion

    #region Bài 3 - Đo lường thuật toán O(n^2)

    static void BubbleSort(int[] arr)
    {
        int n = arr.Length;
        // Vòng lặp ngoài: Duyệt qua từng phần tử của mảng
        for (int i = 0; i < n - 1; i++)
        {
            // Vòng lặp trong: So sánh các cặp cạnh nhau
            // (n - i - 1) giúp bỏ qua các phần tử cuối cùng đã được sắp xếp đúng vị trí
            for (int j = 0; j < n - i - 1; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    // Hoán đổi vị trí (Swap)
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
            }
        }
    }

    #endregion

    static void Main()
    {

        #region Bài 1  - Nhận diện Big OBài 1  - Nhận diện Big O

        // int[] mangKiemChung = { 10, 20, 30, 40, 50 };
        // // 1. Kiểm chứng phương thức O(1)
        // Console.WriteLine("1. Gọi hàm InPhanTuDauTien (O(1)):");
        // InPhanTuDauTien(mangKiemChung);
        // Console.WriteLine();

        // // 2. Kiểm chứng phương thức O(n)
        // Console.WriteLine("2. Gọi hàm InToanBoMang (O(n)):");
        // InToanBoMang(mangKiemChung);

        #endregion

        #region Bài 2  - Thực hành dùng Stopwatch

        // var stopwatch01 = Stopwatch.StartNew();
        // ThuatToan01();
        // stopwatch01.Stop();
        // Console.WriteLine("ThuatToan01 đo bằng stopwatch02: " + stopwatch01.ElapsedMilliseconds + " ms");

        // var stopwatch02 = Stopwatch.StartNew();
        // ThuatToan02();
        // stopwatch02.Stop();
        // Console.WriteLine("ThuatToan02 đo bằng stopwatch02: " + stopwatch02.ElapsedMilliseconds + " ms");

        #endregion
    }
}