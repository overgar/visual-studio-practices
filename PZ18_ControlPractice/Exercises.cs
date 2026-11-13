using System.Globalization;
using System.Numerics;
using System.Text;

internal static class Exercises
{
    private const double Pi = Math.PI;
    private const int MaxGridPoints = 10_000_000;
    private const int MaxEnumeration = 1_000_000;
    private static readonly CultureInfo Ru = CultureInfo.GetCultureInfo("ru-RU");

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
            case 15: Task15(); break;
            case 16: Task16(); break;
            case 17: Task17(); break;
            case 18: Task18(); break;
            case 19: Task19(); break;
            case 20: Task20(); break;
            case 21: Task21(); break;
            case 22: Task22(); break;
            case 23: Task23(); break;
            case 24: Task24(); break;
            default: throw new ArgumentOutOfRangeException(nameof(task));
        }
    }

    // 1. Максимум y = 5 cos(3x) на интервале.
    private static void Task1()
    {
        Console.WriteLine("Задание 1. Максимум y = 5 cos(3x)");
        var (a, b, h) = Input.IntervalAndStep();
        var count = SegmentCount(a, b, h);
        var bestX = a;
        var bestY = 5 * Math.Cos(3 * a);
        for (var i = 1; i <= count; i++)
        {
            var x = Math.Min(a + i * h, b);
            var y = 5 * Math.Cos(3 * x);
            if (y > bestY)
            {
                bestX = x;
                bestY = y;
            }
        }
        Console.WriteLine($"Максимум на узлах заданного шага: y = {F(bestY)}, x = {F(bestX)}.");
    }

    // 2. Число счастливых билетов с одинаковой суммой цифр половин.
    private static void Task2()
    {
        Console.WriteLine("Задание 2. Количество счастливых билетов (ведущие нули разрешены)");
        var digits = Input.Int("Чётное число цифр в билете: ", 2, 100);
        if (digits % 2 != 0)
        {
            Console.WriteLine("У счастливого билета должно быть чётное количество цифр.");
            return;
        }

        var half = digits / 2;
        var ways = new BigInteger[9 * half + 1];
        ways[0] = BigInteger.One;
        var maxSum = 0;
        for (var position = 0; position < half; position++)
        {
            var next = new BigInteger[ways.Length];
            for (var sum = 0; sum <= maxSum; sum++)
                for (var digit = 0; digit <= 9; digit++)
                    next[sum + digit] += ways[sum];
            ways = next;
            maxSum += 9;
        }

        var total = BigInteger.Zero;
        for (var sum = 0; sum < ways.Length; sum++)
            total += ways[sum] * ways[sum];
        Console.WriteLine($"Количество {digits}-значных счастливых билетов: {total}.");
    }

    // 3. Площадь под y = 7 sin(x), рассчитанная методом трапеций для |y|.
    private static void Task3()
    {
        Console.WriteLine("Задание 3. Площадь под y = 7 sin(x) (составной метод трапеций)");
        var (a, b, h) = Input.IntervalAndStep();
        var area = IntegrateAbsolute(x => 7 * Math.Sin(x), a, b, h);
        Console.WriteLine($"Приближённая геометрическая площадь: {F(area)}.");
    }

    // 4. Представление ulong в шестнадцатеричной системе.
    private static void Task4()
    {
        Console.WriteLine("Задание 4. Шестнадцатеричная запись ulong");
        var value = Input.ULong("Введите число: ");
        Console.WriteLine($"Шестнадцатеричное представление: 0x{value:X}.");
    }

    // 5. Площадь под y = 2x + 2 sin(x/3).
    private static void Task5()
    {
        Console.WriteLine("Задание 5. Площадь под y = 2x + 2 sin(x/3)");
        var (a, b, h) = Input.IntervalAndStep();
        var area = IntegrateAbsolute(x => 2 * x + 2 * Math.Sin(x / 3), a, b, h);
        Console.WriteLine($"Приближённая геометрическая площадь: {F(area)}.");
    }

    // 6. Число нулей в стандартной двоичной записи ulong без ведущих нулей.
    private static void Task6()
    {
        Console.WriteLine("Задание 6. Количество нулей в двоичной записи ulong (без ведущих нулей)");
        var value = Input.ULong("Введите число: ");
        var bitLength = value == 0 ? 1 : 64 - BitOperations.LeadingZeroCount(value);
        var zeros = bitLength - BitOperations.PopCount(value);
        Console.WriteLine($"Двоичная запись: {ConvertToBinary(value)}");
        Console.WriteLine($"Количество нулей: {zeros}.");
    }

    // 7. Объём сферы методом Монте-Карло и ошибка относительно точного объёма.
    private static void Task7()
    {
        Console.WriteLine("Задание 7. Объём сферы методом Монте-Карло");
        var radius = Input.PositiveDouble("Радиус сферы: ");
        var iterations = Input.Iterations();
        if (radius > 1e100)
        {
            Console.WriteLine("Слишком большой радиус для вычислений double.");
            return;
        }

        var r2 = radius * radius;
        var inside = 0;
        for (var i = 0; i < iterations; i++)
        {
            var x = (2 * Random.Shared.NextDouble() - 1) * radius;
            var y = (2 * Random.Shared.NextDouble() - 1) * radius;
            var z = (2 * Random.Shared.NextDouble() - 1) * radius;
            if (x * x + y * y + z * z <= r2)
                inside++;
        }

        var estimate = 8 * radius * radius * radius * inside / iterations;
        var exact = 4 * Pi * radius * radius * radius / 3;
        var error = Math.Abs(estimate - exact);
        var relative = exact == 0 ? 0 : 100 * error / exact;
        Console.WriteLine($"Оценка объёма: {F(estimate)}; точное значение для сравнения: {F(exact)}.");
        Console.WriteLine($"Абсолютная ошибка: {F(error)}; относительная ошибка: {F(relative)}% (N={iterations}).");
    }

    // 8. Текстовый график y = A sin(ωx) на фиксированном диапазоне [-2π, 2π].
    private static void Task8()
    {
        Console.WriteLine("Задание 8. График y = A sin(ωx), диапазон x = [-2π, 2π]");
        var amplitude = Input.Double("A: ");
        var omega = Input.Double("ω: ");
        DrawPlot(x => amplitude * Math.Sin(omega * x), -2 * Pi, 2 * Pi, "y = A sin(ωx)");
    }

    // 9. Восьмеричная строка с проверкой переполнения ulong.
    private static void Task9()
    {
        Console.WriteLine("Задание 9. Восьмеричное число в десятичной системе");
        while (true)
        {
            Console.Write("Введите восьмеричное число (цифры 0–7): ");
            var text = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(text))
            {
                Console.WriteLine("Строка не должна быть пустой.");
                continue;
            }

            try
            {
                ulong value = 0;
                foreach (var ch in text)
                {
                    if (ch is < '0' or > '7')
                        throw new FormatException("В восьмеричной записи разрешены только цифры 0–7.");
                    value = checked(value * 8 + (uint)(ch - '0'));
                }
                Console.WriteLine($"Десятичное представление: {value}.");
                return;
            }
            catch (OverflowException)
            {
                Console.WriteLine("Число не помещается в диапазон UInt64. Повторите ввод.");
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    // 10. ASCII-окружность с геометрическим диаметром n.
    private static void Task10()
    {
        Console.WriteLine("Задание 10. Окружность в текстовом режиме");
        var diameter = Input.Int("Диаметр (целое число 1–100): ", 1, 100);
        if (diameter == 1)
        {
            Console.WriteLine("*");
            return;
        }
        var radius = diameter / 2.0;
        for (var row = 0; row <= diameter; row++)
        {
            var y = row - radius;
            var line = new StringBuilder(diameter + 1);
            for (var column = 0; column <= diameter; column++)
            {
                var x = column - radius;
                var distance = Math.Sqrt(x * x + y * y);
                line.Append(Math.Abs(distance - radius) <= 0.55 ? '*' : ' ');
            }
            Console.WriteLine(line.ToString().TrimEnd());
        }
    }

    // 11. Монте-Карло для площади между квадратичной параболой и Ox.
    private static void Task11()
    {
        Console.WriteLine("Задание 11. Площадь, ограниченная параболой и осью Ox (Монте-Карло)");
        var a = Input.Double("Коэффициент a при x²: ");
        var b = Input.Double("Коэффициент b при x: ");
        var c = Input.Double("Свободный коэффициент c: ");
        var iterations = Input.Iterations();
        if (a == 0)
        {
            Console.WriteLine("Для параболы коэффициент a должен быть отличен от нуля.");
            return;
        }

        var discriminant = b * b - 4 * a * c;
        if (!double.IsFinite(discriminant) || discriminant < 0)
        {
            Console.WriteLine("У параболы нет двух действительных пересечений с Ox, замкнутая фигура не образуется.");
            return;
        }
        var root = Math.Sqrt(discriminant);
        var x1 = (-b - root) / (2 * a);
        var x2 = (-b + root) / (2 * a);
        if (x1 > x2) (x1, x2) = (x2, x1);
        if (x1 == x2)
        {
            Console.WriteLine("Площадь равна 0: парабола касается оси Ox.");
            return;
        }

        double Function(double x) => a * x * x + b * x + c;
        var maxAbs = Math.Max(Math.Abs(Function(x1)), Math.Abs(Function(x2)));
        var vertex = -b / (2 * a);
        if (vertex > x1 && vertex < x2)
            maxAbs = Math.Max(maxAbs, Math.Abs(Function(vertex)));
        var inside = 0;
        for (var i = 0; i < iterations; i++)
        {
            var x = x1 + Random.Shared.NextDouble() * (x2 - x1);
            var y = Random.Shared.NextDouble() * maxAbs;
            if (y <= Math.Abs(Function(x)))
                inside++;
        }
        var estimate = (x2 - x1) * maxAbs * inside / iterations;
        var exact = Math.Abs(a) * Math.Pow(x2 - x1, 3) / 6;
        Console.WriteLine($"Оценка площади: {F(estimate)}; точное значение для сравнения: {F(exact)} (N={iterations}).");
        Console.WriteLine($"Абсолютная ошибка: {F(Math.Abs(estimate - exact))}.");
    }

    // 12. Ряд Лейбница: минимальное число членов, при котором фактическая ошибка < ε.
    private static void Task12()
    {
        Console.WriteLine("Задание 12. Точность ряда Лейбница для π");
        var epsilon = Input.PositiveDouble("Заданная абсолютная точность ε: ");
        var bound = Math.Max(1, Math.Ceiling((4 / epsilon - 1) / 2));
        if (!double.IsFinite(bound) || bound > MaxGridPoints)
        {
            Console.WriteLine($"Для такой точности простой ряд требует более {MaxGridPoints:N0} членов по оценке остатка. Укажите более грубую точность.");
            return;
        }

        double sum = 0;
        double compensation = 0;
        var required = 0;
        for (var k = 0; k < (long)bound; k++)
        {
            var term = (k % 2 == 0 ? 4.0 : -4.0) / (2 * k + 1);
            var adjusted = term - compensation;
            var next = sum + adjusted;
            compensation = (next - sum) - adjusted;
            sum = next;
            required++;
            if (Math.Abs(Pi - sum) < epsilon)
                break;
        }
        Console.WriteLine($"Требуется членов: {required}.");
        Console.WriteLine($"Частичная сумма: {F(sum)}; фактическая ошибка относительно π: {F(Math.Abs(Pi - sum))}.");
    }

    // 13. Текстовый график y = A exp(-x/τ), диапазон [0, 5τ].
    private static void Task13()
    {
        Console.WriteLine("Задание 13. График y = A exp(-x/τ), диапазон x = [0, 5τ]");
        var amplitude = Input.Double("A: ");
        var tau = Input.PositiveDouble("τ: ");
        if (tau > 1e100)
        {
            Console.WriteLine("τ слишком велико для отображения в выбранном масштабе.");
            return;
        }
        DrawPlot(x => amplitude * Math.Exp(-x / tau), 0, 5 * tau, "y = A exp(-x/τ)");
    }

    // 14. Монте-Карло для первой положительной части синусоиды от t=0 до следующего нуля.
    private static void Task14()
    {
        Console.WriteLine("Задание 14. Площадь первой положительной полуволны y = A sin(ωt + α)");
        var amplitude = Input.Double("A (0..π): ");
        var omega = Input.Double("ω (0..π]: ");
        var alpha = Input.Double("α (0..π): ");
        var iterations = Input.Iterations();
        if (amplitude < 0 || amplitude > Pi || omega <= 0 || omega > Pi || alpha < 0 || alpha > Pi)
        {
            Console.WriteLine("Требуются 0 ≤ A ≤ π, 0 < ω ≤ π и 0 ≤ α ≤ π.");
            return;
        }
        var end = (Pi - alpha) / omega;
        if (end == 0 || amplitude == 0)
        {
            Console.WriteLine("Площадь равна 0.");
            return;
        }
        var inside = 0;
        for (var i = 0; i < iterations; i++)
        {
            var t = Random.Shared.NextDouble() * end;
            var y = Random.Shared.NextDouble() * amplitude;
            if (y <= amplitude * Math.Sin(omega * t + alpha))
                inside++;
        }
        var estimate = end * amplitude * inside / iterations;
        var exact = amplitude * (1 + Math.Cos(alpha)) / omega;
        Console.WriteLine($"Оценка методом Монте-Карло: {F(estimate)}; точное значение для сравнения: {F(exact)} (N={iterations}).");
        Console.WriteLine($"Абсолютная ошибка: {F(Math.Abs(estimate - exact))}.");
    }

    // 15. Монте-Карло для круга; точность трактуется как абсолютный 95%-й запас ошибки.
    private static void Task15()
    {
        Console.WriteLine("Задание 15. Площадь круга методом Монте-Карло с заданной точностью");
        var radius = Input.PositiveDouble("Радиус: ");
        var epsilon = Input.PositiveDouble("Допустимая абсолютная ошибка площади ε: ");
        if (radius > 1e100)
        {
            Console.WriteLine("Слишком большой радиус для вычислений double.");
            return;
        }
        var estimateCount = Math.Ceiling(Math.Pow(1.96 * 2 * radius * radius / epsilon, 2));
        if (!double.IsFinite(estimateCount) || estimateCount > MaxGridPoints)
        {
            Console.WriteLine($"Для указанной точности нужно более {MaxGridPoints:N0} итераций. Увеличьте ε или уменьшите радиус.");
            return;
        }
        var iterations = Math.Max(1_000, (int)estimateCount);
        var inside = 0;
        var r2 = radius * radius;
        for (var i = 0; i < iterations; i++)
        {
            var x = (2 * Random.Shared.NextDouble() - 1) * radius;
            var y = (2 * Random.Shared.NextDouble() - 1) * radius;
            if (x * x + y * y <= r2)
                inside++;
        }
        var area = 4 * r2 * inside / iterations;
        var p = (double)inside / iterations;
        var margin95 = 1.96 * 4 * r2 * Math.Sqrt(p * (1 - p) / iterations);
        var exact = Pi * r2;
        Console.WriteLine($"Оценка площади: {F(area)}; точное значение для сравнения: {F(exact)}.");
        Console.WriteLine($"95%-й доверительный запас по выборке: ±{F(margin95)}; итераций: {iterations:N0}.");
    }

    // 16. Площадь под y = 3x² - 2x + 5.
    private static void Task16()
    {
        Console.WriteLine("Задание 16. Площадь под y = 3x² - 2x + 5 (составной метод трапеций)");
        var (a, b, h) = Input.IntervalAndStep();
        var area = IntegrateAbsolute(x => 3 * x * x - 2 * x + 5, a, b, h);
        Console.WriteLine($"Приближённая геометрическая площадь: {F(area)}.");
    }

    // 17. Ромб, горизонтальная диагональ d, вертикальная диагональ 2d.
    private static void Task17()
    {
        Console.WriteLine("Задание 17. Ромб: меньшая диагональ горизонтальна, большая вдвое длиннее");
        var d = Input.Int("Меньшая диагональ d (1–80): ", 1, 80);
        var center = d / 2.0;
        for (var row = 0; row <= 2 * d; row++)
        {
            var verticalOffset = Math.Abs(row - d);
            var halfWidth = center * (1 - verticalOffset / (double)d);
            var left = (int)Math.Round(center - halfWidth, MidpointRounding.AwayFromZero);
            var right = (int)Math.Round(center + halfWidth, MidpointRounding.AwayFromZero);
            var line = new StringBuilder(d + 1);
            for (var column = 0; column <= d; column++)
                line.Append(column == left || column == right ? '*' : ' ');
            Console.WriteLine(line.ToString().TrimEnd());
        }
    }

    // 18. График параболы; интервал [-5, 5] выбран, так как он не задан в условии.
    private static void Task18()
    {
        Console.WriteLine("Задание 18. График y = Ax² + C, диапазон x = [-5, 5]");
        var a = Input.Double("A: ");
        var c = Input.Double("C: ");
        DrawPlot(x => a * x * x + c, -5, 5, "y = Ax² + C");
    }

    // 19. Числа из n цифр (без ведущего нуля) с заданной суммой цифр.
    private static void Task19()
    {
        Console.WriteLine("Задание 19. Все n-значные числа с заданной суммой цифр");
        var digits = Input.Int("Количество цифр (1–18): ", 1, 18);
        var sum = Input.Int($"Сумма цифр (0–{9 * digits}): ", 0, 9 * digits);
        var memo = new Dictionary<(int Left, int Remaining, bool First), BigInteger>();

        BigInteger Count(int left, int remaining, bool first)
        {
            if (remaining < 0 || remaining > 9 * left)
                return BigInteger.Zero;
            if (left == 0)
                return remaining == 0 ? BigInteger.One : BigInteger.Zero;
            var key = (left, remaining, first);
            if (memo.TryGetValue(key, out var cached))
                return cached;
            var total = BigInteger.Zero;
            var start = first ? 1 : 0;
            for (var digit = start; digit <= 9 && digit <= remaining; digit++)
                total += Count(left - 1, remaining - digit, false);
            memo[key] = total;
            return total;
        }

        var count = Count(digits, sum, true);
        Console.WriteLine($"Количество подходящих чисел: {count}.");
        if (count > MaxEnumeration)
        {
            Console.WriteLine($"Полный список не создаётся: число результатов превышает защитный лимит {MaxEnumeration:N0}.");
            return;
        }

        var resultPath = ResultPath("task19_numbers.txt");
        using (var writer = new StreamWriter(resultPath, false, new UTF8Encoding(false)))
        {
            var buffer = new StringBuilder(digits);
            void Generate(int position, int remaining)
            {
                if (position == digits)
                {
                    if (remaining == 0)
                        writer.WriteLine(buffer.ToString());
                    return;
                }
                var start = position == 0 ? 1 : 0;
                var positionsAfter = digits - position - 1;
                for (var digit = start; digit <= 9 && digit <= remaining; digit++)
                {
                    if (remaining - digit > positionsAfter * 9)
                        continue;
                    buffer.Append((char)('0' + digit));
                    Generate(position + 1, remaining - digit);
                    buffer.Length--;
                }
            }
            Generate(0, sum);
        }
        Console.WriteLine($"Полный список сохранён в {resultPath}.");
    }

    // 20. Кубическое уравнение: сканируем отрезок, затем используем safeguarded Newton.
    private static void Task20()
    {
        Console.WriteLine("Задание 20. Корень ax³ + bx² + cx + d = 0 методом Ньютона");
        var a = Input.Double("a: ");
        var b = Input.Double("b: ");
        var c = Input.Double("c: ");
        var d = Input.Double("d: ");
        var epsilon = Input.PositiveDouble("Точность ε: ");
        if (a == 0)
        {
            Console.WriteLine("Коэффициент a должен быть отличен от нуля.");
            return;
        }

        var scale = Math.Max(Math.Max(Math.Abs(a), Math.Abs(b)), Math.Max(Math.Abs(c), Math.Abs(d)));
        var aa = a / scale;
        var bb = b / scale;
        var cc = c / scale;
        var dd = d / scale;
        double Function(double x) => ((aa * x + bb) * x + cc) * x + dd;
        double Derivative(double x) => (3 * aa * x + 2 * bb) * x + cc;
        var bound = 1 + Math.Max(Math.Abs(bb / aa), Math.Max(Math.Abs(cc / aa), Math.Abs(dd / aa)));
        if (!double.IsFinite(bound) || bound > 1e100)
        {
            Console.WriteLine("Корни выходят за безопасный диапазон вычислений double.");
            return;
        }

        const int scanSegments = 20_000;
        var left = -bound;
        var fLeft = Function(left);
        var found = false;
        for (var i = 1; i <= scanSegments; i++)
        {
            var right = -bound + (2 * bound * i / scanSegments);
            var fRight = Function(right);
            if (fLeft == 0)
            {
                Console.WriteLine($"Найден корень x = {F(left)} (точный узел сканирования).");
                return;
            }
            if (fRight == 0)
            {
                Console.WriteLine($"Найден корень x = {F(right)} (точный узел сканирования).");
                return;
            }
            if (Math.Sign(fLeft) != Math.Sign(fRight))
            {
                found = true;
                break;
            }
            left = right;
            fLeft = fRight;
        }
        if (!found)
        {
            Console.WriteLine("Не удалось выделить интервал с корнем; проверьте коэффициенты и масштаб.");
            return;
        }

        var rightBound = left + 2 * bound / scanSegments;
        var x = (left + rightBound) / 2;
        var iterations = 0;
        for (; iterations < 10_000; iterations++)
        {
            var fx = Function(x);
            if (Math.Abs(fx) <= epsilon || rightBound - left <= epsilon)
                break;
            var derivative = Derivative(x);
            var candidate = Math.Abs(derivative) > 1e-15 ? x - fx / derivative : double.NaN;
            if (!double.IsFinite(candidate) || candidate <= left || candidate >= rightBound)
                candidate = (left + rightBound) / 2;
            var fCandidate = Function(candidate);
            if (Math.Sign(fLeft) != Math.Sign(fCandidate))
            {
                rightBound = candidate;
            }
            else
            {
                left = candidate;
                fLeft = fCandidate;
            }
            x = candidate;
        }
        var residual = Math.Abs(Function(x));
        Console.WriteLine($"Корень x ≈ {F(x)}; |f(x)| = {F(residual)}; итераций: {iterations}.");
        if (residual > epsilon * 10 && rightBound - left > epsilon * 10)
            Console.WriteLine("Предупреждение: достигнут предел итераций до заданной точности.");
    }

    // 21. Чётность числа единичных битов.
    private static void Task21()
    {
        Console.WriteLine("Задание 21. Чётно ли количество единиц в двоичной записи ulong");
        var value = Input.ULong("Введите число: ");
        var ones = BitOperations.PopCount(value);
        Console.WriteLine($"Количество единиц: {ones}; ответ: {(ones % 2 == 0 ? "да, чётное" : "нет, нечётное")}.");
    }

    // 22. Все положительные делители числа.
    private static void Task22()
    {
        Console.WriteLine("Задание 22. Все делители положительного числа");
        var value = Input.ULong("Введите положительное число: ");
        if (value == 0)
        {
            Console.WriteLine("Ноль не имеет конечного списка положительных делителей.");
            return;
        }
        var low = new List<ulong>();
        var high = new List<ulong>();
        for (ulong divisor = 1; divisor <= value / divisor; divisor++)
        {
            if (value % divisor != 0)
                continue;
            low.Add(divisor);
            var pair = value / divisor;
            if (pair != divisor)
                high.Add(pair);
        }
        high.Reverse();
        Console.WriteLine($"Делители числа {value}: {string.Join(", ", low.Concat(high))}.");
    }

    // 23. Перечисление ulong с заданным количеством установленных битов.
    private static void Task23()
    {
        Console.WriteLine("Задание 23. Все ulong с заданным числом единичных битов");
        var ones = Input.Int("Количество единичных битов (0–64): ", 0, 64);
        var count = Binomial(64, ones);
        Console.WriteLine($"Всего возможных значений: {count}.");
        if (count > MaxEnumeration)
        {
            Console.WriteLine($"Список не создаётся: он превышает защитный лимит {MaxEnumeration:N0} значений.");
            return;
        }

        var resultPath = ResultPath($"task23_ulong_{ones}_bits.txt");
        using (var writer = new StreamWriter(resultPath, false, new UTF8Encoding(false)))
        {
            void Generate(int startBit, int remaining, ulong value)
            {
                if (remaining == 0)
                {
                    writer.WriteLine(value);
                    return;
                }
                for (var bit = startBit; bit <= 64 - remaining; bit++)
                    Generate(bit + 1, remaining - 1, value | (1UL << bit));
            }
            Generate(0, ones, 0);
        }
        Console.WriteLine($"Полный список сохранён в {resultPath}.");
    }

    // 24. Степень через быстрое возведение; умножение заменено двоичным сложением.
    private static void Task24()
    {
        Console.WriteLine("Задание 24. Вычисление X^Y без оператора умножения");
        var x = Input.ULong("Положительное X: ");
        var y = Input.Int("Положительное Y (1–1 000 000): ", 1, 1_000_000);
        if (x == 0)
        {
            Console.WriteLine("X должно быть положительным.");
            return;
        }
        if (x > 1 && y * Math.Log10(x) > 1_000_000)
        {
            Console.WriteLine("Результат слишком велик для безопасного текстового вывода (более миллиона цифр).");
            return;
        }

        var result = BigInteger.One;
        var factor = new BigInteger(x);
        var exponent = y;
        while (exponent > 0)
        {
            if ((exponent & 1) != 0)
                result = MultiplyByAddition(result, factor);
            exponent >>= 1;
            if (exponent > 0)
                factor = MultiplyByAddition(factor, factor);
        }
        Console.WriteLine($"{x}^{y} = {result}.");
    }

    private static BigInteger MultiplyByAddition(BigInteger left, BigInteger right)
    {
        var result = BigInteger.Zero;
        while (right > 0)
        {
            if (!right.IsEven)
                result += left;
            right >>= 1;
            if (right > 0)
                left += left;
        }
        return result;
    }

    private static BigInteger Binomial(int n, int k)
    {
        k = Math.Min(k, n - k);
        var result = BigInteger.One;
        for (var i = 1; i <= k; i++)
            result = result * (n - k + i) / i;
        return result;
    }

    private static string ResultPath(string fileName)
    {
        var directory = Path.Combine(Directory.GetCurrentDirectory(), "Results");
        Directory.CreateDirectory(directory);
        return Path.Combine(directory, fileName);
    }

    private static int SegmentCount(double a, double b, double step)
    {
        var count = Math.Ceiling((b - a) / step);
        if (!double.IsFinite(count) || count > MaxGridPoints)
            throw new InvalidOperationException($"Шаг слишком мал: количество расчётных отрезков превышает {MaxGridPoints:N0}.");
        return Math.Max(1, (int)count);
    }

    private static double IntegrateAbsolute(Func<double, double> function, double a, double b, double step)
    {
        var segments = SegmentCount(a, b, step);
        var sum = 0.0;
        var previousX = a;
        var previous = Math.Abs(function(a));
        for (var i = 1; i <= segments; i++)
        {
            var x = Math.Min(a + i * step, b);
            var current = Math.Abs(function(x));
            sum += (previous + current) * (x - previousX) / 2;
            previousX = x;
            previous = current;
        }
        return sum;
    }

    private static void DrawPlot(Func<double, double> function, double xMin, double xMax, string title)
    {
        const int width = 65;
        const int height = 21;
        var values = new double[width];
        var minY = 0.0;
        var maxY = 0.0;
        for (var column = 0; column < width; column++)
        {
            var x = xMin + (xMax - xMin) * column / (width - 1);
            values[column] = function(x);
            if (double.IsFinite(values[column]))
            {
                minY = Math.Min(minY, values[column]);
                maxY = Math.Max(maxY, values[column]);
            }
        }
        if (Math.Abs(maxY - minY) < 1e-12)
        {
            minY -= 1;
            maxY += 1;
        }

        var grid = new char[height, width];
        for (var row = 0; row < height; row++)
            for (var column = 0; column < width; column++)
                grid[row, column] = ' ';

        var zeroRow = (int)Math.Round((maxY / (maxY - minY)) * (height - 1));
        if (zeroRow >= 0 && zeroRow < height)
            for (var column = 0; column < width; column++)
                grid[zeroRow, column] = '─';

        if (xMin <= 0 && xMax >= 0)
        {
            var zeroColumn = (int)Math.Round((0 - xMin) / (xMax - xMin) * (width - 1));
            if (zeroColumn >= 0 && zeroColumn < width)
                for (var row = 0; row < height; row++)
                    grid[row, zeroColumn] = '│';
            if (zeroRow >= 0 && zeroRow < height && zeroColumn >= 0 && zeroColumn < width)
                grid[zeroRow, zeroColumn] = '┼';
        }

        for (var column = 0; column < width; column++)
        {
            if (!double.IsFinite(values[column]))
                continue;
            var row = (int)Math.Round((maxY - values[column]) / (maxY - minY) * (height - 1));
            row = Math.Clamp(row, 0, height - 1);
            grid[row, column] = '*';
        }

        Console.WriteLine(title);
        for (var row = 0; row < height; row++)
        {
            var line = new char[width];
            for (var column = 0; column < width; column++)
                line[column] = grid[row, column];
            Console.WriteLine(new string(line).TrimEnd());
        }
        Console.WriteLine($"x: {F(xMin)} .. {F(xMax)}; y: {F(minY)} .. {F(maxY)}");
    }

    private static string ConvertToBinary(ulong value)
    {
        if (value == 0)
            return "0";
        var chars = new char[64];
        var position = chars.Length;
        while (value != 0)
        {
            chars[--position] = (value & 1) == 1 ? '1' : '0';
            value >>= 1;
        }
        return new string(chars, position, chars.Length - position);
    }

    private static string F(double value) => value.ToString("0.##########", Ru);
}
