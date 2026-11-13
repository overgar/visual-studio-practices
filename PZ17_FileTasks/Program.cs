using System.Text;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

var outputDirectory = args.Length > 1
    ? Path.GetFullPath(args[1])
    : Directory.GetCurrentDirectory();
Directory.CreateDirectory(outputDirectory);

var inputFile = args.Length > 0
    ? Path.GetFullPath(args[0])
    : Path.Combine(AppContext.BaseDirectory, "sample.txt");

if (!File.Exists(inputFile))
{
    Console.Error.WriteLine($"Текстовый файл не найден: {inputFile}");
    return 1;
}

// Задание 1: натуральные числа 1–500 в одной строке через запятую.
var numbersPath = Path.Combine(outputDirectory, "numbers.txt");
var numbers = string.Join(',', Enumerable.Range(1, 500));
File.WriteAllText(numbersPath, numbers, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));

// Задание 2: каждый цвет массива записывается с новой строки.
var colorsPath = Path.Combine(outputDirectory, "colors.txt");
string[] colors = ["red", "green", "black", "white", "blue"];
File.WriteAllLines(colorsPath, colors, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));

// Задание 3: длина самой длинной строки выбранного текстового файла.
var longestLineLength = File.ReadLines(inputFile)
    .Select(line => line.Length)
    .DefaultIfEmpty(0)
    .Max();

Console.WriteLine($"Задание 1: записано 500 чисел в {numbersPath}");
Console.WriteLine($"Задание 2: записано 5 строк в {colorsPath}");
Console.WriteLine($"Задание 3: длина самой длинной строки в {inputFile}: {longestLineLength}");
return 0;
