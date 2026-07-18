
class SanPham
{
    public string? TenSP { get; set; }
    public decimal? GiaTien { get; set; }

    public SanPham(string tenSP, decimal giaTien)
    {
        TenSP = tenSP;
        GiaTien = giaTien;
    }
}

class Program
{
    #region Bài 1 - Tìm kiếm điểm số
    static bool DiemSoTuyetDoi(double[] mang)
    {
        for (int i = 0; i < mang.Length; i++)
        {
            if (mang[i] == 10.0) // So sánh từng phần tử với giá trị cần tìm
            {
                return true; // Trả về vị trí (index) nếu tìm thấy
            }
        }
        return false; // Trả về -1 nếu không tìm thấy
    }
    #endregion

    #region Bài 3  - Tìm kiếm đối tượng (Min/Max tuỳ chỉnh)
    static SanPham CheapestProduct(SanPham[] dsSp)
    {
        // Thuật toán tìm kiếm tuần tự duyệt qua mảng để tìm và trả về sản phẩm có GiaTien rẻ nhất
        if (dsSp.Length == 0)
            throw new System.InvalidOperationException("Không Có sản phẩm nào trong danh sách");

        SanPham cheapest = dsSp[0];

        for (int i = 1; i < dsSp.Length; i++)
            if (dsSp[i].GiaTien < cheapest.GiaTien)
                cheapest = dsSp[i];

        return cheapest;
    }
    #endregion

    #region Bài 4 - Tìm kiếm nhị phân bằng Đệ quy
    static int TimKiemNhiPhanDeQuy(int[] mang, int giaTri, int left, int right)
    {
        if (mang == null || mang.Length == 0)
            throw new System.InvalidOperationException("Không Có phần tử nào trong mảng");

        if (left > right)
            return -1;

        int mid = left + (right - left) / 2;

        if (mang[mid] == giaTri)
            return mid;

        if (mang[mid] > giaTri)
            return TimKiemNhiPhanDeQuy(mang, giaTri, left, mid - 1);

        return TimKiemNhiPhanDeQuy(mang, giaTri, mid + 1, right);
    }
    #endregion

    #region Bài 5 - Tìm tất cả các vị trí
    static List<int> TimKiemTatCa(int[] mang, int giaTri)
    {
        List<int> viTri = new List<int>();

        for (int i = 0; i < mang.Length; i++)
            if (mang[i] == giaTri) viTri.Add(i);

        return viTri;
    }
    #endregion

    static void Main()
    {
        int[] matrix = { 2, 5, 8, 5, 9, 5, 12 };
        int giaTri = 5;

        // Bài 3  - Tìm kiếm đối tượng (Min/Max tuỳ chỉnh)
        SanPham[] dsSanPham = new SanPham[3] {
            new SanPham("Áo thun", 150),
            new SanPham("Quần thun", 170),
            new SanPham("Giày", 180),
        };

        SanPham cheapest = CheapestProduct(dsSanPham);
        Console.WriteLine($"Sản phẩm có giá tiền rẻ nhất là: {cheapest.TenSP} - {cheapest.GiaTien}$");

        // Bài 4 - Tìm kiếm nhị phân bằng Đệ quy
        Console.WriteLine($"{giaTri} nằm ở vị trí thứ {TimKiemNhiPhanDeQuy(matrix, giaTri, 0, matrix.Length - 1)} trong mảng trên");

        // Bài 5 - Tìm tất cả các vị trí
        List<int> viTri = TimKiemTatCa(matrix, giaTri);
        if (viTri.Count == 0)
            Console.WriteLine("Mảng rỗng");
        else
            Console.WriteLine($"{giaTri} nằm ở vị trí thứ {string.Join(", ", viTri)} trong mảng trên");
    }
}