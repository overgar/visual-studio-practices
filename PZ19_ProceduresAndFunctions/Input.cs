using System.Globalization;
using System.Text.RegularExpressions;

namespace PZ19;

internal static class Input
{
    private static readonly CultureInfo Inv = CultureInfo.InvariantCulture;
    private static readonly Regex TokenSplit = new(@"[\s;]+", RegexOptions.Compiled);

    public static string Line(string prompt, bool allowEmpty = false)
    {
        while (true)
        {
            Console.Write(prompt);
            var text = Console.ReadLine();
            if (text is null)
                throw new OperationCanceledException();
            text = text.Trim();
            if (allowEmpty || text.Length > 0)
                return text;
            Console.WriteLine("Строка не должна быть пустой.");
        }
    }

    public static int Int(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt);
            var text = Console.ReadLine()?.Trim();
            if (int.TryParse(text, NumberStyles.Integer, Inv, out var value) && value >= min && value <= max)
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
            if (long.TryParse(text, NumberStyles.Integer, Inv, out var value) && value >= min && value <= max)
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
            if (ulong.TryParse(text, NumberStyles.None, Inv, out var value))
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
            if (double.TryParse(text, NumberStyles.Float, Inv, out var value) && double.IsFinite(value))
                return value;
            Console.WriteLine("Введите конечное число (точка или запятая как разделитель).");
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

    public static double NonNegativeDouble(string prompt)
    {
        while (true)
        {
            var value = Double(prompt);
            if (value >= 0)
                return value;
            Console.WriteLine("Значение должно быть неотрицательным.");
        }
    }

    public static Point2 Point2(string prompt, int index)
    {
        while (true)
        {
            var text = Line(prompt);
            var tokens = TokenSplit.Split(text);
            if (tokens.Length >= 2 &&
                TryDouble(tokens[0], out var x) &&
                TryDouble(tokens[1], out var y))
            {
                return new Point2(x, y, index);
            }
            Console.WriteLine("Введите две координаты через пробел: x y.");
        }
    }

    public static Point3 Point3(string prompt, int index)
    {
        while (true)
        {
            var text = Line(prompt);
            var tokens = TokenSplit.Split(text);
            if (tokens.Length >= 3 &&
                TryDouble(tokens[0], out var x) &&
                TryDouble(tokens[1], out var y) &&
                TryDouble(tokens[2], out var z))
            {
                return new Point3(x, y, z, index);
            }
            Console.WriteLine("Введите три координаты через пробел: x y z.");
        }
    }

    public static (double A, double B) Pair(string prompt)
    {
        while (true)
        {
            var text = Line(prompt);
            var tokens = TokenSplit.Split(text);
            if (tokens.Length >= 2 && TryDouble(tokens[0], out var a) && TryDouble(tokens[1], out var b))
                return (a, b);
            Console.WriteLine("Введите два числа через пробел.");
        }
    }

    public static (double A, double B, double C) Triple(string prompt)
    {
        while (true)
        {
            var text = Line(prompt);
            var tokens = TokenSplit.Split(text);
            if (tokens.Length >= 3 &&
                TryDouble(tokens[0], out var a) &&
                TryDouble(tokens[1], out var b) &&
                TryDouble(tokens[2], out var c))
            {
                return (a, b, c);
            }
            Console.WriteLine("Введите три числа через пробел.");
        }
    }

    public static double[] Numbers(string prompt)
    {
        while (true)
        {
            var text = Line(prompt);
            var tokens = TokenSplit.Split(text);
            var result = new double[tokens.Length];
            var ok = true;
            for (var i = 0; i < tokens.Length; i++)
            {
                if (!TryDouble(tokens[i], out result[i]))
                {
                    ok = false;
                    break;
                }
            }

            if (ok && result.Length > 0)
                return result;
            Console.WriteLine("Введите числа через пробел или точку с запятой.");
        }
    }

    public static bool TryDouble(string text, out double value)
    {
        return double.TryParse(text.Trim().Replace(',', '.'), NumberStyles.Float, Inv, out value) &&
               double.IsFinite(value);
    }
}
