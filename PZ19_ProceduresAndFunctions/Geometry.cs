namespace PZ19;

internal static class Geometry
{
    public const double Eps = 1e-9;

    public static double Distance(Point2 a, Point2 b) =>
        Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y));

    public static double Distance(Point3 a, Point3 b) =>
        Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y) + (a.Z - b.Z) * (a.Z - b.Z));

    public static double Cross(Point2 a, Point2 b, Point2 c) =>
        (b.X - a.X) * (c.Y - a.Y) - (b.Y - a.Y) * (c.X - a.X);

    public static bool OnSegment(Point2 a, Point2 b, Point2 p)
    {
        if (Math.Abs(Cross(a, b, p)) > Eps)
            return false;
        return p.X >= Math.Min(a.X, b.X) - Eps && p.X <= Math.Max(a.X, b.X) + Eps &&
               p.Y >= Math.Min(a.Y, b.Y) - Eps && p.Y <= Math.Max(a.Y, b.Y) + Eps;
    }

    public static bool SegmentsIntersect(Point2 a, Point2 b, Point2 c, Point2 d)
    {
        var d1 = Cross(c, d, a);
        var d2 = Cross(c, d, b);
        var d3 = Cross(a, b, c);
        var d4 = Cross(a, b, d);

        if (((d1 > Eps && d2 < -Eps) || (d1 < -Eps && d2 > Eps)) &&
            ((d3 > Eps && d4 < -Eps) || (d3 < -Eps && d4 > Eps)))
            return true;

        if (Math.Abs(d1) <= Eps && OnSegment(c, d, a)) return true;
        if (Math.Abs(d2) <= Eps && OnSegment(c, d, b)) return true;
        if (Math.Abs(d3) <= Eps && OnSegment(a, b, c)) return true;
        if (Math.Abs(d4) <= Eps && OnSegment(a, b, d)) return true;
        return false;
    }

    public static List<(int I, int J)> FindPolygonIntersections(IReadOnlyList<Point2> points)
    {
        var result = new List<(int, int)>();
        var n = points.Count;
        if (n < 4)
            return result;

        for (var i = 0; i < n; i++)
        {
            var i2 = (i + 1) % n;
            for (var j = i + 1; j < n; j++)
            {
                var j2 = (j + 1) % n;
                if (i == j || i2 == j || j2 == i)
                    continue;
                if (SegmentsIntersect(points[i], points[i2], points[j], points[j2]))
                    result.Add((i, j));
            }
        }

        return result;
    }

    public static double PolygonPerimeter(IReadOnlyList<Point2> points)
    {
        var sum = 0.0;
        for (var i = 0; i < points.Count; i++)
            sum += Distance(points[i], points[(i + 1) % points.Count]);
        return sum;
    }

    public static double PolygonArea(IReadOnlyList<Point2> points)
    {
        var sum = 0.0;
        for (var i = 0; i < points.Count; i++)
        {
            var j = (i + 1) % points.Count;
            sum += points[i].X * points[j].Y - points[j].X * points[i].Y;
        }
        return Math.Abs(sum) / 2.0;
    }

    public static bool HasDuplicatePoints(IReadOnlyList<Point2> points)
    {
        for (var i = 0; i < points.Count; i++)
            for (var j = i + 1; j < points.Count; j++)
                if (Distance(points[i], points[j]) <= Eps)
                    return true;
        return false;
    }

    public static List<Point2> OrderAsSimplePolygon(IReadOnlyList<Point2> source)
    {
        if (source.Count < 3)
            throw new ArgumentException("Для многоугольника нужно не менее трёх точек.");
        if (HasDuplicatePoints(source))
            throw new ArgumentException("Среди точек есть совпадающие.");

        var cx = source.Average(p => p.X);
        var cy = source.Average(p => p.Y);
        var ordered = source
            .OrderBy(p => Math.Atan2(p.Y - cy, p.X - cx))
            .ThenBy(p => Distance(new Point2(cx, cy), p))
            .ToList();

        Uncross(ordered);
        return ordered;
    }

    private static void Uncross(List<Point2> points)
    {
        var n = points.Count;
        for (var iteration = 0; iteration < 10_000; iteration++)
        {
            var changed = false;
            for (var i = 0; i < n; i++)
            {
                for (var j = i + 2; j < n; j++)
                {
                    if (i == 0 && j == n - 1)
                        continue;
                    var i2 = (i + 1) % n;
                    var j2 = (j + 1) % n;
                    if (!SegmentsIntersect(points[i], points[i2], points[j], points[j2]))
                        continue;

                    ReverseRange(points, i + 1, j);
                    changed = true;
                }
            }

            if (!changed)
                return;
        }

        throw new InvalidOperationException("Не удалось устранить самопересечения.");
    }

    private static void ReverseRange(List<Point2> points, int from, int to)
    {
        while (from < to)
        {
            (points[from], points[to]) = (points[to], points[from]);
            from++;
            to--;
        }
    }

    public static List<(Point2 A, Point2 B)> PairWithoutCrossings(IReadOnlyList<Point2> points)
    {
        if (points.Count % 2 != 0)
            throw new ArgumentException("Количество точек должно быть чётным.");
        if (HasDuplicatePoints(points))
            throw new ArgumentException("Среди точек есть совпадающие.");

        var sorted = points.OrderBy(p => p.X).ThenBy(p => p.Y).ToList();
        var pairs = new List<(Point2 A, Point2 B)>();
        for (var i = 0; i < sorted.Count; i += 2)
            pairs.Add((sorted[i], sorted[i + 1]));

        for (var i = 0; i < pairs.Count; i++)
            for (var j = i + 1; j < pairs.Count; j++)
                if (SegmentsIntersect(pairs[i].A, pairs[i].B, pairs[j].A, pairs[j].B))
                    throw new InvalidOperationException("Полученные отрезки пересекаются.");

        return pairs;
    }

    public static bool IsRegularPolygon(IReadOnlyList<Point2> points, out int vertexCount)
    {
        vertexCount = 0;
        if (points.Count < 3 || HasDuplicatePoints(points))
            return false;

        var n = points.Count;
        var cx = points.Average(p => p.X);
        var cy = points.Average(p => p.Y);
        var radii = points.Select(p => Distance(new Point2(cx, cy), p)).ToArray();
        var maxRadius = radii.Max();
        if (maxRadius <= Eps)
            return false;
        if (radii.Any(r => Math.Abs(r - maxRadius) > 1e-6 * Math.Max(1.0, maxRadius)))
            return false;

        var angles = points
            .Select(p =>
            {
                var a = Math.Atan2(p.Y - cy, p.X - cx);
                return a < 0 ? a + 2 * Math.PI : a;
            })
            .OrderBy(a => a)
            .ToArray();

        var expected = 2 * Math.PI / n;
        for (var i = 0; i < n; i++)
        {
            var next = i == n - 1 ? angles[0] + 2 * Math.PI : angles[i + 1];
            if (Math.Abs(next - angles[i] - expected) > 1e-6)
                return false;
        }

        vertexCount = n;
        return true;
    }

    public static double TriangleArea(double a, double b, double c)
    {
        var p = (a + b + c) / 2;
        var value = p * (p - a) * (p - b) * (p - c);
        if (value < -Eps)
            throw new ArgumentException("Из таких сторон треугольник построить нельзя.");
        return Math.Sqrt(Math.Max(0, value));
    }

    public static Rect BoundingRectangle(IReadOnlyList<Rect> rectangles)
    {
        var minX = rectangles.Min(r => r.X);
        var minY = rectangles.Min(r => r.Y);
        var maxX = rectangles.Max(r => r.Right);
        var maxY = rectangles.Max(r => r.Bottom);
        return new Rect(minX, minY, maxX - minX, maxY - minY);
    }
}
