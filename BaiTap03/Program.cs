// Console.WriteLine("Hello, World!");
using System.Text.RegularExpressions;

class HocSinh
{
    private string _ten = "";
    private float _diemToan, _diemLy, _diemHoa;

    public float DiemToan
    {
        get
        {
            return _diemToan;
        }

        set
        {
            if (value >= 0 && value <= 10)
                _diemToan = value;
            else
                throw new ArgumentException("Điểm số phải nằm trong khoảng từ 0 -> 10");
        }
    }

    public float DiemLy
    {
        get
        {
            return _diemLy;
        }

        set
        {
            if (value > 0 && value <= 10)
                _diemLy = value;
            else
                throw new ArgumentException("Điểm số phải nằm trong khoảng từ 0 -> 10");
        }
    }

    public float DiemHoa
    {
        get
        {
            return _diemHoa;
        }

        set
        {
            if (value > 0 && value <= 10)
                _diemHoa = value;
            else
                throw new ArgumentException("Điểm số phải nằm trong khoảng từ 0 -> 10");
        }
    }

    public string TenHocSinh
    {
        get
        {
            return _ten;
        }

        set
        {
            // Định dạng mã unikey cho phép người dùng nhập
            string pattern = @"^[\p{L}\s]+$";
            // Nếu hợp lệ thì gán, nếu sai thì ném ra lỗi hoặc gán giá trị mặc định
            if (!string.IsNullOrWhiteSpace(value) && Regex.IsMatch(value, pattern))
                _ten = value.Trim();
            else
                throw new ArgumentException("Tên không hợp lệ!");
        }
    }

    static float NhapDiemTrongKhoang(string thongBao)
    {
        float giaTri;
        while (true)
        {
            Console.Write(thongBao);
            if (float.TryParse(Console.ReadLine(), out giaTri) && (giaTri >= 0 && giaTri <= 10))
                return giaTri;
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Lỗi: Bạn phải nhập vào giá trị không hợp lệ!");
                Console.ResetColor();

            }
        }
    }

    public void NhapThongTin()
    {
        // Định dạng đầu vô và đầu ra mã Unikey
        Console.InputEncoding = System.Text.Encoding.UTF8;
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Định dạng mã unikey cho phép người dùng nhập
        string pattern = @"^[\p{L}\s]+$";
        Console.Write("Nhập họ và tên học sinh: ");
        string? tenHs = Console.ReadLine() ?? string.Empty;

        // Vòng lặp bắt người dùng nhập theo định dạng tên không kí tự đặt biệt, không null
        while (string.IsNullOrWhiteSpace(tenHs) || !Regex.IsMatch(tenHs, pattern))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Tên không được để trống, không chứa số hoặc ký tự đặc biệt!");
            Console.ResetColor();

            Console.Write("Nhập lại họ và tên học sinh: ");
            tenHs = Console.ReadLine() ?? string.Empty;
        }

        TenHocSinh = tenHs;

        DiemToan = NhapDiemTrongKhoang("Nhập điểm môn toán: ");
        DiemLy = NhapDiemTrongKhoang("Nhập điểm môn lý: ");
        DiemHoa = NhapDiemTrongKhoang("Nhập điểm môn hóa: ");
    }

    public float TinhDiemTrungBinh()
    {
        return (_diemHoa + _diemLy + _diemToan) / 3;
    }

    public void HienThi()
    {
        Console.WriteLine("Xin Chào! " + TenHocSinh);
        Console.WriteLine("Điểm môn toán của bạn là: " + DiemToan);
        Console.WriteLine("Điểm môn lý của bạn là: " + DiemLy);
        Console.WriteLine("Điểm môn hóa của bạn là: " + DiemHoa);
        Console.WriteLine("Điểm môn trung bình của bạn là: " + TinhDiemTrungBinh());
    }
}

class Program
{
    static void Main()
    {
        //
        HocSinh hs = new();
        hs.NhapThongTin();
        hs.HienThi();
    }
}
