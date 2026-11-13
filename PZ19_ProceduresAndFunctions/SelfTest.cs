using System.Numerics;

namespace PZ19;

internal static class SelfTest
{
    private static int _passed;

    public static void Run()
    {
        _passed = 0;
        TestQuadratic();
        TestHeron();
        TestProducts();
        TestSums();
        TestSeries();
        TestGcd();
        TestBinomial();
        TestCurrency();
        TestBases();
        TestFibonacci();
        TestGeometry();
        TestComplex();
        TestRegularPolygon();
        TestPolyline();
        TestRectangles();
        TestTriangles();
        TestRegularPolygonGeometry();
        TestSorting();
        Console.WriteLine($"ПЗ19: самопроверка пройдена, проверок — {_passed}.");
    }

    private static void TestQuadratic()
    {
        var (roots, _) = Group1.SolveQuadratic(1, -3, 2);
        Check(roots.Length == 2 && Math.Abs(roots[0] - 1) < 1e-12 && Math.Abs(roots[1] - 2) < 1e-12, "корни x^2-3x+2");
        var (none, _) = Group1.SolveQuadratic(1, 0, 1);
        Check(none.Length == 0, "нет действительных корней");
        var (one, _) = Group1.SolveQuadratic(1, -2, 1);
        Check(one.Length == 1 && Math.Abs(one[0] - 1) < 1e-12, "один корень");
        var (linear, _) = Group1.SolveQuadratic(0, 2, -4);
        Check(linear.Length == 1 && Math.Abs(linear[0] - 2) < 1e-12, "линейное уравнение");
    }

    private static void TestHeron()
    {
        Check(Math.Abs(Group1.HeronArea(3, 4, 5) - 6) < 1e-12, "площадь 3-4-5");
        var threw = false;
        try { Group1.HeronArea(1, 2, 10); }
        catch (ArgumentException) { threw = true; }
        Check(threw, "неравенство треугольника");
    }

    private static void TestProducts()
    {
        Check(Group1.ProductOddFor(1, 5) == 15, "нечётные for");
        Check(Group1.ProductOddWhile(1, 5) == 15, "нечётные while");
        Check(Group1.ProductOddDoWhile(1, 5) == 15, "нечётные do-while");
        Check(Group1.ProductMultiplesOf4(1, 10) == 32, "произведение чисел, делящихся на 4");
    }

    private static void TestSums()
    {
        Check(Group1.SumPowersOfTwoLoop(5) == 63, "сумма степеней двойки циклом");
        Check(Group1.SumPowersOfTwoFormula(5) == 63, "сумма степеней двойки формулой");
        Check(Math.Abs(Group1.SumPowersOfHalfLoop(3) - 1.875) < 1e-12, "сумма половин циклом");
        Check(Math.Abs(Group1.SumPowersOfHalfFormula(3) - 1.875) < 1e-12, "сумма половин формулой");
    }

    private static void TestSeries()
    {
        Check(Math.Abs(Group1.CosSeries(0.5) - Math.Cos(0.5)) < 1e-14, "ряд косинуса");
        Check(Math.Abs(Group1.SinSeries(0.5) - Math.Sin(0.5)) < 1e-14, "ряд синуса");
        Check(Math.Abs(Group1.TanSeries(0.5) - Math.Tan(0.5)) < 1e-12, "тангенс через ряды");
        Check(Math.Abs(Group1.CosSeries(100) - Math.Cos(100)) < 1e-12, "приведение угла косинуса");
    }

    private static void TestGcd()
    {
        Check(Group1.Gcd(48, 18) == 6, "НОД");
        var foundAny = false;
        for (var a = 1; a <= 100; a++)
        {
            var y1 = (long)a * a * ((long)a * a + 12) - 5;
            var y2 = (long)a * ((long)a * a + 1);
            var gcd = Group1.Gcd(y1, y2);
            if (gcd > 1)
            {
                foundAny = true;
                break;
            }
        }
        Check(foundAny, "существует a с НОД > 1");
    }

    private static void TestBinomial()
    {
        Check(Group1.BinomialExpansion(2) == "a^2 + 2xa + x^2", "бином n=2");
        Check(Group1.BinomialExpansion(3) == "a^3 + 3xa^2 + 3x^2a + x^3", "бином n=3");
        Check(Group1.BinomialExpansion(0) == "1", "бином n=0");
    }

    private static void TestCurrency()
    {
        Check(Math.Abs(Group1.ConvertCurrency(100, "USD", "RUB") - 9000) < 1e-9, "USD→RUB");
        Check(Math.Abs(Group1.ConvertCurrency(9000, "RUB", "USD") - 100) < 1e-9, "RUB→USD");
    }

    private static void TestBases()
    {
        Check(Group1.ToBase(259, 2) == "100000011", "259 в двоичную");
        Check(Group1.ToBase(259, 8) == "403", "259 в восьмеричную");
        Check(Group1.ToBase(259, 16) == "103", "259 в шестнадцатеричную");
        Check(Group1.ToBase(0, 16) == "0", "ноль");
        Check(Group1.ParseBase("A2F", 16) == 2607, "A2F в десятичную");
        Check(Group1.ParseBase("0x10", 16) == 16, "префикс 0x");
    }

    private static void TestFibonacci()
    {
        Check(Group1.Fibonacci(1) == 1, "F1");
        Check(Group1.Fibonacci(2) == 1, "F2");
        Check(Group1.Fibonacci(10) == 55, "F10");
        Check(Group1.Fibonacci(20) == 6765, "F20");
        var phi = (1 + Math.Sqrt(5)) / 2;
        var found = Group1.FindGoldenThreshold(phi, 1e-10);
        Check(found.N is > 20 and < 100, "найден номер для золотого сечения");
        Check(found.Error < 1e-10, "точность золотого сечения");
        Check(Math.Abs((double)Group1.Fibonacci(found.N) / (double)Group1.Fibonacci(found.N - 1) - phi) < 1e-10,
            "отношение Фибоначчи");
    }

    private static void TestGeometry()
    {
        var square = new List<Point2>
        {
            new(0, 0, 1), new(1, 0, 2), new(1, 1, 3), new(0, 1, 4)
        };
        Check(Geometry.FindPolygonIntersections(square).Count == 0, "квадрат без пересечений");
        Check(Math.Abs(Geometry.PolygonArea(square) - 1) < 1e-12, "площадь квадрата");
        Check(Math.Abs(Geometry.PolygonPerimeter(square) - 4) < 1e-12, "периметр квадрата");

        var bowTie = new List<Point2>
        {
            new(0, 0, 1), new(1, 1, 2), new(1, 0, 3), new(0, 1, 4)
        };
        Check(Geometry.FindPolygonIntersections(bowTie).Count > 0, "бабочка имеет пересечение");

        var shuffled = new List<Point2>
        {
            new(0, 0, 1), new(1, 1, 2), new(0, 1, 3), new(1, 0, 4)
        };
        var ordered = Geometry.OrderAsSimplePolygon(shuffled);
        Check(Geometry.FindPolygonIntersections(ordered).Count == 0, "упорядоченный многоугольник простой");
        Check(Math.Abs(Geometry.PolygonArea(ordered) - 1) < 1e-12, "площадь упорядоченного квадрата");

        var pairs = Geometry.PairWithoutCrossings(shuffled);
        Check(pairs.Count == 2, "две пары точек");
        Check(!Geometry.SegmentsIntersect(pairs[0].A, pairs[0].B, pairs[1].A, pairs[1].B), "пары не пересекаются");

        var vertical = new List<Point3>
        {
            new(0, 0, 0, 1), new(0, 5, 0, 2), new(0, 1, 0, 3)
        };
        var sorted = Group2.SortByDistance(vertical);
        Check(sorted[0].Index == 1 && sorted[1].Index == 3 && sorted[2].Index == 2, "сортировка 3D по расстоянию");
    }

    private static void TestComplex()
    {
        var a = new ComplexNumber(1, 1);
        var b = new ComplexNumber(1, -1);
        var product = a.Multiply(b);
        Check(Math.Abs(product.Re - 2) < 1e-12 && Math.Abs(product.Im) < 1e-12, "произведение комплексных");
        Check(Group2.Quadrant(a) == 1, "первый квадрант");
        Check(Group2.Quadrant(new ComplexNumber(-1, -1)) == 3, "третий квадрант");
        Check(Group2.Quadrant(new ComplexNumber(0, 1)) == 0, "точка на оси");
    }

    private static void TestRegularPolygon()
    {
        var square = new List<Point2>
        {
            new(0, 0, 1), new(1, 0, 2), new(1, 1, 3), new(0, 1, 4)
        };
        Check(Geometry.IsRegularPolygon(square, out var count) && count == 4, "правильный квадрат");
        var notRegular = new List<Point2>
        {
            new(0, 0, 1), new(2, 0, 2), new(2, 1, 3), new(0, 1, 4)
        };
        Check(!Geometry.IsRegularPolygon(notRegular, out _), "неправильный прямоугольник");
    }

    private static void TestPolyline()
    {
        var points = new List<(Point2 Point, string Color)>
        {
            (new Point2(0, 0, 1), "красный"),
            (new Point2(3, 0, 2), "красный"),
            (new Point2(3, 4, 3), "красный"),
            (new Point2(10, 10, 4), "синий")
        };
        var lengths = Group2.PolylineLengths(points);
        Check(lengths.Count == 2, "две ломаные по цветам");
        Check(lengths[0].Color == "красный" && Math.Abs(lengths[0].Length - 7) < 1e-12, "длина красной ломаной");
        Check(lengths[1].Color == "синий" && Math.Abs(lengths[1].Length) < 1e-12, "длина синей ломаной");

        var ordered = Group2.OrderAsPolyline(new[]
        {
            new Point2(3, 0, 1), new Point2(0, 0, 2), new Point2(2, 5, 3), new Point2(1, 1, 4)
        });
        Check(ordered.Select(p => p.Index).SequenceEqual(new[] { 2, 4, 3, 1 }), "порядок x-монотонной ломаной");
        var shortest = Group2.FindShortestSegment(ordered);
        Check(Math.Abs(shortest.Length - Math.Sqrt(2)) < 1e-12, "минимальный сегмент");
    }

    private static void TestRectangles()
    {
        var rects = new List<Rect>
        {
            new(0, 0, 2, 2),
            new(1, 1, 3, 1)
        };
        var bounding = Geometry.BoundingRectangle(rects);
        Check(Math.Abs(bounding.X) < 1e-12 && Math.Abs(bounding.Y) < 1e-12, "левый верхний угол охвата");
        Check(Math.Abs(bounding.Width - 4) < 1e-12 && Math.Abs(bounding.Height - 2) < 1e-12, "размер охвата");
        Check(rects[0].Contains(1, 1) && rects[1].Contains(2, 1), "точка внутри прямоугольников");
        Check(!rects[0].Contains(3, 3), "точка снаружи");
    }

    private static void TestTriangles()
    {
        var triangles = new List<(int Index, double A, double B, double C, double Area)>
        {
            (1, 3, 4, 5, 6),
            (2, 6, 8, 10, 24),
            (3, 2, 3, 4, 2.9047375096555625)
        };
        var groups = Group2.GroupSimilarTriangles(triangles);
        var similar = groups.Where(g => g.Count > 1).ToList();
        Check(similar.Count == 1, "одна группа подобных");
        Check(similar[0].SequenceEqual(new[] { 1, 2 }), "подобны 1 и 2");
    }

    private static void TestRegularPolygonGeometry()
    {
        var square = Group2.RegularPolygonGeometry(4, 100);
        Check(Math.Abs(square.Side - 10) < 1e-12, "сторона квадрата");
        Check(Math.Abs(square.Perimeter - 40) < 1e-12, "периметр квадрата");
        var triangle = Group2.RegularPolygonGeometry(3, 100);
        Check(Math.Abs(triangle.Perimeter - 3 * Math.Sqrt(4 * 100 * Math.Sqrt(3) / 3)) < 1e-9,
            "периметр правильного треугольника");
    }

    private static void TestSorting()
    {
        var points = new List<Point3>
        {
            new(1, 2, 3, 1),
            new(1, 1, 9, 2),
            new(0, 5, 3, 3),
            new(1, 1, 0, 4)
        };
        var sorted = Group2.SortByPriority(points, new[] { 'Y', 'X', 'Z' });
        Check(sorted.Select(p => p.Index).SequenceEqual(new[] { 4, 2, 1, 3 }), "сортировка по Y X Z");
        Check(Group2.SortByPriority(points, new[] { 'Z', 'X', 'Y' }).Select(p => p.Index).SequenceEqual(new[] { 4, 3, 1, 2 }),
            "сортировка по Z X Y");
    }

    private static void Check(bool condition, string name)
    {
        if (!condition)
            throw new InvalidOperationException($"Самопроверка не пройдена: {name}.");
        _passed++;
        Console.WriteLine($"OK  {name}");
    }
}
