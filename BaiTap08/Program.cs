#region Bài tập thực hành số 01 - Khởi tạo đối tượng struct không dùng từ khóa new()

struct ThoiGian
{
    public int? Gio;
    public int? Phut;
    public int? Giay;
}

// class Program
// {
//     public static void Main()
//     {
//         ThoiGian thoiGian;
//         thoiGian.Gio = 7;
//         thoiGian.Phut = 10;
//         thoiGian.Giay = 30;

//         Console.WriteLine($"Thời gian của bạn là: {thoiGian.Gio} giờ {thoiGian.Phut} phut {thoiGian.Giay} giây");
//     }

// }
#endregion

#region Bài thực hành số 02 - Khởi tạo đối tượng struct dùng từ khóa new()

struct Diem2D
{
    public int X;
    public int Y;

    // Constructor: phương thức khởi tạo khi dùng từ khóa new()
    public Diem2D(int x, int y)
    {
        X = x;
        Y = y;
    }
}

// class Program
// {
//     public static void Main()
//     {
//         // Cách 01: Khởi tạo trực tiếp trong new()
//         Diem2D diem01 = new(2, 4);
//         Console.WriteLine($"Điểm 2D đầu tiên bạn khởi tạo có tọa độ là: diem01({diem01.X}, {diem01.Y})");

//         // Cách 02: Khởi tạo bằng cách gán giá trị
//         Diem2D diem02 = new();
//         diem02.X = 7;
//         diem02.Y = 1;
//         Console.WriteLine($"Điểm 2D thứ 2 bạn khởi tạo có tọa độ là: diem02({diem02.X}, {diem02.Y})");
//     }
// }

#endregion

#region Bài thực hành số 03 - Struct chứa phương thức xử lý nội bộ

struct HinhChuNhat
{
    public double ChieuDai;
    public double ChieuRong;

    public HinhChuNhat(double chieuDai, double chieuRong)
    {
        ChieuDai = chieuDai;
        ChieuRong = chieuRong;
    }

    // Struct có thể chứa phương thức
    public double TinhDienTich()
    {
        return ChieuDai * ChieuRong;
    }
}

// class Program
// {
//     public static void Main()
//     {
//         HinhChuNhat hCN01 = new(3, 5);
//         Console.WriteLine($"Diện tích hình chữ nhật là: {hCN01.TinhDienTich}");
//     }
// }
#endregion

#region Bài thự hành số 04 - Kiểm chứng tính chất "Tham trị" (Value Type) của Struct
struct NhanVien
{
    public string? HoTen;
    public int LuongCoBan;

    public NhanVien(string hoTen, int luongCoBan)
    {
        HoTen = hoTen;
        LuongCoBan = luongCoBan;
    }
}

class Program
{
    static void TangLuong(NhanVien nv)
    {
        nv.LuongCoBan += 999;
        Console.WriteLine($"Lương sau khi gọi trong hàm TangLuong: {nv.LuongCoBan}");
    }
    static void Main()
    {
        NhanVien nv1 = new("Nguyễn Minh Hải", 10000);
        TangLuong(nv1);

        // Mức lương vẫn là 5000 do Struct truyền bản sao, không ảnh hưởng biến gốc
        Console.WriteLine($"Lương sau khi gọi hàm TangLuong trong main: {nv1.LuongCoBan}");
    }
}
#endregion
