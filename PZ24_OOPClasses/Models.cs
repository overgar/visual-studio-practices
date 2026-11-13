namespace PZ24;

internal abstract class Figure
{
    protected Figure(string name)
    {
        Name = name;
    }

    public string Name { get; }
    public abstract double Area { get; }
    public abstract double Perimeter { get; }

    public virtual string GetInfo() =>
        $"{Name}: площадь = {Area:0.###}, периметр = {Perimeter:0.###}";

    public void PrintInfo() => Console.WriteLine(GetInfo());
}

internal sealed class Rectangle : Figure
{
    public Rectangle(double width, double height) : base("Прямоугольник")
    {
        if (width <= 0 || height <= 0)
            throw new ArgumentOutOfRangeException(nameof(width), "Стороны должны быть положительными.");
        Width = width;
        Height = height;
    }

    public double Width { get; }
    public double Height { get; }
    public override double Area => Width * Height;
    public override double Perimeter => 2 * (Width + Height);
    public override string GetInfo() => base.GetInfo() + $"; стороны {Width:0.###}×{Height:0.###}";
}

internal sealed class Circle : Figure
{
    public Circle(double radius) : base("Круг")
    {
        if (radius <= 0)
            throw new ArgumentOutOfRangeException(nameof(radius), "Радиус должен быть положительным.");
        Radius = radius;
    }

    public double Radius { get; }
    public override double Area => Math.PI * Radius * Radius;
    public override double Perimeter => 2 * Math.PI * Radius;
    public override string GetInfo() => base.GetInfo() + $"; радиус {Radius:0.###}";
}

internal sealed class Triangle : Figure
{
    public Triangle(double a, double b, double c) : base("Треугольник")
    {
        if (a <= 0 || b <= 0 || c <= 0)
            throw new ArgumentOutOfRangeException(nameof(a), "Стороны должны быть положительными.");
        if (a + b <= c || a + c <= b || b + c <= a)
            throw new ArgumentException("Из таких сторон треугольник построить нельзя.");
        A = a;
        B = b;
        C = c;
    }

    public double A { get; }
    public double B { get; }
    public double C { get; }
    public override double Area
    {
        get
        {
            var p = (A + B + C) / 2;
            return Math.Sqrt(p * (p - A) * (p - B) * (p - C));
        }
    }
    public override double Perimeter => A + B + C;
    public override string GetInfo() => base.GetInfo() + $"; стороны {A:0.###}, {B:0.###}, {C:0.###}";
}

internal sealed class Buyer
{
    public Buyer(string surname, string name, string patronymic, string address, string creditCard, string bankAccount)
    {
        Surname = surname;
        Name = name;
        Patronymic = patronymic;
        Address = address;
        CreditCard = creditCard;
        BankAccount = bankAccount;
    }

    public string Surname { get; private set; }
    public string Name { get; private set; }
    public string Patronymic { get; private set; }
    public string Address { get; private set; }
    public string CreditCard { get; private set; }
    public string BankAccount { get; private set; }

    public string FullName => $"{Surname} {Name} {Patronymic}".Trim();

    public void SetAddress(string address) => Address = address;
    public void SetCreditCard(string creditCard) => CreditCard = creditCard;
    public void SetBankAccount(string bankAccount) => BankAccount = bankAccount;
    public string GetFullName() => FullName;
    public string GetCreditCard() => CreditCard;

    public string GetInfo() =>
        $"{FullName}; адрес: {Address}; карта: {CreditCard}; счёт: {BankAccount}";
}

internal sealed class Subscriber
{
    public Subscriber(int id, string surname, string name, string patronymic, string address,
        string creditCard, decimal debit, decimal credit, int intercityMinutes, int cityMinutes)
    {
        Id = id;
        Surname = surname;
        Name = name;
        Patronymic = patronymic;
        Address = address;
        CreditCard = creditCard;
        Debit = debit;
        Credit = credit;
        IntercityMinutes = intercityMinutes;
        CityMinutes = cityMinutes;
    }

    public int Id { get; }
    public string Surname { get; private set; }
    public string Name { get; private set; }
    public string Patronymic { get; private set; }
    public string Address { get; private set; }
    public string CreditCard { get; private set; }
    public decimal Debit { get; private set; }
    public decimal Credit { get; private set; }
    public int IntercityMinutes { get; private set; }
    public int CityMinutes { get; private set; }

    public string FullName => $"{Surname} {Name} {Patronymic}".Trim();

    public void SetAddress(string address) => Address = address;
    public void SetBalances(decimal debit, decimal credit) => (Debit, Credit) = (debit, credit);

    public string GetInfo() =>
        $"№{Id}: {FullName}; адрес: {Address}; карта: {CreditCard}; дебет: {Debit:0.00}; кредит: {Credit:0.00}; " +
        $"междугородные: {IntercityMinutes} мин; городские: {CityMinutes} мин";
}
