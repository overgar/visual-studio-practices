namespace PZ19;

internal static class Group2
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
        Console.WriteLine("Точки в трёхмерном пространстве: сортировка по удалению от центра и расстояния");
        var n = Input.Int("Количество точек (2..200) = ", 2, 200);
        var points = new List<Point3>();
        for (var i = 0; i < n; i++)
            points.Add(Input.Point3($"Точка {i + 1} (x y z): ", i + 1));

        var sorted = SortByDistance(points);
        Console.WriteLine("Точки по возрастанию расстояния от начала координат:");
        foreach (var p in sorted)
            Console.WriteLine($"  №{p.Index,2}: ({p.X:0.###}; {p.Y:0.###}; {p.Z:0.###}), R = {Math.Sqrt(p.X * p.X + p.Y * p.Y + p.Z * p.Z):0.###}");

        var max = double.MinValue;
        var min = double.MaxValue;
        (Point3 A, Point3 B) maxPair = default, minPair = default;
        for (var i = 0; i < points.Count; i++)
        {
            for (var j = i + 1; j < points.Count; j++)
            {
                var distance = Geometry.Distance(points[i], points[j]);
                if (distance > max)
                {
                    max = distance;
                    maxPair = (points[i], points[j]);
                }
                if (distance < min)
                {
                    min = distance;
                    minPair = (points[i], points[j]);
                }
            }
        }

        Console.WriteLine($"Минимальное расстояние: {min:0.###} между №{minPair.A.Index} и №{minPair.B.Index}.");
        Console.WriteLine($"Максимальное расстояние: {max:0.###} между №{maxPair.A.Index} и №{maxPair.B.Index}.");
    }

    internal static List<Point3> SortByDistance(IEnumerable<Point3> points) =>
        points.OrderBy(p => p.X * p.X + p.Y * p.Y + p.Z * p.Z)
              .ThenBy(p => p.Index)
              .ToList();

    private static void Task2()
    {
        Console.WriteLine("Многоугольник: проверка пересечения граней, периметр и площадь");
        var n = Input.Int("Количество вершин (3..200) = ", 3, 200);
        var points = new List<Point2>();
        for (var i = 0; i < n; i++)
            points.Add(Input.Point2($"Вершина {i + 1} (x y): ", i + 1));

        var intersections = Geometry.FindPolygonIntersections(points);
        if (intersections.Count > 0)
        {
            Console.WriteLine("Многоугольник имеет пересекающиеся грани:");
            foreach (var (i, j) in intersections)
                Console.WriteLine($"  Грань {i + 1}-{(i + 1) % n + 1} пересекает грань {j + 1}-{(j + 1) % n + 1}.");
            return;
        }

        var perimeter = Geometry.PolygonPerimeter(points);
        var area = Geometry.PolygonArea(points);
        Console.WriteLine("Пересекающихся граней нет.");
        Console.WriteLine($"Периметр P = {perimeter:0.############}");
        Console.WriteLine($"Площадь S = {area:0.############}");
        if (area < Geometry.Eps)
            Console.WriteLine("Замечание: площадь близка к нулю, многоугольник вырожден.");
    }

    private static void Task3()
    {
        Console.WriteLine("Упорядочивание точек в простой многоугольник");
        var n = Input.Int("Количество точек (3..200) = ", 3, 200);
        var points = new List<Point2>();
        for (var i = 0; i < n; i++)
            points.Add(Input.Point2($"Точка {i + 1} (x y): ", i + 1));

        var ordered = Geometry.OrderAsSimplePolygon(points);
        var intersections = Geometry.FindPolygonIntersections(ordered);
        Console.WriteLine("Порядок обхода (исходные номера): " + string.Join(" -> ", ordered.Select(p => p.Index)));
        Console.WriteLine(intersections.Count == 0
            ? "Стороны не пересекаются."
            : "Внимание: остались пересечения.");
        Console.WriteLine($"Периметр: {Geometry.PolygonPerimeter(ordered):0.###}, площадь: {Geometry.PolygonArea(ordered):0.###}.");
    }

    private static void Task4()
    {
        Console.WriteLine("Разбиение точек на пары непересекающимися отрезками");
        var n = Input.Int("Количество точек (чётное, 2..20) = ", 2, 20);
        if (n % 2 != 0)
        {
            Console.WriteLine("Количество точек должно быть чётным.");
            return;
        }

        var points = new List<Point2>();
        for (var i = 0; i < n; i++)
            points.Add(Input.Point2($"Точка {i + 1} (x y): ", i + 1));

        var pairs = Geometry.PairWithoutCrossings(points);
        Console.WriteLine("Пары (исходные номера):");
        foreach (var (a, b) in pairs)
            Console.WriteLine($"  №{a.Index} ({a.X:0.###}; {a.Y:0.###}) — №{b.Index} ({b.X:0.###}; {b.Y:0.###})");
        Console.WriteLine("Отрезки не пересекаются.");
    }

    private static void Task5()
    {
        Console.WriteLine("Комплексные числа: сортировка по модулю, поиск по квадранту, произведение");
        var n = Input.Int("Количество чисел (1..100) = ", 1, 100);
        var numbers = new List<ComplexNumber>();
        for (var i = 0; i < n; i++)
        {
            var (re, im) = Input.Pair($"Число {i + 1} (Re Im): ");
            numbers.Add(new ComplexNumber(re, im));
        }

        var sorted = numbers.OrderBy(z => z.Modulus).ThenBy(z => z.Re).ThenBy(z => z.Im).ToList();
        Console.WriteLine("По возрастанию модуля:");
        foreach (var z in sorted)
            Console.WriteLine($"  {z}   |z| = {z.Modulus:0.###}");

        var quadrant = Input.Int("Квадрант для поиска (1..4) = ", 1, 4);
        var found = numbers.Where(z => Quadrant(z) == quadrant).ToList();
        Console.WriteLine(found.Count == 0
            ? $"В квадранте {quadrant} чисел нет."
            : $"Квадрант {quadrant}: " + string.Join(", ", found));

        var product = new ComplexNumber(1, 0);
        foreach (var z in numbers)
            product = product.Multiply(z);
        Console.WriteLine($"Произведение всех чисел: {product}");
    }

    internal static int Quadrant(ComplexNumber z)
    {
        if (Math.Abs(z.Re) < Geometry.Eps || Math.Abs(z.Im) < Geometry.Eps)
            return 0;
        if (z.Re > 0 && z.Im > 0) return 1;
        if (z.Re < 0 && z.Im > 0) return 2;
        if (z.Re < 0 && z.Im < 0) return 3;
        return 4;
    }

    private static void Task6()
    {
        Console.WriteLine("Сортировка точек по удалению от центра и проверка правильного многоугольника");
        var n = Input.Int("Количество точек (3..100) = ", 3, 100);
        var points = new List<Point2>();
        for (var i = 0; i < n; i++)
            points.Add(Input.Point2($"Точка {i + 1} (x y): ", i + 1));

        var cx = points.Average(p => p.X);
        var cy = points.Average(p => p.Y);
        var center = new Point2(cx, cy);
        var sorted = points.OrderBy(p => Geometry.Distance(center, p)).ThenBy(p => p.Index).ToList();
        Console.WriteLine($"Центр множества: ({cx:0.###}; {cy:0.###})");
        foreach (var p in sorted)
            Console.WriteLine($"  №{p.Index}: ({p.X:0.###}; {p.Y:0.###}), R = {Geometry.Distance(center, p):0.###}");

        if (Geometry.IsRegularPolygon(points, out var count))
            Console.WriteLine($"Точки являются вершинами правильного {count}-угольника.");
        else
            Console.WriteLine("Точки не являются вершинами правильного многоугольника.");
    }

    private static void Task7()
    {
        Console.WriteLine("Прямоугольники: сортировка по площади и ориентация");
        var n = Input.Int("Количество прямоугольников (1..25) = ", 1, 25);
        var rectangles = new List<(int Index, double Width, double Height)>();
        for (var i = 0; i < n; i++)
        {
            var (width, height) = Input.Pair($"Прямоугольник {i + 1} (ширина высота): ");
            if (width <= 0 || height <= 0)
            {
                Console.WriteLine("Стороны должны быть положительными. Повторите ввод.");
                i--;
                continue;
            }
            rectangles.Add((i + 1, width, height));
        }

        Console.WriteLine("По возрастанию площади:");
        foreach (var r in rectangles.OrderBy(r => r.Width * r.Height).ThenBy(r => r.Index))
        {
            var orientation = r.Width >= r.Height ? "горизонтальная" : "вертикальная";
            Console.WriteLine($"  №{r.Index}: {r.Width:0.###}×{r.Height:0.###}, S = {r.Width * r.Height:0.###}, {orientation}");
        }
    }

    private static void Task8()
    {
        Console.WriteLine("Ломаные линии: подсчёт длин по цветам и сортировка по цвету");
        var n = Input.Int("Количество точек (2..200) = ", 2, 200);
        var points = new List<(Point2 Point, string Color)>();
        for (var i = 0; i < n; i++)
        {
            var p = Input.Point2($"Точка {i + 1} (x y): ", i + 1);
            var color = Input.Line($"Цвет точки {i + 1}: ");
            points.Add((p, color));
        }

        var lengths = PolylineLengths(points);
        foreach (var item in lengths)
            Console.WriteLine($"  Цвет «{item.Color}»: точек {item.Count}, длина {item.Length:0.###}");
    }

    internal static List<(string Color, int Count, double Length)> PolylineLengths(
        IEnumerable<(Point2 Point, string Color)> points)
    {
        return points
            .GroupBy(p => p.Color, StringComparer.OrdinalIgnoreCase)
            .OrderBy(g => g.Key, StringComparer.OrdinalIgnoreCase)
            .Select(g =>
            {
                var list = g.Select(x => x.Point).ToList();
                var length = 0.0;
                for (var i = 1; i < list.Count; i++)
                    length += Geometry.Distance(list[i - 1], list[i]);
                return (g.Key, list.Count, length);
            })
            .ToList();
    }

    private static void Task9()
    {
        Console.WriteLine("Ломаная без самопересечений и минимальный сегмент");
        var n = Input.Int("Количество точек (2..200) = ", 2, 200);
        var points = new List<Point2>();
        for (var i = 0; i < n; i++)
            points.Add(Input.Point2($"Точка {i + 1} (x y): ", i + 1));

        var ordered = OrderAsPolyline(points);
        Console.WriteLine("Порядок обхода (исходные номера): " + string.Join(" -> ", ordered.Select(p => p.Index)));
        var shortest = FindShortestSegment(ordered);
        Console.WriteLine($"Минимальный сегмент: №{shortest.A.Index} — №{shortest.B.Index}, длина {shortest.Length:0.###}.");
        Console.WriteLine("Ломаная является x-монотонной, поэтому не имеет самопересечений.");
    }

    internal static List<Point2> OrderAsPolyline(IEnumerable<Point2> points) =>
        points.OrderBy(p => p.X).ThenBy(p => p.Y).ThenBy(p => p.Index).ToList();

    internal static (Point2 A, Point2 B, double Length) FindShortestSegment(IReadOnlyList<Point2> ordered)
    {
        if (ordered.Count < 2)
            throw new ArgumentException("Нужно не менее двух точек.");
        var best = (A: ordered[0], B: ordered[1], Length: Geometry.Distance(ordered[0], ordered[1]));
        for (var i = 1; i < ordered.Count - 1; i++)
        {
            var length = Geometry.Distance(ordered[i], ordered[i + 1]);
            if (length < best.Length)
                best = (ordered[i], ordered[i + 1], length);
        }
        return best;
    }

    private static void Task10()
    {
        Console.WriteLine("Охватывающий прямоугольник и поиск прямоугольников, содержащих точку");
        var n = Input.Int("Количество прямоугольников (1..10) = ", 1, 10);
        var rectangles = new List<Rect>();
        for (var i = 0; i < n; i++)
        {
            Console.WriteLine($"Прямоугольник {i + 1}: x, y, ширина, высота.");
            while (true)
            {
                var values = Input.Numbers("x y w h: ");
                if (values.Length >= 4 && values[2] > 0 && values[3] > 0)
                {
                    rectangles.Add(new Rect(values[0], values[1], values[2], values[3]));
                    break;
                }
                Console.WriteLine("Нужны четыре числа, ширина и высота положительны.");
            }
        }

        var bounding = Geometry.BoundingRectangle(rectangles);
        Console.WriteLine($"Минимальный охватывающий прямоугольник: x = {bounding.X:0.###}, y = {bounding.Y:0.###}, w = {bounding.Width:0.###}, h = {bounding.Height:0.###}.");
        var (px, py) = Input.Pair("Точка для поиска (x y): ");
        var containing = rectangles
            .Select((r, i) => (Rect: r, Index: i + 1))
            .Where(item => item.Rect.Contains(px, py))
            .ToList();

        if (containing.Count == 0)
            Console.WriteLine("Ни один прямоугольник не содержит эту точку.");
        else
            foreach (var item in containing)
                Console.WriteLine($"Точку содержит прямоугольник №{item.Index}: x = {item.Rect.X:0.###}, y = {item.Rect.Y:0.###}, w = {item.Rect.Width:0.###}, h = {item.Rect.Height:0.###}.");
    }

    private static void Task11()
    {
        Console.WriteLine("Звёздный каталог");
        var n = Input.Int("Количество звёзд (1..100) = ", 1, 100);
        var stars = new List<Star>();
        for (var i = 0; i < n; i++)
            stars.Add(ReadStar(i + 1));

        Console.WriteLine("По возрастанию расстояния до Земли:");
        foreach (var star in stars.OrderBy(s => s.Distance).ThenBy(s => s.Name))
            Console.WriteLine($"  {star.Name}: m = {star.Magnitude:0.##}, d = {star.Distance:0.###} св. лет, RA = {star.RightAscension:0.###}, Dec = {star.Declination:0.###}");

        var min = Input.Double("Поиск: минимальная видимая величина = ");
        var max = Input.Double("Поиск: максимальная видимая величина = ");
        if (min > max)
            (min, max) = (max, min);
        var found = stars.Where(s => s.Magnitude >= min - Geometry.Eps && s.Magnitude <= max + Geometry.Eps).ToList();
        Console.WriteLine(found.Count == 0
            ? "Звёзд с такой видимой величиной нет."
            : "Найденные звёзды:");
        foreach (var star in found.OrderBy(s => s.Magnitude))
            Console.WriteLine($"  {star.Name}: m = {star.Magnitude:0.##}, d = {star.Distance:0.###}");
    }

    private static Star ReadStar(int index)
    {
        while (true)
        {
            var text = Input.Line($"Звезда {index} (название; величина; расстояние; RA; Dec): ");
            var parts = text.Split(';');
            if (parts.Length == 5 &&
                Input.TryDouble(parts[1], out var magnitude) &&
                Input.TryDouble(parts[2], out var distance) &&
                Input.TryDouble(parts[3], out var ra) &&
                Input.TryDouble(parts[4], out var dec))
            {
                var name = parts[0].Trim();
                if (name.Length == 0)
                {
                    Console.WriteLine("Название не должно быть пустым.");
                    continue;
                }
                if (distance < 0)
                {
                    Console.WriteLine("Расстояние не может быть отрицательным.");
                    continue;
                }
                return new Star(name, magnitude, distance, ra, dec);
            }
            Console.WriteLine("Формат: Название; величина; расстояние; RA; Dec (через точку с запятой).");
        }
    }

    private static void Task12()
    {
        Console.WriteLine("Треугольники: сортировка по площади и группы подобных");
        var n = Input.Int("Количество треугольников (1..100) = ", 1, 100);
        var triangles = new List<(int Index, double A, double B, double C, double Area)>();
        for (var i = 0; i < n; i++)
        {
            while (true)
            {
                var (x, y, z) = Input.Triple($"Треугольник {i + 1} (три стороны): ");
                var sides = new[] { x, y, z }.OrderBy(v => v).ToArray();
                if (sides[0] <= 0 || sides[0] + sides[1] <= sides[2] + Geometry.Eps)
                {
                    Console.WriteLine("Из таких сторон треугольник построить нельзя. Повторите ввод.");
                    continue;
                }
                triangles.Add((i + 1, sides[0], sides[1], sides[2], Geometry.TriangleArea(sides[0], sides[1], sides[2])));
                break;
            }
        }

        Console.WriteLine("По возрастанию площади:");
        foreach (var t in triangles.OrderBy(t => t.Area).ThenBy(t => t.Index))
            Console.WriteLine($"  №{t.Index}: стороны {t.A:0.###}, {t.B:0.###}, {t.C:0.###}; S = {t.Area:0.###}");

        var groups = GroupSimilarTriangles(triangles);
        var similarGroups = groups.Where(g => g.Count > 1).ToList();
        if (similarGroups.Count == 0)
        {
            Console.WriteLine("Подобных треугольников не найдено.");
            return;
        }

        Console.WriteLine("Группы подобных треугольников:");
        foreach (var group in similarGroups)
            Console.WriteLine("  " + string.Join(", ", group.Select(i => $"№{i}")));
    }

    internal static List<List<int>> GroupSimilarTriangles(
        IReadOnlyList<(int Index, double A, double B, double C, double Area)> triangles,
        double tolerance = 1e-6)
    {
        var parent = Enumerable.Range(0, triangles.Count).ToArray();
        int Find(int x) => parent[x] == x ? x : parent[x] = Find(parent[x]);
        void Union(int a, int b)
        {
            a = Find(a);
            b = Find(b);
            if (a != b)
                parent[b] = a;
        }

        for (var i = 0; i < triangles.Count; i++)
        {
            for (var j = i + 1; j < triangles.Count; j++)
            {
                if (AreSimilar(triangles[i], triangles[j], tolerance))
                    Union(i, j);
            }
        }

        return Enumerable.Range(0, triangles.Count)
            .GroupBy(Find)
            .Select(g => g.Select(i => triangles[i].Index).OrderBy(i => i).ToList())
            .ToList();
    }

    private static bool AreSimilar(
        (int Index, double A, double B, double C, double Area) x,
        (int Index, double A, double B, double C, double Area) y,
        double tolerance)
    {
        var r1 = (x.A / x.C, x.B / x.C);
        var r2 = (y.A / y.C, y.B / y.C);
        return Math.Abs(r1.Item1 - r2.Item1) <= tolerance && Math.Abs(r1.Item2 - r2.Item2) <= tolerance;
    }

    private static void Task13()
    {
        Console.WriteLine("Правильные многоугольники: сортировка по площади и периметру");
        var n = Input.Int("Количество многоугольников (1..25) = ", 1, 25);
        var polygons = new List<(int Index, int Vertices, double Area, double Side, double Perimeter)>();
        for (var i = 0; i < n; i++)
        {
            var vertices = Input.Int($"Многоугольник {i + 1}: число вершин (3..100) = ", 3, 100);
            var area = Input.PositiveDouble("Площадь = ");
            var geometry = RegularPolygonGeometry(vertices, area);
            polygons.Add((i + 1, vertices, area, geometry.Side, geometry.Perimeter));
        }

        Console.WriteLine("По возрастанию площади:");
        foreach (var p in polygons.OrderBy(p => p.Area).ThenBy(p => p.Index))
            Console.WriteLine($"  №{p.Index}: n = {p.Vertices}, S = {p.Area:0.###}, a = {p.Side:0.###}, P = {p.Perimeter:0.###}");

        Console.WriteLine("По возрастанию периметра:");
        foreach (var p in polygons.OrderBy(p => p.Perimeter).ThenBy(p => p.Index))
            Console.WriteLine($"  №{p.Index}: P = {p.Perimeter:0.###}, S = {p.Area:0.###}");

        Console.WriteLine($"Суммарная площадь: {polygons.Sum(p => p.Area):0.###}");
        Console.WriteLine($"Суммарный периметр: {polygons.Sum(p => p.Perimeter):0.###}");
    }

    internal static (double Side, double Perimeter) RegularPolygonGeometry(int vertices, double area)
    {
        if (vertices < 3)
            throw new ArgumentException("Число вершин должно быть не меньше трёх.");
        if (area <= 0)
            throw new ArgumentException("Площадь должна быть положительной.");
        var side = Math.Sqrt(4 * area * Math.Tan(Math.PI / vertices) / vertices);
        return (side, side * vertices);
    }

    private static void Task14()
    {
        Console.WriteLine("Сортировка 3D-точек по приоритету координат");
        var n = Input.Int("Количество точек (1..17) = ", 1, 17);
        var points = new List<Point3>();
        for (var i = 0; i < n; i++)
            points.Add(Input.Point3($"Точка {i + 1} (x y z): ", i + 1));

        var priorityText = Input.Line("Приоритет координат (например, Y X Z): ").ToUpperInvariant();
        var priority = priorityText
            .Split(new[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(s => s[0])
            .ToArray();

        if (priority.Length != 3 || priority.Distinct().Count() != 3 || priority.Any(c => c is not ('X' or 'Y' or 'Z')))
            throw new FormatException("Приоритет должен быть перестановкой X, Y, Z.");

        var sorted = SortByPriority(points, priority);
        Console.WriteLine($"Порядок приоритета: {string.Join(" → ", priority)}");
        foreach (var p in sorted)
            Console.WriteLine($"  №{p.Index,2}: ({p.X:0.###}; {p.Y:0.###}; {p.Z:0.###})");
    }

    internal static List<Point3> SortByPriority(IEnumerable<Point3> points, IReadOnlyList<char> priority)
    {
        if (priority.Count != 3 || priority.Distinct().Count() != 3)
            throw new ArgumentException("Нужна перестановка X, Y, Z.");

        double Coordinate(Point3 p, char axis) => axis switch
        {
            'X' => p.X,
            'Y' => p.Y,
            'Z' => p.Z,
            _ => throw new ArgumentException($"Неизвестная координата: {axis}.")
        };

        return points
            .OrderBy(p => Coordinate(p, priority[0]))
            .ThenBy(p => Coordinate(p, priority[1]))
            .ThenBy(p => Coordinate(p, priority[2]))
            .ThenBy(p => p.Index)
            .ToList();
    }
}
