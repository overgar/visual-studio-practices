using System.Numerics;

namespace PZ20;

internal static class Recursion
{
    /// <summary>Сумма элементов последовательности без циклов.</summary>
    public static long SumRecursive(IReadOnlyList<int> numbers, int index = 0)
    {
        if (index >= numbers.Count)
            return 0;
        return checked(numbers[index] + SumRecursive(numbers, index + 1));
    }

    /// <summary>Факториал: базовый случай 0! = 1, затем рекурсивный спуск.</summary>
    public static BigInteger Factorial(int n)
    {
        if (n < 0)
            throw new ArgumentOutOfRangeException(nameof(n), "Факториал определён для неотрицательных чисел.");
        if (n == 0)
            return BigInteger.One;
        return n * Factorial(n - 1);
    }

    public static BigInteger FactorialIterative(int n)
    {
        if (n < 0)
            throw new ArgumentOutOfRangeException(nameof(n));
        var result = BigInteger.One;
        for (var i = 2; i <= n; i++)
            result *= i;
        return result;
    }

    public static BigInteger Fibonacci(int n)
    {
        if (n < 0)
            throw new ArgumentOutOfRangeException(nameof(n));
        if (n is 0 or 1)
            return n;
        return Fibonacci(n - 1) + Fibonacci(n - 2);
    }

    public static BigInteger FibonacciIterative(int n)
    {
        if (n < 0)
            throw new ArgumentOutOfRangeException(nameof(n));
        BigInteger a = 0, b = 1;
        for (var i = 0; i < n; i++)
            (a, b) = (b, a + b);
        return a;
    }
}
