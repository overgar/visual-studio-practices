namespace PZ25;

internal static class SelfTest
{
    private static int _passed;

    public static void Run()
    {
        _passed = 0;
        var date = new DateTime(2015, 7, 20, 18, 30, 25);

        Check(date.ToString("dd.MM.yyyy", DateTimeLogic.Ru) == "20.07.2015", "формат dd.MM.yyyy");
        Check(date.ToString("HH:mm:ss", DateTimeLogic.Ru) == "18:30:25", "формат HH:mm:ss");
        Check(date.ToString("dddd", DateTimeLogic.Ru) == "понедельник", "день недели");
        Check(date.ToString("D", DateTimeLogic.Ru).Contains("20 июля 2015"), "длинный формат даты");
        Check(date.AddHours(3).Hour == 21, "AddHours(3)");
        Check(date.AddDays(-20).Day == 30, "AddDays(-20)");
        Check(date.Subtract(new DateTime(2015, 7, 20, 15, 30, 25)).TotalHours == 3, "Subtract");
        Check(DateTimeLogic.Age(new DateTime(2000, 1, 1), new DateTime(2026, 11, 13)) == (26, 10, 12), "возраст");
        Check(DateTimeLogic.Age(new DateTime(2000, 12, 31), new DateTime(2026, 11, 13)) == (25, 10, 13), "возраст до дня рождения");
        Check(DateTimeLogic.IsLeapYear(2024) && !DateTimeLogic.IsLeapYear(2025), "високосный год");
        Check(DateTimeLogic.DaysInMonth(2024, 2) == 29, "дней в феврале 2024");
        Check(date.ToString("O", DateTimeLogic.Ru).Contains("2015-07-20T18:30:25"), "формат O");
        Check(DateTimeLogic.FormatAll(date).Contains("2015") &&
              DateTimeLogic.FormatAll(date).Contains("июль", StringComparison.OrdinalIgnoreCase),
            "все стандартные форматы");

        using var form = new MainForm();
        Check(form.Controls.OfType<DateTimePicker>().Count() == 2, "два DateTimePicker");
        Check(form.Controls.OfType<NumericUpDown>().Count() == 1, "NumericUpDown для дней");
        Check(form.Controls.OfType<Button>().Count() >= 5, "кнопки операций с датами");

        Console.WriteLine($"ПЗ25: самопроверка пройдена, проверок — {_passed}.");
    }

    private static void Check(bool condition, string name)
    {
        if (!condition)
            throw new InvalidOperationException($"Самопроверка не пройдена: {name}.");
        _passed++;
        Console.WriteLine($"OK  {name}");
    }
}
