namespace PZ21;

/// <summary>Класс с перегруженным методом Average для int[] и float[].</summary>
internal static class AverageMethods
{
    public static double Average(int[] values)
    {
        if (values is null)
            throw new ArgumentNullException(nameof(values));
        if (values.Length == 0)
            throw new ArgumentException("Массив не должен быть пустым.", nameof(values));

        long sum = 0;
        foreach (var value in values)
            sum += value;
        return (double)sum / values.Length;
    }

    public static float Average(float[] values)
    {
        if (values is null)
            throw new ArgumentNullException(nameof(values));
        if (values.Length == 0)
            throw new ArgumentException("Массив не должен быть пустым.", nameof(values));

        double sum = 0;
        foreach (var value in values)
            sum += value;
        return (float)(sum / values.Length);
    }
}
