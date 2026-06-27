
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

class Program
{
    public static void Main()
    {
        ToaDo2D t1 = new(0.5f, 2);
        ToaDo2D t2 = new(2, 3);
        Console.WriteLine($"Tổng hai tọa độ:  {t1 - t2}");
    }
}