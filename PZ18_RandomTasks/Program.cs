using System.Globalization;
using System.Text;

internal static class Program
{
    private static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
        CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("ru-RU");

        while (true)
        {
            Console.WriteLine("Контрольно-практическое занятие — список заданий");
            for (var i = 1; i <= 24; i++)
                Console.WriteLine($"{i,2}. {PracticeTasks.GetTitle(i)}");
            Console.WriteLine(" 0. Выход");
            Console.Write("Выберите задание (0 — выход): ");
            var text = Console.ReadLine();
            if (text is null || text.Trim() == "0")
                break;
            if (!int.TryParse(text, out var task) || task is < 1 or > 24)
            {
                Console.WriteLine("Введите номер от 1 до 24 или 0 для выхода.");
                continue;
            }
            Console.WriteLine();
            try
            {
                PracticeTasks.Run(task);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Операция отменена.");
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidOperationException or OverflowException)
            {
                Console.WriteLine($"Не удалось выполнить задание: {ex.Message}");
            }
            Console.WriteLine();
        }
    }
}
