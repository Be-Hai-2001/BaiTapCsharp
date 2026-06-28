
// Bài tập 1
public class ToaDo2D
{
    public float ToaDoX { get; set; }
    public float ToaDoY { get; set; }

    public ToaDo2D(float x, float y)
    {
        ToaDoX = x;
        ToaDoY = y;
    }

    // Tính tổng 2 toạn độ
    public static ToaDo2D operator +(ToaDo2D a, ToaDo2D b)
    {
        return new ToaDo2D(b.ToaDoX + b.ToaDoX, a.ToaDoY + b.ToaDoY);
    }

    // Tính "Hiệu" 2 toạn độ
    public static ToaDo2D operator -(ToaDo2D a, ToaDo2D b)
    {
        return new ToaDo2D(b.ToaDoX - a.ToaDoX, b.ToaDoY - a.ToaDoY);
    }

    // Conver hiển thị ra màn hình theo class ToanDo2D
    public override string ToString()
    {
        return $"({ToaDoX} : {ToaDoY})";
    }
}

// Bài tập số 2
public class ThoiGian
{
    public int Gio { get; set; }
    public int Phut { get; set; }

    public ThoiGian(int gio, int phut)
    {
        Gio = gio;
        Phut = phut;
    }

    // So sánh hai thời gian giống nhau
    public static bool operator ==(ThoiGian a, ThoiGian b)
    {
        return (a.Phut == b.Phut && a.Gio == b.Gio) ? true : false;
    }

    // So sánh hai thời gian khác nhau
    public static bool operator !=(ThoiGian a, ThoiGian b)
    {
        return (a.Phut != b.Phut || a.Gio != b.Gio) ? true : false;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not ThoiGian other)
        {
            return false;
        }

        return Gio * other.Phut == other.Gio * Phut;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Gio, Phut);
    }

    public override string ToString()
    {
        return $"{Gio} giờ {Phut} phút";
    }
}

// Bài tập số 3 
public class SoPhuc
{
    public double PhanThuc { get; set; }
    public double PhanAo { get; set; }
    public double DonViAo { get; set; }

    public SoPhuc(double pThuc, double pAo)
    {
        PhanThuc = pThuc;
        PhanAo = pAo;
    }

    // Conver về tham số hiển thị theo Obj class
    public override string ToString()
    {
        return PhanAo >= 0 ? ($"{PhanThuc} + {PhanAo}i") : ($"{PhanThuc} {PhanAo}i");
    }

    // Nạp chồng toán tử nhân 2 số phức
    public static SoPhuc operator *(SoPhuc a, SoPhuc b)
    {

        double phanThucMoi = (a.PhanThuc * b.PhanThuc) - (a.PhanAo * b.PhanAo);
        double phanAoMoi = (a.PhanThuc * b.PhanAo) + (a.PhanAo * b.PhanThuc);

        return new SoPhuc(phanThucMoi, phanAoMoi);
    }

    public SoPhuc Multiply(SoPhuc sp)
    {
        return this * sp;
    }
}

// Bài tập số 4
public class TienTe
{
    public double SoTien { get; set; }

    public TienTe(double soTien)
    {
        SoTien = soTien;
    }

    // 1. Chuyển đổi ngầm định (Implicit): double -> TienTe
    public static implicit operator TienTe(double value)
    {
        return new TienTe(value);
    }

    // 2. Chuyển đổi tường minh (Explicit): TienTe -> double
    public static explicit operator double(TienTe value)
    {
        return value.SoTien;
    }


}

// Bài tập số 5
public class Vector3D
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }

    public Vector3D(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static Vector3D operator +(Vector3D a, Vector3D b)
    {
        return new Vector3D(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    }

    public static Vector3D operator -(Vector3D a, Vector3D b)
    {
        return new Vector3D(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
    }

    // Tích vô hướng 2 vector
    public static double operator *(Vector3D a, Vector3D b)
    {
        return (a.X * b.X + a.Y * b.Y + a.Z * b.Z);
    }

    // So sánh hai vector gian giống nhau
    public static bool operator ==(Vector3D a, Vector3D b)
    {
        return (a.X == b.X && a.Y == b.Y && a.Z == b.Z) ? true : false;
    }

    // So sánh hai vector khác nhau
    public static bool operator !=(Vector3D a, Vector3D b)
    {
        return (a.X != b.X || a.Y != b.Y || a.Z == b.Z) ? true : false;
    }

    // So sánh hai vector gian giống nhau
    public override bool Equals(object? obj)
    {
        if (obj is not Vector3D other)
        {
            return false;
        }

        return X == other.X && Y == other.Y && Z == other.Z;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z);
    }

    public override string ToString()
    {
        return $"AB({X},{Y},{Z})";
    }

    // Chuyển đổi tường minh Vector3D => double
    public static explicit operator double(Vector3D v)
    {
        return Math.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);
    }

}

// Class run 
class Program
{
    public static void Main()
    {
        // Bài tập số 01
        // ToaDo2D t1 = new(0.5f, 2);
        // ToaDo2D t2 = new(2, 3);
        // Console.WriteLine($"Tổng hai tọa độ:  {t1 + t2}");
        // Console.WriteLine($"Hiệu 2 hai tọa độ:  {t1 - t2}");

        // Bài tập số 02
        // ThoiGian tg1 = new(1, 60);
        // ThoiGian tg2 = new(1, 60);
        // Console.WriteLine((tg1 == tg2) ? "Hai thời gian như nhau" : "Hai thời gian khác nhau");

        // Bài tập sô 03
        // Cách 1: Sử dụng toán tử * đã nạp chồng
        // SoPhuc sp1 = new(1, -3);
        // SoPhuc sp2 = new(2, -1);
        // Console.WriteLine($"Tích 2 hai số phức:  {sp1 * sp2}");
        // // Cách 2: Sử dụng phương thức Multiply
        // Console.WriteLine($"Tích 2 hai số phức:  {sp1.Multiply(sp2)}");

        // Bài tập số 04
        // TienTe giaTri1 = 50000;
        // Console.WriteLine($"Ép kiểu double -> TienTe : giaTri1 thuộc kiểu {giaTri1.GetType()}");

        // double giaTri2 = (double)giaTri1;
        // Console.WriteLine($"Ép kiểu TienTe -> double : giaTri2 thuộc kiểu {giaTri2.GetType()}");

        // Bài tập số 05
        Vector3D v1 = new(1, 1, 2);
        Vector3D v2 = new(1, 2, 2);

        // Tổng 2 vector
        Console.WriteLine($"Tổng hai vector là: {v1 + v2}");
        // Hiệu 2 vector
        Console.WriteLine($"Hiệu hai vector là: {v1 - v2}");
        // So sánh 2 vector
        Console.WriteLine((v1 == v2) ? "Hai Vector như nhau" : "Hai Vector khác nhau");
    }
}