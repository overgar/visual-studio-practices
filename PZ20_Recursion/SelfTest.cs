namespace PZ20;

internal static class SelfTest
{
    private static int _passed;

    public static void Run()
    {
        _passed = 0;
        Check(Recursion.SumRecursive(new[] { 1, 7, 9 }) == 17, "сумма 1, 7, 9 = 17");
        Check(Recursion.SumRecursive(Array.Empty<int>()) == 0, "сумма пустой последовательности");
        Check(Recursion.SumRecursive(new[] { -5, 10, -2 }) == 3, "сумма с отрицательными числами");
        Check(Recursion.Factorial(0) == 1, "0! = 1");
        Check(Recursion.Factorial(5) == 120, "5! = 120");
        Check(Recursion.Factorial(10) == 3628800, "10! = 3628800");
        Check(Recursion.Factorial(20) == Recursion.FactorialIterative(20), "факториал 20 рекурсией и циклом");
        Check(Recursion.Factorial(100) == Recursion.FactorialIterative(100), "факториал 100 рекурсией и циклом");
        Check(Recursion.Fibonacci(0) == 0, "F0 = 0");
        Check(Recursion.Fibonacci(1) == 1, "F1 = 1");
        Check(Recursion.Fibonacci(10) == 55, "F10 = 55");
        Check(Recursion.Fibonacci(25) == Recursion.FibonacciIterative(25), "F25 рекурсией и циклом");
        Console.WriteLine($"ПЗ20: самопроверка пройдена, проверок — {_passed}.");
    }

    private static void Check(bool condition, string name)
    {
        if (!condition)
            throw new InvalidOperationException($"Самопроверка не пройдена: {name}.");
        _passed++;
        Console.WriteLine($"OK  {name}");
    }
}
