namespace PZ24;

internal static class SelfTest
{
    private static int _passed;

    public static void Run()
    {
        _passed = 0;
        TestFigures();
        TestBuyers();
        TestSubscribers();
        TestForm();

        Console.WriteLine($"ПЗ24: самопроверка пройдена, проверок — {_passed}.");
    }

    private static void TestFigures()
    {
        var rectangle = new Rectangle(4, 3);
        Check(Math.Abs(rectangle.Area - 12) < 1e-12, "площадь прямоугольника");
        Check(Math.Abs(rectangle.Perimeter - 14) < 1e-12, "периметр прямоугольника");

        var circle = new Circle(2.5);
        Check(Math.Abs(circle.Area - Math.PI * 6.25) < 1e-12, "площадь круга");
        Check(Math.Abs(circle.Perimeter - 5 * Math.PI) < 1e-12, "длина окружности");

        var triangle = new Triangle(3, 4, 5);
        Check(Math.Abs(triangle.Area - 6) < 1e-12, "площадь треугольника");
        Check(Math.Abs(triangle.Perimeter - 12) < 1e-12, "периметр треугольника");

        Figure[] figures = { rectangle, circle, triangle };
        Check(figures.Length == 3 && figures.All(f => f.GetInfo().Length > 0), "массив фигур и вывод информации");
        Check(figures.Sum(f => f.Area) > 0, "суммарная площадь фигур");
    }

    private static void TestBuyers()
    {
        var buyers = DemoData.CreateBuyers();
        var text = DemoData.FormatBuyers(buyers, 2000, 5000);
        Check(text.Contains("Иванов"), "покупатель Иванов в списке");
        Check(text.Contains("Сидоров"), "покупатель Сидоров в списке");
        var sorted = buyers.OrderBy(b => b.Surname, StringComparer.CurrentCulture).Select(b => b.Surname).ToArray();
        Check(sorted.SequenceEqual(new[] { "Алексеева", "Борисов", "Иванов", "Петров", "Сидоров" }),
            "алфавитный порядок покупателей");
        Check(text.Contains("3100 3333") && text.Contains("4500 1111"), "фильтр по диапазону карты");
        Check(!text.Split("Покупатели с номером карты")[1].Contains("1200 3456"), "карта вне диапазона не попала в фильтр");
    }

    private static void TestSubscribers()
    {
        var subscribers = DemoData.CreateSubscribers();
        var text = DemoData.FormatSubscribers(subscribers, 100);
        Check(text.Contains("междугородные: 45 мин"), "абонент с междугородней связью");
        Check(text.Contains("Алексеева"), "абонент с городскими > 100");
        Check(!text.Split("Абоненты с городскими")[1].Split("Абоненты, пользовавшиеся")[0].Contains("Петров"),
            "Петров не попал в фильтр городских > 100");
        var sorted = subscribers.OrderBy(s => s.Surname, StringComparer.CurrentCulture).Select(s => s.Surname).ToArray();
        Check(sorted.SequenceEqual(new[] { "Алексеева", "Борисов", "Иванов", "Петров", "Сидоров", "Яковлев" }),
            "алфавитный порядок абонентов");
    }

    private static void TestForm()
    {
        using var form = new MainForm();
        var tab = form.Controls.OfType<Panel>().FirstOrDefault();
        Check(tab != null, "панель заданий ООП");
        var buttons = form.Controls.OfType<Button>().ToList();
        Check(buttons.Count >= 4, "кнопки упражнения и навигации");
        Check(form.Controls.OfType<ComboBox>().Count() == 2, "два ComboBox: цвет и размер");
        Check(form.BackColor == SystemColors.Control, "исходный цвет формы");
        var cmbColor = form.Controls.OfType<ComboBox>().First();
        Check(cmbColor.Items.Count == 5, "варианты цвета");
        var cmbSize = form.Controls.OfType<ComboBox>().Last();
        Check(cmbSize.Items.Count == 5, "варианты размера");
    }

    private static void Check(bool condition, string name)
    {
        if (!condition)
            throw new InvalidOperationException($"Самопроверка не пройдена: {name}.");
        _passed++;
        Console.WriteLine($"OK  {name}");
    }
}
