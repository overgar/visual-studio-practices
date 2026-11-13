namespace PZ23;

internal static class SelfTest
{
    private static int _passed;

    public static void Run()
    {
        _passed = 0;
        var temp = Path.Combine(Path.GetTempPath(), "pz23_selftest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(temp);
        var path = Path.Combine(temp, InsuranceFile.FileName);

        var first = new InsuranceRecord("Иванов Иван", "Москва", "Мира", "1", "10", "Мужской", 12, "Автострахование");
        var second = new InsuranceRecord("Петрова Анна", "Тверь", "Ленина", "5", "2", "Женский", 24, "Здоровье");

        Check(InsuranceRecord.TryValidate(first, out var error) && error.Length == 0, "корректная запись");
        Check(!InsuranceRecord.TryValidate(first with { FullName = "" }, out error) && error.Contains("фамилия"),
            "пустое ФИО");
        Check(!InsuranceRecord.TryValidate(first with { Apartment = " " }, out error) && error.Contains("квартира"),
            "пустая квартира");
        Check(!InsuranceRecord.TryValidate(first with { TermMonths = 0 }, out error), "нулевой срок");
        Check(first.Address == "г. Москва, ул. Мира, д. 1, кв. 10", "форматирование адреса");

        InsuranceFile.Append(path, first);
        var afterFirst = new FileInfo(path).Length;
        InsuranceFile.Append(path, second);
        var afterSecond = new FileInfo(path).Length;
        Check(afterSecond > afterFirst, "файл дописывается, а не перезаписывается");
        var blocks = InsuranceFile.ReadBlocks(path);
        Check(blocks.Length == 2, "в файле две записи");
        Check(blocks[0].Contains("Иванов Иван") && blocks[0].Contains("Автострахование"), "первая запись в файле");
        Check(blocks[1].Contains("Петрова Анна") && blocks[1].Contains("Здоровье"), "вторая запись в файле");

        using (var form = new Form1())
        {
            Check(form.Controls.Find("txtFullName", true).Length == 1, "поле ФИО");
            Check(form.Controls.Find("txtCity", true).Length == 1, "поле города");
            Check(form.Controls.Find("txtStreet", true).Length == 1, "поле улицы");
            Check(form.Controls.Find("txtHouse", true).Length == 1, "поле дома");
            Check(form.Controls.Find("txtApartment", true).Length == 1, "поле квартиры");
            Check(form.cmbGender.Items.Count == 2, "два варианта пола");
            Check(form.cmbInsuranceType.Items.Count >= 4, "виды страхования");
            Check(form.numTerm.Minimum == 1 && form.numTerm.Maximum == 120, "срок страхования");
        }

        try { Directory.Delete(temp, true); } catch { }
        Console.WriteLine($"ПЗ23: самопроверка пройдена, проверок — {_passed}.");
    }

    private static void Check(bool condition, string name)
    {
        if (!condition)
            throw new InvalidOperationException($"Самопроверка не пройдена: {name}.");
        _passed++;
        Console.WriteLine($"OK  {name}");
    }
}
