namespace PZ21;

/// <summary>Окружность на плоскости с четырьмя перегруженными конструкторами.</summary>
internal sealed class Circle
{
    public int X { get; }
    public int Y { get; }
    public int Radius { get; }

    public double Area => Math.PI * Radius * Radius;
    public double Circumference => 2 * Math.PI * Radius;

    public Circle() : this(0, 0, 0)
    {
    }

    public Circle(int radius) : this(0, 0, radius)
    {
    }

    public Circle(int x, int y) : this(x, y, 0)
    {
    }

    public Circle(int x, int y, int radius)
    {
        if (radius < 0)
            throw new ArgumentOutOfRangeException(nameof(radius), "Радиус не может быть отрицательным.");
        X = x;
        Y = y;
        Radius = radius;
    }

    public override string ToString() =>
        $"Circle(X = {X}, Y = {Y}, Radius = {Radius}), S = {Area:0.###}, C = {Circumference:0.###}";
}
