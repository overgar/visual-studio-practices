namespace PZ21;

internal static class SelfTest
{
    private static int _passed;

    public static void Run()
    {
        _passed = 0;
        var intAverage = AverageMethods.Average(new[] { 1, 2, 3, 4 });
        Check(Math.Abs(intAverage - 2.5) < 1e-12, "Average(int[]) = 2.5");
        var floatAverage = AverageMethods.Average(new[] { 1.5f, 2.5f, 3.0f });
        Check(Math.Abs(floatAverage - 7.0f / 3.0f) < 1e-6, "Average(float[])");
        Check(AverageMethods.Average(new[] { -5, 5 }) == 0, "Average с отрицательными числами");

        var emptyThrew = false;
        try { AverageMethods.Average(Array.Empty<int>()); }
        catch (ArgumentException) { emptyThrew = true; }
        Check(emptyThrew, "пустой массив вызывает исключение");

        var c0 = new Circle();
        Check(c0.X == 0 && c0.Y == 0 && c0.Radius == 0, "Circle()");
        var c1 = new Circle(5);
        Check(c1.X == 0 && c1.Y == 0 && c1.Radius == 5, "Circle(radius)");
        var c2 = new Circle(10, 20);
        Check(c2.X == 10 && c2.Y == 20 && c2.Radius == 0, "Circle(x, y)");
        var c3 = new Circle(10, 20, 7);
        Check(c3.X == 10 && c3.Y == 20 && c3.Radius == 7, "Circle(x, y, radius)");
        Check(Math.Abs(c3.Area - Math.PI * 49) < 1e-12, "площадь Circle");
        Check(Math.Abs(c3.Circumference - 14 * Math.PI) < 1e-12, "длина окружности Circle");

        var negativeThrew = false;
        try { _ = new Circle(-1); }
        catch (ArgumentOutOfRangeException) { negativeThrew = true; }
        Check(negativeThrew, "отрицательный радиус запрещён");

        Console.WriteLine($"ПЗ21: самопроверка пройдена, проверок — {_passed}.");
    }

    private static void Check(bool condition, string name)
    {
        if (!condition)
            throw new InvalidOperationException($"Самопроверка не пройдена: {name}.");
        _passed++;
        Console.WriteLine($"OK  {name}");
    }
}
