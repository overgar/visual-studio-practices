using System.Numerics;

namespace PZ19;

internal static class Group1
{
    public static void Run(int task)
    {
        switch (task)
        {
            case 1: Task1(); break;
            case 2: Task2(); break;
            case 3: Task3(); break;
            case 4: Task4(); break;
            case 5: Task5(); break;
            case 6: Task6(); break;
            case 7: Task7(); break;
            case 8: Task8(); break;
            case 9: Task9(); break;
            case 10: Task10(); break;
            case 11: Task11(); break;
            case 12: Task12(); break;
            case 13: Task13(); break;
            case 14: Task14(); break;
            default: throw new ArgumentOutOfRangeException(nameof(task));
        }
    }

    private static void Task1()
    {
        Console.WriteLine("Решение квадратного уравнения ax^2 + bx + c = 0");
        var a = Input.Double("a = ");
        var b = Input.Double("b = ");
        var c = Input.Double("c = ");
        var (roots, message) = SolveQuadratic(a, b, c);
        Console.WriteLine(message);
        for (var i = 0; i < roots.Length; i++)
            Console.WriteLine($"x{i + 1} = {roots[i]:0.############}");
    }

    internal static (double[] Roots, string Message) SolveQuadratic(double a, double b, double c)
    {
        var scale = Math.Max(1.0, Math.Max(Math.Abs(a), Math.Max(Math.Abs(b), Math.Abs(c))));
        var eps = 1e-12 * scale;

        if (Math.Abs(a) <= eps)
        {
            if (Math.Abs(b) <= eps)
            {
                return Math.Abs(c) <= eps
                    ? (Array.Empty<double>(), "Уравнение является тождеством: любое x — решение.")
                    : (Array.Empty<double>(), "Уравнение не имеет решений.");
            }

            return (new[] { -c / b }, "Уравнение линейное, один корень:");
        }

        var discriminant = b * b - 4 * a * c;
        Console.WriteLine($"D = {discriminant:0.############}");

        if (discriminant < -eps)
            return (Array.Empty<double>(), "Дискриминант меньше нуля — действительных корней нет.");

        if (Math.Abs(discriminant) <= eps)
        {
            var x = -b / (2 * a);
            return (new[] { x }, "Дискриминант равен нулю, один корень:");
        }

        var sqrt = Math.Sqrt(discriminant);
        var x1 = (-b - sqrt) / (2 * a);
        var x2 = (-b + sqrt) / (2 * a);
        if (x1 > x2)
            (x1, x2) = (x2, x1);
        return (new[] { x1, x2 }, "Дискриминант положителен, два корня:");
    }

    private static void Task2()
    {
        Console.WriteLine("Площадь треугольника по формуле Герона");
        var a = Input.PositiveDouble("Сторона a = ");
        var b = Input.PositiveDouble("Сторона b = ");
        var c = Input.PositiveDouble("Сторона c = ");
        var area = HeronArea(a, b, c);
        Console.WriteLine($"Площадь S = {area:0.############}");
    }

    internal static double HeronArea(double a, double b, double c)
    {
        if (a <= 0 || b <= 0 || c <= 0)
            throw new ArgumentException("Стороны должны быть положительными.");
        if (a + b <= c + Geometry.Eps || a + c <= b + Geometry.Eps || b + c <= a + Geometry.Eps)
            throw new ArgumentException("Из таких сторон треугольник построить нельзя.");
        return Geometry.TriangleArea(a, b, c);
    }

    private static void Task3()
    {
        Console.WriteLine("Произведение нечётных целых чисел на отрезке");
        var min = Input.Int("n_min = ", int.MinValue + 1, int.MaxValue - 1);
        var max = Input.Int("n_max = ", int.MinValue + 1, int.MaxValue - 1);
        if (min > max)
        {
            (min, max) = (max, min);
            Console.WriteLine($"Границы переставлены: [{min}; {max}].");
        }

        var forResult = ProductOddFor(min, max);
        var whileResult = ProductOddWhile(min, max);
        var doResult = ProductOddDoWhile(min, max);
        Console.WriteLine($"for:      {forResult}");
        Console.WriteLine($"while:    {whileResult}");
        Console.WriteLine($"do-while: {doResult}");
        Console.WriteLine(forResult == whileResult && whileResult == doResult
            ? "Все три способа дали одинаковый результат."
            : "Результаты различаются — это ошибка.");
    }

    internal static BigInteger ProductOddFor(int min, int max)
    {
        var product = BigInteger.One;
        for (var i = min; i <= max; i++)
        {
            if (i % 2 != 0)
                product *= i;
            if (i == int.MaxValue)
                break;
        }
        return product;
    }

    internal static BigInteger ProductOddWhile(int min, int max)
    {
        var product = BigInteger.One;
        var i = min;
        while (i <= max)
        {
            if (i % 2 != 0)
                product *= i;
            if (i == int.MaxValue)
                break;
            i++;
        }
        return product;
    }

    internal static BigInteger ProductOddDoWhile(int min, int max)
    {
        var product = BigInteger.One;
        var i = min;
        do
        {
            if (i % 2 != 0)
                product *= i;
            if (i == int.MaxValue)
                break;
            i++;
        } while (i <= max);
        return product;
    }

    private static void Task4()
    {
        Console.WriteLine("Произведение целых чисел, делящихся на 4, на отрезке");
        var min = Input.Int("n_min = ", int.MinValue + 1, int.MaxValue - 1);
        var max = Input.Int("n_max = ", int.MinValue + 1, int.MaxValue - 1);
        if (min > max)
        {
            (min, max) = (max, min);
            Console.WriteLine($"Границы переставлены: [{min}; {max}].");
        }

        var product = ProductMultiplesOf4(min, max);
        Console.WriteLine(product == BigInteger.One
            ? "В интервале нет чисел, делящихся на 4; произведение пустого множества принято равным 1."
            : $"Произведение = {product}");
    }

    internal static BigInteger ProductMultiplesOf4(int min, int max)
    {
        var product = BigInteger.One;
        for (var i = min; i <= max; i++)
        {
            if (i % 4 == 0)
                product *= i;
            if (i == int.MaxValue)
                break;
        }
        return product;
    }

    private static void Task5()
    {
        Console.WriteLine("Сумма ряда 1 + 2 + 4 + ... + 2^N");
        var n = Input.Int("N (0..10000) = ", 0, 10_000);
        var loop = SumPowersOfTwoLoop(n);
        var formula = SumPowersOfTwoFormula(n);
        Console.WriteLine($"Циклом:  {loop}");
        Console.WriteLine($"Формулой (2^(N+1) - 1): {formula}");
        Console.WriteLine(loop == formula ? "Результаты совпадают." : "Результаты различаются — это ошибка.");
    }

    internal static BigInteger SumPowersOfTwoLoop(int n)
    {
        var sum = BigInteger.Zero;
        var term = BigInteger.One;
        for (var i = 0; i <= n; i++)
        {
            sum += term;
            term <<= 1;
        }
        return sum;
    }

    internal static BigInteger SumPowersOfTwoFormula(int n) => (BigInteger.One << (n + 1)) - 1;

    private static void Task6()
    {
        Console.WriteLine("Сумма ряда 1 + 1/2 + 1/4 + ... + 1/2^N");
        var n = Input.Int("N (0..10000) = ", 0, 10_000);
        var loop = SumPowersOfHalfLoop(n);
        var formula = SumPowersOfHalfFormula(n);
        Console.WriteLine($"Циклом:  {loop:0.################}");
        Console.WriteLine($"Формулой (2 - 1/2^N): {formula:0.################}");
        Console.WriteLine($"Разность: {Math.Abs(loop - formula):0.################}");
        Console.WriteLine("При N → ∞ сумма стремится к 2.");
    }

    internal static double SumPowersOfHalfLoop(int n)
    {
        var sum = 0.0;
        var term = 1.0;
        for (var i = 0; i <= n; i++)
        {
            sum += term;
            term /= 2;
        }
        return sum;
    }

    internal static double SumPowersOfHalfFormula(int n) => 2.0 - Math.Pow(2.0, -n);

    private static void Task7()
    {
        Console.WriteLine("cos(x) разложением в ряд");
        var x = Input.Double("x (радианы) = ");
        var series = CosSeries(x);
        var library = Math.Cos(x);
        Console.WriteLine($"cos({x:0.############}) ряд:      {series:0.################}");
        Console.WriteLine($"cos({x:0.############}) Math.Cos: {library:0.################}");
        Console.WriteLine($"Абсолютная разность: {Math.Abs(series - library):0.################}");
    }

    internal static double CosSeries(double x, double eps = 1e-15)
    {
        x -= 2 * Math.PI * Math.Round(x / (2 * Math.PI));
        var sum = 1.0;
        var term = 1.0;
        for (var k = 1; k <= 10_000; k++)
        {
            term *= -x * x / ((2 * k - 1) * (2.0 * k));
            sum += term;
            if (Math.Abs(term) < eps)
                break;
        }
        return sum;
    }

    private static void Task8()
    {
        Console.WriteLine("tg(x) через разложение sin(x) и cos(x) в ряды");
        var x = Input.Double("x (радианы) = ");
        var series = TanSeries(x);
        var library = Math.Tan(x);
        Console.WriteLine($"tg({x:0.############}) ряды:     {series:0.################}");
        Console.WriteLine($"tg({x:0.############}) Math.Tan: {library:0.################}");
        Console.WriteLine($"Абсолютная разность: {Math.Abs(series - library):0.################}");
    }

    internal static double TanSeries(double x, double eps = 1e-15)
    {
        var reduced = x - Math.PI * Math.Round(x / Math.PI);
        var sin = SinSeries(reduced, eps);
        var cos = CosSeries(reduced, eps);
        if (Math.Abs(cos) < 1e-14)
            throw new InvalidOperationException("Косинус близок к нулю: значение тангенса не определено.");
        return sin / cos;
    }

    internal static double SinSeries(double x, double eps = 1e-15)
    {
        x -= 2 * Math.PI * Math.Round(x / (2 * Math.PI));
        var sum = x;
        var term = x;
        for (var k = 1; k <= 10_000; k++)
        {
            term *= -x * x / ((2 * k) * (2.0 * k + 1));
            sum += term;
            if (Math.Abs(term) < eps)
                break;
        }
        return sum;
    }

    private static void Task9()
    {
        Console.WriteLine("Поиск a (1..100), при которых НОД(y1(a), y2(a)) > 1");
        Console.WriteLine("y1(a) = a^2(a^2 + 12) - 5,  y2(a) = a(a^2 + 1)");
        var found = new List<(int A, long Y1, long Y2, long Gcd)>();
        for (var a = 1; a <= 100; a++)
        {
            var y1 = (long)a * a * ((long)a * a + 12) - 5;
            var y2 = (long)a * ((long)a * a + 1);
            var gcd = Gcd(Math.Abs(y1), Math.Abs(y2));
            if (gcd > 1)
                found.Add((a, y1, y2, gcd));
        }

        if (found.Count == 0)
        {
            Console.WriteLine("Таких значений a нет.");
            return;
        }

        Console.WriteLine($"Найдено значений: {found.Count}");
        foreach (var item in found)
            Console.WriteLine($"a = {item.A,3}: y1 = {item.Y1,12}, y2 = {item.Y2,10}, НОД = {item.Gcd}");
    }

    internal static long Gcd(long a, long b)
    {
        a = Math.Abs(a);
        b = Math.Abs(b);
        while (b != 0)
            (a, b) = (b, a % b);
        return a;
    }

    private static void Task10()
    {
        Console.WriteLine("Разложение бинома Ньютона (x + a)^n");
        var n = Input.Int("n (0..100) = ", 0, 100);
        Console.WriteLine($"(x + a)^{n} = {BinomialExpansion(n)}");
    }

    internal static string BinomialExpansion(int n)
    {
        if (n < 0)
            throw new ArgumentOutOfRangeException(nameof(n));

        var terms = new List<string>();
        var coefficient = BigInteger.One;
        for (var k = 0; k <= n; k++)
        {
            terms.Add(FormatMonomial(coefficient, k, n - k));
            if (k < n)
                coefficient = coefficient * (n - k) / (k + 1);
        }
        return string.Join(" + ", terms);
    }

    private static string FormatMonomial(BigInteger coefficient, int xPower, int aPower)
    {
        var variables = string.Empty;
        if (xPower > 0)
            variables += xPower == 1 ? "x" : $"x^{xPower}";
        if (aPower > 0)
            variables += aPower == 1 ? "a" : $"a^{aPower}";

        if (variables.Length == 0)
            return coefficient.ToString();
        return coefficient.IsOne ? variables : coefficient + variables;
    }

    private static void Task11()
    {
        Console.WriteLine("Конвертор валют (switch-case)");
        Console.WriteLine("Доступные валюты: RUB, USD, EUR, CNY, KZT");
        var amount = Input.NonNegativeDouble("Сумма = ");
        var from = Input.Line("Из валюты: ").ToUpperInvariant();
        var to = Input.Line("В валюту: ").ToUpperInvariant();
        var result = ConvertCurrency(amount, from, to);
        Console.WriteLine($"{amount:0.##} {from} = {result:0.##} {to}");
        Console.WriteLine("Курсы условные и заданы в учебных целях.");
    }

    internal static double ConvertCurrency(double amount, string from, string to)
    {
        if (amount < 0)
            throw new ArgumentException("Сумма не может быть отрицательной.");

        var fromRate = CurrencyRate(from);
        var toRate = CurrencyRate(to);
        return amount * fromRate / toRate;
    }

    private static double CurrencyRate(string code) => code.ToUpperInvariant() switch
    {
        "RUB" => 1.0,
        "USD" => 90.0,
        "EUR" => 100.0,
        "CNY" => 12.5,
        "KZT" => 0.18,
        _ => throw new ArgumentException($"Неизвестная валюта: {code}.")
    };

    private static void Task12()
    {
        Console.WriteLine("Перевод целого положительного числа в системы счисления 2, 8 и 16");
        var value = Input.ULong("Число = ");
        Console.WriteLine($"{value}(10) = {ToBase(value, 2)}(2) = {ToBase(value, 8)}(8) = {ToBase(value, 16)}(16)");
    }

    internal static string ToBase(ulong value, int radix)
    {
        if (radix is < 2 or > 16)
            throw new ArgumentOutOfRangeException(nameof(radix), "Основание должно быть от 2 до 16.");
        if (value == 0)
            return "0";

        const string digits = "0123456789ABCDEF";
        var result = new Stack<char>();
        while (value > 0)
        {
            result.Push(digits[(int)(value % (ulong)radix)]);
            value /= (ulong)radix;
        }
        return new string(result.ToArray());
    }

    private static void Task13()
    {
        Console.WriteLine("Перевод шестнадцатеричного числа в десятичное");
        var text = Input.Line("Шестнадцатеричное число (например, A2F) = ");
        var value = ParseBase(text, 16);
        Console.WriteLine($"{text.Trim().ToUpperInvariant()}(16) = {value}(10)");
    }

    internal static ulong ParseBase(string text, int radix)
    {
        if (radix is < 2 or > 16)
            throw new ArgumentOutOfRangeException(nameof(radix), "Основание должно быть от 2 до 16.");

        text = text.Trim();
        if (text.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            text = text[2..];
        if (text.Length == 0)
            throw new FormatException("Пустая строка не является числом.");

        ulong result = 0;
        foreach (var ch in text)
        {
            int digit;
            if (ch >= '0' && ch <= '9')
                digit = ch - '0';
            else if (ch >= 'A' && ch <= 'F')
                digit = ch - 'A' + 10;
            else if (ch >= 'a' && ch <= 'f')
                digit = ch - 'a' + 10;
            else
                throw new FormatException($"Символ '{ch}' не является цифрой {radix}-й системы.");

            if (digit >= radix)
                throw new FormatException($"Цифра '{ch}' недопустима для основания {radix}.");
            result = checked(result * (ulong)radix + (ulong)digit);
        }
        return result;
    }

    private static void Task14()
    {
        Console.WriteLine("Число Фибоначчи и золотое сечение");
        var n = Input.Int("Номер члена N (1..1000) = ", 1, 1000);
        var fn = Fibonacci(n);
        Console.WriteLine($"F({n}) = {fn}");
        if (n >= 2)
            Console.WriteLine($"F({n}) / F({n - 1}) = {(double)fn / (double)Fibonacci(n - 1):0.################}");

        var phi = (1 + Math.Sqrt(5)) / 2;
        var found = FindGoldenThreshold(phi, 1e-10);
        if (found.N < 0)
        {
            Console.WriteLine("За 2000 членов требуемая точность не достигнута.");
            return;
        }

        Console.WriteLine($"Золотое сечение φ = {phi:0.################}");
        Console.WriteLine($"Начиная с N = {found.N}: |F(N)/F(N-1) - φ| = {found.Error:E3} < 1E-10");
        Console.WriteLine($"F({found.N}) = {found.Current}, F({found.N - 1}) = {found.Previous}");
    }

    internal static BigInteger Fibonacci(int n)
    {
        if (n < 0)
            throw new ArgumentOutOfRangeException(nameof(n));
        if (n == 0)
            return BigInteger.Zero;
        BigInteger previous = 0, current = 1;
        for (var i = 1; i < n; i++)
            (previous, current) = (current, previous + current);
        return current;
    }

    internal static (int N, BigInteger Previous, BigInteger Current, double Ratio, double Error) FindGoldenThreshold(
        double phi, double tolerance)
    {
        BigInteger previous = 1, current = 1;
        for (var n = 3; n <= 2000; n++)
        {
            var next = previous + current;
            var ratio = (double)next / (double)current;
            var error = Math.Abs(ratio - phi);
            if (error < tolerance)
                return (n, current, next, ratio, error);
            previous = current;
            current = next;
        }
        return (-1, 0, 0, 0, double.NaN);
    }
}
