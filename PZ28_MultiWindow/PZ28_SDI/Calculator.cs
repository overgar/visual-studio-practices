namespace PZ28.Sdi;

/// <summary>Вычисления для формы ввода исходных данных и вывода результата.</summary>
public static class Calculator
{
    public static int[] Parse(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return Array.Empty<int>();

        return text
            .Split(new[] { ' ', ',', ';', '\t' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(part => int.Parse(part.Trim()))
            .ToArray();
    }

    public static int[] Filter(int[] numbers, int threshold, bool greater) =>
        numbers.Where(n => greater ? n > threshold : n < threshold).ToArray();

    public static long Sum(int[] numbers) => numbers.Sum(n => (long)n);

    public static long Product(int[] numbers)
    {
        long result = 1;
        foreach (var number in numbers)
            result *= number;
        return result;
    }
}
