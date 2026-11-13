using System.Globalization;
using System.Text;
using System.Xml.Serialization;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

var peopleFile = Path.Combine(AppContext.BaseDirectory, "people.xml");
Person[] people;

try
{
    people = LoadPeople(peopleFile);
}
catch (Exception ex)
{
    Console.Error.WriteLine($"Не удалось загрузить список учеников: {ex.Message}");
    return;
}

if (people.Length == 0)
{
    Console.WriteLine("Список учеников пуст.");
    return;
}

while (true)
{
    PrintMenu();
    Console.Write("Введите команду: ");
    var command = Console.ReadLine()?.Trim();

    switch (command)
    {
        case "0":
            return;
        case "1":
            ShowExtremum(people, best: true);
            break;
        case "2":
            ShowExtremum(people, best: false);
            break;
        case "3":
            Console.WriteLine("Список");
            foreach (var person in people)
                PrintPerson(person);
            break;
        case "4":
            ShowAboveAverage(people);
            break;
        case null:
            return;
        default:
            Console.WriteLine("Неизвестная команда. Введите число от 0 до 4.");
            break;
    }
}

static Person[] LoadPeople(string path)
{
    if (!File.Exists(path))
        throw new FileNotFoundException("Файл people.xml не найден рядом с программой.", path);

    var serializer = new XmlSerializer(typeof(Person[]));
    using var stream = File.OpenRead(path);
    if (serializer.Deserialize(stream) is not Person[] people)
        throw new InvalidDataException("Файл не содержит сериализованный массив учеников.");

    foreach (var person in people)
    {
        if (string.IsNullOrWhiteSpace(person.Name))
            throw new InvalidDataException("У одного из учеников не указано имя.");
        if (person.Marks is null || person.Marks.Length != 5)
            throw new InvalidDataException($"У ученика «{person.Name}» должно быть ровно 5 оценок.");
    }

    return people;
}

static void PrintMenu()
{
    Console.WriteLine();
    Console.WriteLine("Команды:");
    Console.WriteLine("0 - Выход;");
    Console.WriteLine("1 - Показать самого лучшего ученика;");
    Console.WriteLine("2 - Показать самого худшего ученика;");
    Console.WriteLine("3 - Показать весь список;");
    Console.WriteLine("4 - Показать учеников со средним баллом больше заданного;");
}

static decimal Average(Person person) => person.Marks.Average(mark => (decimal)mark);

static void PrintPerson(Person person)
{
    Console.WriteLine($"Имя: {person.Name}, Оценки: {string.Join(' ', person.Marks)}");
}

static void ShowExtremum(IEnumerable<Person> people, bool best)
{
    var selected = best
        ? people.OrderByDescending(Average).First()
        : people.OrderBy(Average).First();

    Console.WriteLine(best ? "Самый лучший ученик:" : "Самый худший ученик:");
    PrintPerson(selected);
    Console.WriteLine($"Средний балл: {Average(selected).ToString("0.0", CultureInfo.GetCultureInfo("ru-RU"))}");
}

static void ShowAboveAverage(IEnumerable<Person> people)
{
    Console.Write("Введите порог среднего балла: ");
    var text = Console.ReadLine()?.Trim();
    if (string.IsNullOrEmpty(text) ||
        !decimal.TryParse(text.Replace(',', '.'), NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint,
            CultureInfo.InvariantCulture, out var threshold))
    {
        Console.WriteLine("Порог должен быть числом, например 4,0.");
        return;
    }

    var matching = people.Where(person => Average(person) > threshold).ToArray();
    if (matching.Length == 0)
    {
        Console.WriteLine("Подходящих учеников не найдено.");
        return;
    }

    Console.WriteLine($"Ученики со средним баллом больше {threshold.ToString("0.##", CultureInfo.GetCultureInfo("ru-RU"))}:");
    foreach (var person in matching)
    {
        PrintPerson(person);
        Console.WriteLine($"Средний балл: {Average(person).ToString("0.0", CultureInfo.GetCultureInfo("ru-RU"))}");
    }
}

[Serializable]
public class Person
{
    public string Name { get; set; } = string.Empty;
    public int[] Marks { get; set; } = [];

    public Person() { }

    public Person(string name, int[] marks)
    {
        Name = name;
        Marks = marks;
    }
}
