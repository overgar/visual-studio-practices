using System.Globalization;
using System.Text;

namespace PZ20;

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
            Console.WriteLine("=== ПЗ20: рекурсивные функции ===");
            Console.WriteLine("1  Сумма последовательности до 0 (без циклов)");
            Console.WriteLine("2  Факториал: рекурсия и цикл");
            Console.WriteLine("3  Числа Фибоначчи: рекурсия и цикл");
            Console.WriteLine("0  Выход");
            Console.Write("Выберите пункт: ");
            var text = Console.ReadLine()?.Trim();
            if (text is null || text == "0")
                return 0;

            try
            {
                switch (text)
                {
                    case "1": TaskSum(); break;
                    case "2": TaskFactorial(); break;
                    case "3": TaskFibonacci(); break;
                    default: Console.WriteLine("Введите 1, 2, 3 или 0."); continue;
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

    private static void TaskSum()
    {
        Console.WriteLine("Вводите целые числа по одному. Число 0 завершает последовательность.");
        var sum = ReadSum();
        Console.WriteLine($"Сумма: {sum}");
    }

    private static long ReadSum()
    {
        while (true)
        {
            Console.Write("Число (0 — конец): ");
            var text = Console.ReadLine()?.Trim();
            if (long.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out var value))
            {
                if (value == 0)
                    return 0;
                return checked(value + ReadSum());
            }
            Console.WriteLine("Введите целое число.");
        }
    }

    private static void TaskFactorial()
    {
        var n = ReadInt("n (0..1000) = ", 0, 1000);
        var recursive = Recursion.Factorial(n);
        var iterative = Recursion.FactorialIterative(n);
        Console.WriteLine($"{n}! рекурсией: {recursive}");
        Console.WriteLine($"{n}! циклом:    {iterative}");
        Console.WriteLine(recursive == iterative ? "Результаты совпадают." : "Результаты различаются.");
    }

    private static void TaskFibonacci()
    {
        var n = ReadInt("n (0..35) = ", 0, 35);
        var recursive = Recursion.Fibonacci(n);
        var iterative = Recursion.FibonacciIterative(n);
        Console.WriteLine($"F({n}) рекурсией: {recursive}");
        Console.WriteLine($"F({n}) циклом:    {iterative}");
        Console.WriteLine(recursive == iterative ? "Результаты совпадают." : "Результаты различаются.");
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
}
