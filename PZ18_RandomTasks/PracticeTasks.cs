using System.Globalization;
using System.Numerics;
using System.Text;

internal static class PracticeTasks
{
    private const int PlotWidth = 61;
    private const int PlotHeight = 21;
    private const long MaximumIterations = 100_000_000;
    private const long OutputConfirmationThreshold = 1_000_000;

    public static string GetTitle(int task) => task switch
    {
        1 => "Максимум 5·cos(3x) на интервале",
        2 => "Количество счастливых билетов",
        3 => "Площадь под y = 7·sin(x)",
        4 => "Перевод unsigned long в шестнадцатеричную систему",
        5 => "Площадь под y = 2x + 2·sin(x/3)",
        6 => "Количество нулей в двоичной записи",
        7 => "Объём сферы методом Монте-Карло",
        8 => "График y = A·sin(ωx)",
        9 => "Перевод восьмеричной записи в десятичную",
        10 => "Окружность по заданному диаметру",
        11 => "Площадь между параболой и осью Ox методом Монте-Карло",
        12 => "Число членов ряда Лейбница для заданной точности",
        13 => "График y = A·exp(-x/τ)",
        14 => "Площадь первого положительного участка синусоиды методом Монте-Карло",
        15 => "Площадь окружности методом Монте-Карло с заданной точностью",
        16 => "Площадь под y = 3x² - 2x + 5",
        17 => "Ромб с горизонтальной диагональю D и вертикальной 2D",
        18 => "График y = Ax² + C",
        19 => "Все n-значные числа с заданной суммой цифр",
        20 => "Корень кубического уравнения методом Ньютона",
        21 => "Чётность количества единиц в двоичной записи",
        22 => "Все делители положительного числа",
        23 => "Все ulong с заданным количеством единичных битов",
        24 => "Вычисление X^Y без оператора умножения",
        _ => throw new ArgumentOutOfRangeException(nameof(task))
    };

    public static void Run(int task)
    {
        switch (task)
        {
            case 1: Task1(); break; case 2: Task2(); break; case 3: Task3(); break;
            case 4: Task4(); break; case 5: Task5(); break; case 6: Task6(); break;
            case 7: Task7(); break; case 8: Task8(); break; case 9: Task9(); break;
            case 10: Task10(); break; case 11: Task11(); break; case 12: Task12(); break;
            case 13: Task13(); break; case 14: Task14(); break; case 15: Task15(); break;
            case 16: Task16(); break; case 17: Task17(); break; case 18: Task18(); break;
            case 19: Task19(); break; case 20: Task20(); break; case 21: Task21(); break;
            case 22: Task22(); break; case 23: Task23(); break; case 24: Task24(); break;
            default: throw new ArgumentOutOfRangeException(nameof(task));
        }
    }

    private static void Task1()
    {
        var (start, end, step) = ReadInterval();
        var bestX = start;
        var bestY = 5 * Math.Cos(3 * start);
        foreach (var x in SampleInterval(start, end, step))
        {
            var y = 5 * Math.Cos(3 * x);
            if (y > bestY) { bestX = x; bestY = y; }
        }
        Console.WriteLine($"Максимум на выбранной сетке: y = {bestY:G10} при x = {bestX:G10}.");
        Console.WriteLine("Конечная точка включена; шаг задаёт точность поиска.");
    }

    private static void Task2()
    {
        var digits = ReadInt("Введите чётное количество цифр в билете: ");
        if (digits < 2 || digits % 2 != 0)
            throw new ArgumentException("Для билета с двумя равными половинами нужно положительное чётное число цифр.");
        var half = digits / 2;
        var ways = new BigInteger[1]; ways[0] = BigInteger.One;
        for (var position = 0; position < half; position++)
        {
            var next = new BigInteger[ways.Length + 9];
            for (var sum = 0; sum < ways.Length; sum++)
                for (var digit = 0; digit <= 9; digit++) next[sum + digit] += ways[sum];
            ways = next;
        }
        var tickets = BigInteger.Zero;
        foreach (var count in ways) tickets += count * count;
        Console.WriteLine($"Количество счастливых {digits}-значных билетов: {tickets:N0}.");
        Console.WriteLine("Ведущие нули учитываются, как в обычной нумерации билетов.");
    }

    private static void Task3()
    {
        var (a, b, h) = ReadInterval();
        PrintArea("7·sin(x)", IntegrateAbsolute(a, b, h, x => 7 * Math.Sin(x)));
    }

    private static void Task4()
    {
        var value = ReadULong("Введите число в десятичной форме (ulong): ");
        Console.WriteLine($"Шестнадцатеричное представление: {value.ToString("X", CultureInfo.InvariantCulture)}");
    }

    private static void Task5()
    {
        var (a, b, h) = ReadInterval();
        PrintArea("2x + 2·sin(x/3)", IntegrateAbsolute(a, b, h, x => 2 * x + 2 * Math.Sin(x / 3)));
    }

    private static void Task6()
    {
        var value = ReadULong("Введите число (ulong): ");
        var bits = value.ToString("B", CultureInfo.InvariantCulture);
        var bitLength = value == 0 ? 1 : 64 - BitOperations.LeadingZeroCount(value);
        var zeros = bitLength - BitOperations.PopCount(value);
        Console.WriteLine($"Двоичная запись: {bits}");
        Console.WriteLine($"Количество нулей без учёта ведущих нулей: {zeros}.");
    }

    private static void Task7()
    {
        var radius = ReadPositiveDouble("Введите радиус сферы R > 0: ");
        var iterations = ReadIterations();
        long inside = 0;
        var radiusSquared = radius * radius;
        for (long i = 0; i < iterations; i++)
        {
            var x = (2 * Random.Shared.NextDouble() - 1) * radius;
            var y = (2 * Random.Shared.NextDouble() - 1) * radius;
            var z = (2 * Random.Shared.NextDouble() - 1) * radius;
            if (x * x + y * y + z * z <= radiusSquared) inside++;
        }
        var p = (double)inside / iterations;
        var boxVolume = 8 * radius * radius * radius;
        var estimate = boxVolume * p;
        var se = boxVolume * Math.Sqrt(p * (1 - p) / iterations);
        Console.WriteLine($"Оценка объёма: {estimate:G10}.");
        Console.WriteLine($"Стандартная ошибка: {se:G6}; приближённая 95%-я погрешность: ±{1.96 * se:G6}.");
        Console.WriteLine($"Попаданий в сферу: {inside} из {iterations}.");
    }

    private static void Task8()
    {
        var amplitude = ReadDouble("Введите A: ");
        var omega = ReadDouble("Введите ω: ");
        var span = omega == 0 ? 10 : 2 * Math.PI / Math.Abs(omega);
        Console.WriteLine($"График на x ∈ [{-span:G5}; {span:G5}] (два периода при ω ≠ 0):");
        Plot(x => amplitude * Math.Sin(omega * x), -span, span);
    }

    private static void Task9()
    {
        Console.Write("Введите цифры числа в восьмеричной системе (0–7): ");
        var text = ReadRequiredLine();
        if (text.Length == 0 || text.Any(ch => ch is < '0' or > '7'))
            throw new ArgumentException("В восьмеричной записи допустимы только цифры от 0 до 7.");
        var value = BigInteger.Zero;
        foreach (var ch in text) value = value * 8 + (ch - '0');
        Console.WriteLine($"Десятичное представление: {value}.");
    }

    private static void Task10()
    {
        var diameter = ReadInt("Введите целый диаметр окружности: ");
        if (diameter < 1 || diameter > 100)
            throw new ArgumentException("Для экранного рисунка диаметр должен быть от 1 до 100.");
        var rows = diameter + 1;
        var columns = 2 * diameter + 1;
        var tolerance = 2.0 / diameter;
        for (var row = 0; row < rows; row++)
        {
            var y = 1 - 2.0 * row / diameter;
            var line = new StringBuilder(columns);
            for (var column = 0; column < columns; column++)
            {
                var x = (column - diameter) / (double)diameter;
                line.Append(Math.Abs(x * x + y * y - 1) <= tolerance ? '*' : ' ');
            }
            Console.WriteLine(line.ToString().TrimEnd());
        }
        Console.WriteLine("Граница нарисована символами *; ширина скорректирована под пропорции консольного символа.");
    }

    private static void Task11()
    {
        Console.WriteLine("Введите коэффициенты ax² + bx + c:");
        var a = ReadDouble("a: "); var b = ReadDouble("b: "); var c = ReadDouble("c: ");
        var iterations = ReadIterations();
        if (a == 0) throw new ArgumentException("Для параболы коэффициент a должен быть ненулевым.");
        var discriminant = b * b - 4 * a * c;
        if (discriminant <= 0)
            throw new ArgumentException("Нет двух различных пересечений с Ox, поэтому замкнутой конечной области нет.");
        var sqrt = Math.Sqrt(discriminant);
        var r1 = (-b - sqrt) / (2 * a); var r2 = (-b + sqrt) / (2 * a);
        if (r1 > r2) (r1, r2) = (r2, r1);
        double F(double x) => a * x * x + b * x + c;
        var maxY = Math.Abs(F(-b / (2 * a)));
        if (maxY == 0) { Console.WriteLine("Площадь равна 0."); return; }
        long hits = 0;
        for (long i = 0; i < iterations; i++)
        {
            var x = r1 + Random.Shared.NextDouble() * (r2 - r1);
            var y = Random.Shared.NextDouble() * maxY;
            if (y <= Math.Abs(F(x))) hits++;
        }
        var p = (double)hits / iterations;
        var rectangleArea = (r2 - r1) * maxY;
        var se = rectangleArea * Math.Sqrt(p * (1 - p) / iterations);
        Console.WriteLine($"Корни: {r1:G8} и {r2:G8}.");
        Console.WriteLine($"Оценка площади: {rectangleArea * p:G10}; приближённая 95%-я погрешность: ±{1.96 * se:G6}.");
    }

    private static void Task12()
    {
        var epsilon = ReadPositiveDouble("Введите точность ε > 0: ");
        var required = Math.Ceiling((4 / epsilon - 1) / 2);
        if (!double.IsFinite(required) || required > long.MaxValue)
            throw new ArgumentException("Требуемое число членов не помещается в Int64.");
        var terms = Math.Max(1L, (long)required);
        var remainderBound = 4.0 / (2.0 * terms + 1);
        Console.WriteLine($"Достаточно {terms:N0} членов ряда Лейбница.");
        Console.WriteLine($"Оценка остатка: {remainderBound:G8} ≤ ε = {epsilon:G8}.");
    }

    private static void Task13()
    {
        var amplitude = ReadDouble("Введите A: ");
        var tau = ReadPositiveDouble("Введите τ > 0: ");
        var xMax = 5 * tau;
        Console.WriteLine($"График на x ∈ [0; {xMax:G5}]:");
        Plot(x => amplitude * Math.Exp(-x / tau), 0, xMax);
    }

    private static void Task14()
    {
        var amplitude = ReadPositiveDouble("Введите A > 0: ");
        var omega = ReadPositiveDouble("Введите ω > 0: ");
        var alpha = ReadDouble("Введите α в радианах, 0 ≤ α ≤ π: ");
        if (alpha < 0 || alpha > Math.PI) throw new ArgumentException("α должен принадлежать [0; π].");
        var iterations = ReadIterations();
        var tEnd = (Math.PI - alpha) / omega;
        if (tEnd == 0) { Console.WriteLine("При α = π площадь равна 0."); return; }
        var yMax = amplitude * (alpha <= Math.PI / 2 ? 1 : Math.Sin(alpha));
        long hits = 0;
        for (long i = 0; i < iterations; i++)
        {
            var t = Random.Shared.NextDouble() * tEnd;
            var y = Random.Shared.NextDouble() * yMax;
            if (y <= amplitude * Math.Sin(omega * t + alpha)) hits++;
        }
        var p = (double)hits / iterations;
        var rectangleArea = tEnd * yMax;
        var se = rectangleArea * Math.Sqrt(p * (1 - p) / iterations);
        Console.WriteLine($"Оценка площади первого участка над Ox: {rectangleArea * p:G10}; 95%-я погрешность: ±{1.96 * se:G6}.");
        Console.WriteLine($"Попаданий под кривую: {hits} из {iterations}.");
    }

    private static void Task15()
    {
        var radius = ReadPositiveDouble("Введите радиус R > 0: ");
        var accuracy = ReadPositiveDouble("Введите абсолютную погрешность площади ε > 0: ");
        const long batchSize = 10_000, maxSamples = 10_000_000;
        var squareArea = 4 * radius * radius; var radiusSquared = radius * radius;
        long samples = 0, hits = 0; double estimate = 0, margin95 = double.PositiveInfinity;
        while (samples < maxSamples && margin95 > accuracy)
        {
            var batch = Math.Min(batchSize, maxSamples - samples);
            for (long i = 0; i < batch; i++)
            {
                var x = (2 * Random.Shared.NextDouble() - 1) * radius;
                var y = (2 * Random.Shared.NextDouble() - 1) * radius;
                if (x * x + y * y <= radiusSquared) hits++;
            }
            samples += batch;
            var p = (double)hits / samples;
            estimate = squareArea * p;
            margin95 = 1.96 * squareArea * Math.Sqrt(p * (1 - p) / samples);
        }
        Console.WriteLine($"Оценка площади: {estimate:G10}; число точек: {samples:N0}.");
        Console.WriteLine($"Приближённый 95%-й интервал: [{estimate - margin95:G10}; {estimate + margin95:G10}].");
        Console.WriteLine(margin95 <= accuracy
            ? $"Точность достигнута: полуширина интервала {margin95:G6} ≤ ε = {accuracy:G6}."
            : $"Достигнут предел {maxSamples:N0} точек; точность не подтверждена (полуширина {margin95:G6}).");
    }

    private static void Task16()
    {
        var (a, b, h) = ReadInterval();
        PrintArea("3x² - 2x + 5", IntegrateAbsolute(a, b, h, x => 3 * x * x - 2 * x + 5));
    }

    private static void Task17()
    {
        var d = ReadInt("Введите меньшую горизонтальную диагональ D: ");
        if (d < 1 || d > 100) throw new ArgumentException("Для экранного рисунка D должен быть от 1 до 100.");
        for (var row = -d; row <= d; row++)
        {
            var halfWidth = d - Math.Abs(row);
            var line = new StringBuilder(2 * d + 1);
            for (var column = -d; column <= d; column++)
                line.Append(Math.Abs(column) == halfWidth ? '*' : ' ');
            Console.WriteLine(line.ToString().TrimEnd());
        }
        Console.WriteLine($"Горизонтальная диагональ D={d}; вертикальная диагональ 2D={2 * d}.");
    }

    private static void Task18()
    {
        var a = ReadDouble("Введите A: "); var c = ReadDouble("Введите C: ");
        Console.WriteLine("График y = Ax² + C на x ∈ [-10; 10] (диапазон выбран, поскольку в условии он не указан):");
        Plot(x => a * x * x + c, -10, 10);
    }

    private static void Task19()
    {
        var digits = ReadInt("Введите количество цифр n: "); var targetSum = ReadInt("Введите сумму цифр S: ");
        if (digits < 1 || digits > 100) throw new ArgumentException("Задайте количество цифр от 1 до 100.");
        var count = CountNumbersWithDigitSum(digits, targetSum);
        Console.WriteLine($"Подходящих {digits}-значных чисел: {count:N0}.");
        if (count.IsZero) { Console.WriteLine("Таких чисел нет."); return; }
        if (count > OutputConfirmationThreshold && !ConfirmLargeOutput(count))
        { Console.WriteLine("Перечисление отменено; количество решений вычислено точно."); return; }
        var path = Path.Combine(Environment.CurrentDirectory, "task19_numbers.txt");
        var buffer = new char[digits]; BigInteger written = BigInteger.Zero;
        using (var writer = new StreamWriter(path, false, new UTF8Encoding(false)))
        {
            void Generate(int position, int remainingSum)
            {
                if (position == digits)
                {
                    if (remainingSum == 0) { writer.WriteLine(buffer); written++; }
                    return;
                }
                var minimumDigit = position == 0 ? 1 : 0;
                var positionsLeft = digits - position - 1;
                for (var digit = minimumDigit; digit <= 9 && digit <= remainingSum; digit++)
                {
                    var rest = remainingSum - digit;
                    if (rest > 9 * positionsLeft) continue;
                    buffer[position] = (char)('0' + digit);
                    Generate(position + 1, rest);
                }
            }
            Generate(0, targetSum);
        }
        if (written != count) throw new InvalidOperationException($"Ожидалось {count} чисел, записано {written}.");
        Console.WriteLine($"Все числа записаны в {path}.");
    }

    private static void Task20()
    {
        Console.WriteLine("Введите коэффициенты ax³ + bx² + cx + d = 0:");
        var a = ReadDouble("a: "); var b = ReadDouble("b: "); var c = ReadDouble("c: "); var d = ReadDouble("d: ");
        var epsilon = ReadPositiveDouble("Введите точность ε > 0: ");
        if (a == 0) throw new ArgumentException("Коэффициент a должен быть ненулевым.");
        var root = FindCubicRootNewton(a, b, c, d, epsilon);
        var residual = ((a * root + b) * root + c) * root + d;
        Console.WriteLine($"Найденный действительный корень: x ≈ {root:G12}.");
        Console.WriteLine($"Невязка |f(x)| ≈ {Math.Abs(residual):G6}; точность ε = {epsilon:G6}.");
    }

    private static void Task21()
    {
        var value = ReadULong("Введите число (ulong): ");
        var ones = BitOperations.PopCount(value);
        Console.WriteLine($"Количество единиц в двоичной записи: {ones}.");
        Console.WriteLine(ones % 2 == 0 ? "Количество единиц чётное." : "Количество единиц нечётное.");
    }

    private static void Task22()
    {
        var number = ReadULong("Введите целое положительное число: ");
        if (number == 0) throw new ArgumentException("Число должно быть положительным.");
        var low = new List<ulong>(); var high = new List<ulong>();
        for (ulong divisor = 1; divisor <= number / divisor; divisor++)
        {
            if (number % divisor != 0) continue;
            low.Add(divisor);
            var paired = number / divisor;
            if (paired != divisor) high.Add(paired);
        }
        high.Reverse(); low.AddRange(high);
        Console.WriteLine($"Делители числа {number}: {string.Join(", ", low)}");
    }

    private static void Task23()
    {
        var ones = ReadInt("Введите количество единичных битов (0–64): ");
        if (ones is < 0 or > 64) throw new ArgumentException("Для ulong количество единиц должно быть от 0 до 64.");
        var count = Binomial(64, ones);
        Console.WriteLine($"Всего значений ulong с {ones} единицами: {count:N0}.");
        if (count > OutputConfirmationThreshold && !ConfirmLargeOutput(count))
        { Console.WriteLine("Перечисление отменено; количество значений вычислено точно."); return; }
        var path = Path.Combine(Environment.CurrentDirectory, "task23_ulong_values.txt");
        BigInteger written = BigInteger.Zero;
        using (var writer = new StreamWriter(path, false, new UTF8Encoding(false)))
        {
            void Generate(int bit, int remaining, ulong value)
            {
                if (remaining < 0 || remaining > 64 - bit) return;
                if (bit == 64)
                {
                    if (remaining == 0) { writer.WriteLine(value.ToString(CultureInfo.InvariantCulture)); written++; }
                    return;
                }
                Generate(bit + 1, remaining, value);
                if (remaining > 0) Generate(bit + 1, remaining - 1, value | (1UL << bit));
            }
            Generate(0, ones, 0);
        }
        if (written != count) throw new InvalidOperationException($"Ожидалось {count} значений, записано {written}.");
        Console.WriteLine($"Все значения записаны в {path}.");
    }

    private static void Task24()
    {
        var x = ReadBigInteger("Введите целое неотрицательное X: "); var y = ReadBigInteger("Введите целое неотрицательное Y: ");
        if (x < 0 || y < 0) throw new ArgumentException("X и Y должны быть неотрицательными.");
        var result = BigInteger.One; var factor = x; var exponent = y;
        while (exponent > 0)
        {
            if (!exponent.IsEven) result = MultiplyUsingAddition(result, factor);
            exponent >>= 1;
            if (exponent > 0) factor = MultiplyUsingAddition(factor, factor);
        }
        Console.WriteLine($"{x}^{y} = {result}");
        Console.WriteLine("В вычислении степени произведение реализовано только через двоичное удвоение и сложение.");
    }

    private static (double Start, double End, double Step) ReadInterval()
    {
        var start = ReadDouble("Начало интервала a: "); var end = ReadDouble("Конец интервала b: ");
        var step = ReadPositiveDouble("Шаг h > 0: ");
        if (end < start) throw new ArgumentException("Конец интервала должен быть не меньше начала.");
        return (start, end, step);
    }

    private static IEnumerable<double> SampleInterval(double start, double end, double step)
    {
        if (start == end) yield break;
        var countDouble = Math.Floor((end - start) / step);
        if (!double.IsFinite(countDouble) || countDouble > 5_000_000)
            throw new ArgumentException("Шаг слишком мал: число точек превысило 5 миллионов.");
        var count = (long)countDouble;
        for (long i = 1; i <= count; i++)
        {
            var x = start + i * step;
            if (x < end) yield return x;
        }
        yield return end;
    }

    private static double IntegrateAbsolute(double start, double end, double step, Func<double, double> function)
    {
        if (start == end) return 0;
        var segmentsDouble = Math.Ceiling((end - start) / step);
        if (!double.IsFinite(segmentsDouble) || segmentsDouble > 5_000_000)
            throw new ArgumentException("Шаг слишком мал: число сегментов превысило 5 миллионов.");
        var area = 0.0; var segments = (long)segmentsDouble;
        for (long i = 0; i < segments; i++)
        {
            var x0 = start + i * step; var x1 = Math.Min(end, x0 + step);
            area += (Math.Abs(function(x0)) + Math.Abs(function(x1))) * (x1 - x0) / 2;
        }
        return area;
    }

    private static void PrintArea(string name, double area) =>
        Console.WriteLine($"Приближённая геометрическая площадь под |{name}|: {area:G12} (метод трапеций).");

    private static long ReadIterations()
    {
        var value = ReadLong($"Введите число итераций N (1–{MaximumIterations:N0}): ");
        if (value < 1 || value > MaximumIterations) throw new ArgumentException($"N должно быть от 1 до {MaximumIterations:N0}.");
        return value;
    }

    private static double ReadDouble(string prompt)
    {
        Console.Write(prompt);
        var text = ReadRequiredLine().Replace(',', '.');
        if (!double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out var value) || !double.IsFinite(value))
            throw new ArgumentException("Введите конечное число.");
        return value;
    }

    private static double ReadPositiveDouble(string prompt)
    {
        var value = ReadDouble(prompt);
        if (value <= 0) throw new ArgumentException("Значение должно быть положительным.");
        return value;
    }

    private static int ReadInt(string prompt)
    {
        Console.Write(prompt);
        if (!int.TryParse(ReadRequiredLine(), NumberStyles.Integer, CultureInfo.InvariantCulture, out var value))
            throw new ArgumentException("Введите целое число.");
        return value;
    }

    private static long ReadLong(string prompt)
    {
        Console.Write(prompt);
        if (!long.TryParse(ReadRequiredLine(), NumberStyles.Integer, CultureInfo.InvariantCulture, out var value))
            throw new ArgumentException("Введите целое число в диапазоне Int64.");
        return value;
    }

    private static ulong ReadULong(string prompt)
    {
        Console.Write(prompt);
        if (!ulong.TryParse(ReadRequiredLine(), NumberStyles.Integer, CultureInfo.InvariantCulture, out var value))
            throw new ArgumentException("Введите число в диапазоне UInt64.");
        return value;
    }

    private static BigInteger ReadBigInteger(string prompt)
    {
        Console.Write(prompt);
        if (!BigInteger.TryParse(ReadRequiredLine(), NumberStyles.Integer, CultureInfo.InvariantCulture, out var value))
            throw new ArgumentException("Введите целое число.");
        return value;
    }

    private static string ReadRequiredLine() => Console.ReadLine()?.Trim() ?? throw new OperationCanceledException();

    private static void Plot(Func<double, double> function, double xMin, double xMax)
    {
        if (!double.IsFinite(xMin) || !double.IsFinite(xMax) || xMax <= xMin)
            throw new ArgumentException("Невозможно построить график на заданном диапазоне.");
        var values = new double[PlotWidth];
        for (var column = 0; column < PlotWidth; column++)
        {
            var x = xMin + (xMax - xMin) * column / (PlotWidth - 1);
            values[column] = function(x);
            if (!double.IsFinite(values[column])) throw new ArgumentException("Значения функции не помещаются в double.");
        }
        var yMin = values.Min(); var yMax = values.Max();
        if (Math.Abs(yMax - yMin) < 1e-12)
        {
            var margin = Math.Max(1, Math.Abs(yMax) * 0.1); yMin -= margin; yMax += margin;
        }
        else
        {
            var margin = (yMax - yMin) * 0.08; yMin -= margin; yMax += margin;
        }
        var grid = new char[PlotHeight, PlotWidth];
        for (var row = 0; row < PlotHeight; row++)
            for (var column = 0; column < PlotWidth; column++) grid[row, column] = ' ';
        int? zeroRow = 0 >= yMin && 0 <= yMax ? (int)Math.Round(yMax / (yMax - yMin) * (PlotHeight - 1)) : null;
        int? zeroColumn = xMin <= 0 && 0 <= xMax ? (int)Math.Round(-xMin / (xMax - xMin) * (PlotWidth - 1)) : null;
        if (zeroRow is int xAxis) for (var column = 0; column < PlotWidth; column++) grid[xAxis, column] = '-';
        if (zeroColumn is int yAxis) for (var row = 0; row < PlotHeight; row++) grid[row, yAxis] = '|';
        if (zeroRow is int crossRow && zeroColumn is int crossColumn) grid[crossRow, crossColumn] = '+';
        for (var column = 0; column < PlotWidth; column++)
        {
            var row = (int)Math.Round((yMax - values[column]) / (yMax - yMin) * (PlotHeight - 1));
            grid[Math.Clamp(row, 0, PlotHeight - 1), column] = '*';
        }
        for (var row = 0; row < PlotHeight; row++)
        {
            for (var column = 0; column < PlotWidth; column++) Console.Write(grid[row, column]);
            Console.WriteLine();
        }
        Console.WriteLine($"x: {xMin:G5} … {xMax:G5}; y: {values.Min():G5} … {values.Max():G5}; * — график, оси — | и -.");
    }

    private static bool ConfirmLargeOutput(BigInteger count)
    {
        Console.WriteLine($"Будет создан файл на {count:N0} строк.");
        Console.Write("Продолжить? (y/n): ");
        return string.Equals(ReadRequiredLine(), "y", StringComparison.OrdinalIgnoreCase);
    }

    private static BigInteger CountNumbersWithDigitSum(int digits, int targetSum)
    {
        if (targetSum < 1 || targetSum > 9 * digits) return BigInteger.Zero;
        var ways = new BigInteger[targetSum + 1];
        for (var first = 1; first <= 9 && first <= targetSum; first++) ways[first]++;
        for (var position = 1; position < digits; position++)
        {
            var next = new BigInteger[targetSum + 1];
            for (var sum = 0; sum <= targetSum; sum++)
            {
                if (ways[sum].IsZero) continue;
                for (var digit = 0; digit <= 9 && sum + digit <= targetSum; digit++) next[sum + digit] += ways[sum];
            }
            ways = next;
        }
        return ways[targetSum];
    }

    private static double FindCubicRootNewton(double a, double b, double c, double d, double epsilon)
    {
        double F(double x) => ((a * x + b) * x + c) * x + d;
        double Derivative(double x) => (3 * a * x + 2 * b) * x + c;
        var bound = 1 + Math.Max(Math.Abs(b / a), Math.Max(Math.Abs(c / a), Math.Abs(d / a)));
        if (!double.IsFinite(bound)) throw new ArgumentException("Коэффициенты слишком велики для вычислений.");
        var points = new List<double> { -bound, bound };
        var qa = 3 * a; var qb = 2 * b; var qc = c;
        var disc = qb * qb - 4 * qa * qc;
        if (disc >= 0 && double.IsFinite(disc))
        {
            var rootDisc = Math.Sqrt(disc);
            var c1 = (-qb - rootDisc) / (2 * qa); var c2 = (-qb + rootDisc) / (2 * qa);
            if (c1 > -bound && c1 < bound) points.Add(c1);
            if (c2 > -bound && c2 < bound) points.Add(c2);
        }
        points = points.Distinct().OrderBy(x => x).ToList();
        foreach (var point in points) if (Math.Abs(F(point)) <= epsilon) return point;
        for (var i = 0; i < points.Count - 1; i++)
        {
            var left = points[i]; var right = points[i + 1];
            var fLeft = F(left); var fRight = F(right);
            if (Math.Sign(fLeft) == Math.Sign(fRight)) continue;
            var x = (left + right) / 2;
            for (var iteration = 0; iteration < 10_000; iteration++)
            {
                var fx = F(x);
                if (Math.Abs(fx) <= epsilon) return x;
                var derivative = Derivative(x);
                var candidate = derivative == 0 ? double.NaN : x - fx / derivative;
                if (!double.IsFinite(candidate) || candidate <= left || candidate >= right) candidate = (left + right) / 2;
                var fCandidate = F(candidate);
                if (Math.Abs(candidate - x) <= epsilon || Math.Abs(fCandidate) <= epsilon) return candidate;
                if (Math.Sign(fCandidate) == Math.Sign(fLeft)) { left = candidate; fLeft = fCandidate; }
                else { right = candidate; fRight = fCandidate; }
                x = candidate;
            }
            return x;
        }
        throw new InvalidOperationException("Не удалось найти действительный корень с заданной точностью.");
    }

    private static BigInteger Binomial(int n, int k)
    {
        k = Math.Min(k, n - k);
        var result = BigInteger.One;
        for (var i = 1; i <= k; i++) result = result * (n - k + i) / i;
        return result;
    }

    private static BigInteger MultiplyUsingAddition(BigInteger left, BigInteger right)
    {
        var result = BigInteger.Zero; var addend = left; var multiplier = right;
        while (multiplier > 0)
        {
            if (!multiplier.IsEven) result += addend;
            multiplier >>= 1;
            if (multiplier > 0) addend += addend;
        }
        return result;
    }
}
