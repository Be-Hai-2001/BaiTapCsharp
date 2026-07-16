// ======================== CẤU TRÚC ĐỆ QUUY ======================== //
public class Program
{
    static void Main()
    {
        int n = 5;
        // Bài 1 - Đệ quy tuyến tính cơ bản
        Console.WriteLine(DemNguoc(n));

        // Bài 3 - Đệ quy tương hỗ (Mutual Recursion)
        Console.WriteLine(Ping(n));
    }

    #region Bài 1 - Đệ quy tuyến tính cơ bản ( Điếm ngược số n người dùng nhập vào)
    // 5 -> 4 -> 3 -> 2 -> 1 -> 0 (Bắt đầu!) //
    static string DemNguoc(int n)
    {
        if (n == 0) return "Bắt đầu!";
        return $"{n} -> {DemNguoc(n - 1)}";
    }
    #endregion

    #region Bài 3 - Đệ quy tương hỗ (Mutual Recursion)
    // Ping: 5 Pong: 4 Ping: 3 Pong: 2 Ping 1
    static string Ping(int n)
    {
        if (n == 0) return "";
        return $"Ping: {n} -> {Pong(n - 1)}";
    }
    static string Pong(int n)
    {
        if (n == 0) return "";
        return $"Pong: {n} -> {Ping(n - 1)}";
    }
    #endregion

    #region Đệ quy nhị phân & Mảng

    #endregion
}