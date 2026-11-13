using System.Text;

namespace PZ24;

internal static class DemoData
{
    public static Figure[] CreateFigures() => new Figure[]
    {
        new Rectangle(4, 3),
        new Circle(2.5),
        new Triangle(3, 4, 5)
    };

    public static Buyer[] CreateBuyers() => new[]
    {
        new Buyer("Сидоров", "Пётр", "Иванович", "Москва, ул. Мира, 1", "1200 3456", "40817810000000000001"),
        new Buyer("Иванов", "Иван", "Иванович", "Тверь, ул. Ленина, 5", "4500 1111", "40817810000000000002"),
        new Buyer("Алексеева", "Анна", "Сергеевна", "Москва, ул. Гагарина, 7", "8000 2222", "40817810000000000003"),
        new Buyer("Петров", "Сергей", "Петрович", "Казань, ул. Баумана, 3", "3100 3333", "40817810000000000004"),
        new Buyer("Борисов", "Борис", "Борисович", "Москва, ул. Тверская, 9", "500 4444", "40817810000000000005")
    };

    public static Subscriber[] CreateSubscribers() => new[]
    {
        new Subscriber(1, "Сидоров", "Пётр", "Иванович", "Москва, ул. Мира, 1", "1200 3456", 1500m, 0m, 0, 120),
        new Subscriber(2, "Иванов", "Иван", "Иванович", "Тверь, ул. Ленина, 5", "4500 1111", 800m, 200m, 45, 30),
        new Subscriber(3, "Алексеева", "Анна", "Сергеевна", "Москва, ул. Гагарина, 7", "8000 2222", 0m, 500m, 0, 210),
        new Subscriber(4, "Петров", "Сергей", "Петрович", "Казань, ул. Баумана, 3", "3100 3333", 300m, 0m, 90, 15),
        new Subscriber(5, "Борисов", "Борис", "Борисович", "Москва, ул. Тверская, 9", "500 4444", 100m, 0m, 0, 60),
        new Subscriber(6, "Яковлев", "Яков", "Яковлевич", "Сочи, ул. Морская, 2", "9000 5555", 50m, 0m, 10, 140)
    };

    public static string FormatFigures(Figure[] figures)
    {
        var builder = new StringBuilder();
        builder.AppendLine($"Массив из {figures.Length} фигур:");
        foreach (var figure in figures)
            builder.AppendLine("  " + figure.GetInfo());
        builder.AppendLine();
        builder.AppendLine($"Суммарная площадь: {figures.Sum(f => f.Area):0.###}");
        builder.AppendLine($"Суммарный периметр: {figures.Sum(f => f.Perimeter):0.###}");
        return builder.ToString();
    }

    public static string FormatBuyers(Buyer[] buyers, int cardFrom = 2000, int cardTo = 5000)
    {
        var builder = new StringBuilder();
        builder.AppendLine("Покупатели в алфавитном порядке:");
        foreach (var buyer in buyers.OrderBy(b => b.Surname, StringComparer.CurrentCulture)
                                    .ThenBy(b => b.Name, StringComparer.CurrentCulture)
                                    .ThenBy(b => b.Patronymic, StringComparer.CurrentCulture))
        {
            builder.AppendLine("  " + buyer.GetInfo());
        }

        builder.AppendLine();
        builder.AppendLine($"Покупатели с номером карты в диапазоне {cardFrom}–{cardTo}:");
        var found = buyers.Where(b => TryCardNumber(b, out var number) && number >= cardFrom && number <= cardTo).ToList();
        if (found.Count == 0)
            builder.AppendLine("  нет");
        else
            foreach (var buyer in found)
                builder.AppendLine("  " + buyer.GetInfo());
        return builder.ToString();
    }

    private static bool TryCardNumber(Buyer buyer, out int number)
    {
        var digits = new string(buyer.CreditCard.Where(char.IsDigit).ToArray());
        return int.TryParse(digits, out number);
    }

    public static string FormatSubscribers(Subscriber[] subscribers, int cityThreshold = 100)
    {
        var builder = new StringBuilder();
        builder.AppendLine($"Абоненты с городскими переговорами больше {cityThreshold} минут:");
        foreach (var subscriber in subscribers.Where(s => s.CityMinutes > cityThreshold))
            builder.AppendLine("  " + subscriber.GetInfo());

        builder.AppendLine();
        builder.AppendLine("Абоненты, пользовавшиеся междугородной связью:");
        foreach (var subscriber in subscribers.Where(s => s.IntercityMinutes > 0))
            builder.AppendLine("  " + subscriber.GetInfo());

        builder.AppendLine();
        builder.AppendLine("Список абонентов в алфавитном порядке:");
        foreach (var subscriber in subscribers.OrderBy(s => s.Surname, StringComparer.CurrentCulture)
                                              .ThenBy(s => s.Name, StringComparer.CurrentCulture)
                                              .ThenBy(s => s.Patronymic, StringComparer.CurrentCulture))
            builder.AppendLine("  " + subscriber.GetInfo());
        return builder.ToString();
    }
}
