using System.Globalization;
using System.Text;

namespace PZ21;

internal static class Program
{
    private static int Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
        CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("ru-RU");

        if (args.Contains("--selftest", StringComparer.OrdinalIgnoreCase))
        {
            SelfTest.Run();
            return 0;
        }

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("=== ПЗ21: перегрузка методов и конструкторов ===");
            Console.WriteLine("1  AverageMethods.Average для int[] и float[]");
            Console.WriteLine("2  Circle: четыре перегруженных конструктора");
            Console.WriteLine("0  Выход");
            Console.Write("Выберите пункт: ");
            var text = Console.ReadLine()?.Trim();
            if (text is null || text == "0")
                return 0;

            try
            {
                switch (text)
                {
                    case "1": TaskAverage(); break;
                    case "2": TaskCircle(); break;
                    default: Console.WriteLine("Введите 1, 2 или 0."); continue;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            Console.WriteLine("Нажмите Enter, чтобы вернуться в меню.");
            Console.ReadLine();
        }
    }

    private static void TaskAverage()
    {
        Console.WriteLine("Среднее арифметическое массива");
        Console.Write("Тип массива (1 — int, 2 — float): ");
        var type = Console.ReadLine()?.Trim();
        var count = ReadInt("Количество элементов (1..1000) = ", 1, 1000);

        if (type == "1")
        {
            var values = new int[count];
            for (var i = 0; i < count; i++)
                values[i] = ReadInt($"Элемент {i + 1}: ", int.MinValue, int.MaxValue);
            var average = AverageMethods.Average(values);
            Console.WriteLine($"Вызван Average(int[]): {average:0.############}");
        }
        else if (type == "2")
        {
            var values = new float[count];
            for (var i = 0; i < count; i++)
                values[i] = (float)ReadDouble($"Элемент {i + 1}: ");
            var average = AverageMethods.Average(values);
            Console.WriteLine($"Вызван Average(float[]): {average:0.############}");
        }
        else
        {
            Console.WriteLine("Нужно выбрать 1 или 2.");
        }
    }

    private static void TaskCircle()
    {
        Console.WriteLine("Четыре конструктора класса Circle:");
        var circles = new[]
        {
            new Circle(),
            new Circle(5),
            new Circle(10, 20),
            new Circle(10, 20, 7)
        };
        for (var i = 0; i < circles.Length; i++)
            Console.WriteLine($"  {i + 1}) {circles[i]}");
        Console.WriteLine("Конструкторы вызывают друг друга через this(...).");
    }

    private static int ReadInt(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt);
            var text = Console.ReadLine()?.Trim();
            if (int.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out var value) &&
                value >= min && value <= max)
                return value;
            Console.WriteLine($"Введите целое число от {min} до {max}.");
        }
    }

    private static double ReadDouble(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var text = Console.ReadLine()?.Trim()?.Replace(',', '.');
            if (double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out var value) &&
                double.IsFinite(value))
                return value;
            Console.WriteLine("Введите число.");
        }
    }
}
