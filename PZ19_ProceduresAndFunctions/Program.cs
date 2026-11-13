using System.Globalization;
using System.Text;

namespace PZ19;

internal static class Program
{
    private static int Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
        CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("ru-RU");

        if (args.Contains("--selftest", StringComparer.OrdinalIgnoreCase))
        {
            SelfTest.Run();
            return 0;
        }

        while (true)
        {
            PrintMenu();
            Console.Write("Выберите задание (0 — выход): ");
            var text = Console.ReadLine();
            if (text is null || text.Trim() == "0")
                return 0;

            if (!int.TryParse(text.Trim(), out var task) || task is < 1 or > 28)
            {
                Console.WriteLine("Введите номер от 1 до 28 или 0 для выхода.");
                continue;
            }

            try
            {
                if (task <= 14)
                    Group1.Run(task);
                else
                    Group2.Run(task - 14);
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Ошибка формата данных: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            Console.WriteLine();
            Console.WriteLine("Нажмите Enter, чтобы вернуться в меню.");
            Console.ReadLine();
        }
    }

    private static void PrintMenu()
    {
        Console.WriteLine();
        Console.WriteLine("=== ПЗ19: организация процедур и функций ===");
        Console.WriteLine("Подгруппа 1");
        Console.WriteLine(" 1  Решение квадратного уравнения ax^2 + bx + c = 0");
        Console.WriteLine(" 2  Площадь треугольника по формуле Герона");
        Console.WriteLine(" 3  Произведение нечётных чисел (for, while, do-while)");
        Console.WriteLine(" 4  Произведение чётных чисел, делящихся на 4");
        Console.WriteLine(" 5  Сумма 1 + 2 + 4 + ... + 2^N (цикл и формула)");
        Console.WriteLine(" 6  Сумма 1 + 1/2 + 1/4 + ... + 1/2^N (цикл и формула)");
        Console.WriteLine(" 7  cos(x) разложением в ряд");
        Console.WriteLine(" 8  tg(x) через ряды sin(x) и cos(x)");
        Console.WriteLine(" 9  a, при которых НОД(y1(a), y2(a)) > 1");
        Console.WriteLine("10  Разложение бинома Ньютона (x + a)^n");
        Console.WriteLine("11  Конвертор валют (switch-case)");
        Console.WriteLine("12  Перевод десятичного числа в 2, 8 и 16 системы");
        Console.WriteLine("13  Перевод шестнадцатеричного числа в десятичное");
        Console.WriteLine("14  Число Фибоначчи и золотое сечение");
        Console.WriteLine("Подгруппа 2");
        Console.WriteLine("15  Сортировка 3D-точек и расстояния между ними");
        Console.WriteLine("16  Пересечение граней многоугольника, периметр и площадь");
        Console.WriteLine("17  Порядок точек простого многоугольника");
        Console.WriteLine("18  Пары точек без пересечения отрезков");
        Console.WriteLine("19  Комплексные числа: модуль, квадрант, произведение");
        Console.WriteLine("20  Проверка, что точки — вершины правильного многоугольника");
        Console.WriteLine("21  Прямоугольники: сортировка по площади и ориентация");
        Console.WriteLine("22  Длины ломаных линий по цветам");
        Console.WriteLine("23  Непересекающаяся ломаная и минимальный сегмент");
        Console.WriteLine("24  Охватывающий прямоугольник и поиск по точке");
        Console.WriteLine("25  Звёздный каталог");
        Console.WriteLine("26  Треугольники: сортировка и группы подобных");
        Console.WriteLine("27  Правильные многоугольники: площадь и периметр");
        Console.WriteLine("28  Сортировка 3D-точек по приоритету координат");
        Console.WriteLine(" 0  Выход");
    }
}
