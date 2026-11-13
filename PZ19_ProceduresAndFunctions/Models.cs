namespace PZ19;

internal readonly record struct Point2(double X, double Y, int Index = 0);

internal readonly record struct Point3(double X, double Y, double Z, int Index = 0);

internal readonly record struct ComplexNumber(double Re, double Im)
{
    public double Modulus => Math.Sqrt(Re * Re + Im * Im);

    public ComplexNumber Multiply(ComplexNumber other) =>
        new(Re * other.Re - Im * other.Im, Re * other.Im + Im * other.Re);

    public override string ToString()
    {
        var sign = Im < 0 ? "-" : "+";
        return $"{Re:0.###} {sign} {Math.Abs(Im):0.###}i";
    }
}

internal sealed record Star(string Name, double Magnitude, double Distance, double RightAscension, double Declination);

internal sealed record Rect(double X, double Y, double Width, double Height)
{
    public double Area => Width * Height;
    public double Right => X + Width;
    public double Bottom => Y + Height;

    public bool Contains(double x, double y) =>
        x >= X - Geometry.Eps && x <= Right + Geometry.Eps &&
        y >= Y - Geometry.Eps && y <= Bottom + Geometry.Eps;
}
