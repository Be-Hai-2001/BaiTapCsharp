using System;
using System.Text.Json;

public static class DebugExtensions
{
    // Hàm này mô phỏng y hệt dd() của PHP
    public static void dd(this object obj)
    {
        if (obj == null)
        {
            Console.WriteLine("null");
        }
        else
        {
            // Cấu hình để in JSON đẹp đẽ (đụt dòng, dễ nhìn)
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(obj, options);

            Console.WriteLine($"--- DUMP ({obj.GetType().Name}) ---");
            Console.WriteLine(json);
            Console.WriteLine("---------------------------------");
        }

        // "Die" - Dừng chương trình
        Console.WriteLine("\n[Program terminated by dd()] Press any key to exit.");
        Console.ReadKey();
        Environment.Exit(0);
    }
}