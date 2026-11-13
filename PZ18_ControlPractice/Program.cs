using System.Globalization;
using System.Text;

internal static class Program
{
    private static readonly CultureInfo Ru = CultureInfo.GetCultureInfo("ru-RU");

    private static int Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
        CultureInfo.CurrentCulture = Ru;

        while (true)
        {
            PrintMenu();
            Console.Write("Выберите задание (0 — выход): ");
            var text = Console.ReadLine();
            if (text is null || text.Trim() == "0")
                return 0;
            if (!int.TryParse(text.Trim(), out var task) || task is < 1 or > 24)
            {
                Console.WriteLine("Введите номер от 1 до 24 или 0 для выхода.");
                continue;
            }

            try
            {
                Exercises.Run(task);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Операция отменена.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            Console.WriteLine();
            Console.WriteLine("Нажмите Enter, чтобы вернуться в меню.");
            Console.ReadLine();
        }
    }

    private static void PrintMenu()
    {
        Console.WriteLine("=== ПЗ18: контрольная практическая работа ===");
        Console.WriteLine(" 1  Максимум y = 5 cos(3x) на интервале");
        Console.WriteLine(" 2  Количество счастливых билетов");
        Console.WriteLine(" 3  Площадь под y = 7 sin(x)");
        Console.WriteLine(" 4  Перевод ulong в шестнадцатеричную систему");
        Console.WriteLine(" 5  Площадь под y = 2x + 2 sin(x/3)");
        Console.WriteLine(" 6  Число нулей в двоичной записи ulong");
        Console.WriteLine(" 7  Объём сферы методом Монте-Карло");
        Console.WriteLine(" 8  График y = A sin(ωx)");
        Console.WriteLine(" 9  Перевод восьмеричного числа в десятичное");
        Console.WriteLine("10  Окружность по заданному диаметру");
        Console.WriteLine("11  Площадь между параболой и осью Ox (Монте-Карло)");
        Console.WriteLine("12  Число членов ряда Лейбница для заданной точности π");
        Console.WriteLine("13  График y = A exp(-x/τ)");
        Console.WriteLine("14  Площадь полуволны синусоиды (Монте-Карло)");
        Console.WriteLine("15  Площадь окружности с заданной точностью (Монте-Карло)");
        Console.WriteLine("16  Площадь под y = 3x² - 2x + 5");
        Console.WriteLine("17  Ромб с диагоналями d и 2d");
        Console.WriteLine("18  График y = Ax² + C");
        Console.WriteLine("19  Числа заданной длины с заданной суммой цифр");
        Console.WriteLine("20  Корень кубического уравнения (метод Ньютона)");
        Console.WriteLine("21  Чётность количества единиц в двоичной записи");
        Console.WriteLine("22  Все делители положительного числа");
        Console.WriteLine("23  Значения ulong с заданным числом единичных битов");
        Console.WriteLine("24  X^Y без оператора умножения");
        Console.WriteLine(" 0  Выход");
    }
}

internal static class Input
{
    private static readonly CultureInfo Invariant = CultureInfo.InvariantCulture;

    public static int Int(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt);
            var text = Console.ReadLine()?.Trim();
            if (int.TryParse(text, NumberStyles.Integer, Invariant, out var value) && value >= min && value <= max)
                return value;
            Console.WriteLine($"Введите целое число от {min} до {max}.");
        }
    }

    public static long Long(string prompt, long min, long max)
    {
        while (true)
        {
            Console.Write(prompt);
            var text = Console.ReadLine()?.Trim();
            if (long.TryParse(text, NumberStyles.Integer, Invariant, out var value) && value >= min && value <= max)
                return value;
            Console.WriteLine($"Введите целое число от {min} до {max}.");
        }
    }

    public static ulong ULong(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var text = Console.ReadLine()?.Trim();
            if (ulong.TryParse(text, NumberStyles.None, Invariant, out var value))
                return value;
            Console.WriteLine("Введите неотрицательное целое число в диапазоне UInt64.");
        }
    }

    public static double Double(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var text = Console.ReadLine()?.Trim()?.Replace(',', '.');
            if (double.TryParse(text, NumberStyles.Float, Invariant, out var value) && double.IsFinite(value))
                return value;
            Console.WriteLine("Введите конечное число; десятичный разделитель — точка или запятая.");
        }
    }

    public static double PositiveDouble(string prompt)
    {
        while (true)
        {
            var value = Double(prompt);
            if (value > 0)
                return value;
            Console.WriteLine("Значение должно быть больше нуля.");
        }
    }

    public static int Iterations(string prompt = "Количество итераций: ") => Int(prompt, 1, 50_000_000);

    public static (double A, double B, double Step) IntervalAndStep()
    {
        while (true)
        {
            var a = Double("Начало интервала a: ");
            var b = Double("Конец интервала b: ");
            var step = PositiveDouble("Шаг h: ");
            if (b > a)
                return (a, b, step);
            Console.WriteLine("Конец интервала должен быть больше начала.");
        }
    }
}
