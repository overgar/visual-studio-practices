using System.Globalization;
using System.Text;

namespace PZ25;

internal static class DateTimeLogic
{
    public static readonly CultureInfo Ru = CultureInfo.GetCultureInfo("ru-RU");

    public static string FormatAll(DateTime value, CultureInfo? culture = null)
    {
        culture ??= Ru;
        var builder = new StringBuilder();
        builder.AppendLine("Стандартные форматы:");
        builder.AppendLine("D: " + value.ToString("D", culture));
        builder.AppendLine("d: " + value.ToString("d", culture));
        builder.AppendLine("F: " + value.ToString("F", culture));
        builder.AppendLine("f: " + value.ToString("f", culture));
        builder.AppendLine("G: " + value.ToString("G", culture));
        builder.AppendLine("g: " + value.ToString("g", culture));
        builder.AppendLine("M: " + value.ToString("M", culture));
        builder.AppendLine("O: " + value.ToString("O", culture));
        builder.AppendLine("R: " + value.ToString("R", culture));
        builder.AppendLine("s: " + value.ToString("s", culture));
        builder.AppendLine("T: " + value.ToString("T", culture));
        builder.AppendLine("t: " + value.ToString("t", culture));
        builder.AppendLine("U: " + value.ToString("U", culture));
        builder.AppendLine("u: " + value.ToString("u", culture));
        builder.AppendLine("Y: " + value.ToString("Y", culture));
        builder.AppendLine();
        builder.AppendLine("Собственные форматы:");
        builder.AppendLine("dd.MM.yyyy: " + value.ToString("dd.MM.yyyy", culture));
        builder.AppendLine("dddd, d MMMM yyyy: " + value.ToString("dddd, d MMMM yyyy", culture));
        builder.AppendLine("HH:mm:ss: " + value.ToString("HH:mm:ss", culture));
        builder.AppendLine("hh:mm tt: " + value.ToString("hh:mm tt", culture));
        return builder.ToString();
    }

    public static string DescribeDifference(DateTime first, DateTime second)
    {
        var difference = first.Subtract(second);
        var absolute = difference.Duration();
        var builder = new StringBuilder();
        builder.AppendLine($"Первая дата:  {first:dd.MM.yyyy HH:mm:ss}");
        builder.AppendLine($"Вторая дата:  {second:dd.MM.yyyy HH:mm:ss}");
        builder.AppendLine($"Разность:     {difference}");
        builder.AppendLine($"По модулю:    {absolute}");
        builder.AppendLine($"Всего дней:   {absolute.TotalDays:0.####}");
        builder.AppendLine($"Всего часов:  {absolute.TotalHours:0.####}");
        builder.AppendLine($"Всего минут:  {absolute.TotalMinutes:0.####}");
        builder.AppendLine($"Дней: {absolute.Days}, часов: {absolute.Hours}, минут: {absolute.Minutes}, секунд: {absolute.Seconds}.");
        return builder.ToString();
    }

    public static (int Years, int Months, int Days) Age(DateTime birthDate, DateTime onDate)
    {
        if (onDate < birthDate)
            throw new ArgumentException("Дата расчёта не может быть раньше даты рождения.");

        var years = onDate.Year - birthDate.Year;
        if (onDate < birthDate.AddYears(years))
            years--;

        var anniversary = birthDate.AddYears(years);
        var months = 0;
        while (anniversary.AddMonths(months + 1) <= onDate)
            months++;
        var last = anniversary.AddMonths(months);
        var days = (onDate - last).Days;
        return (years, months, days);
    }

    public static bool IsLeapYear(int year) => DateTime.IsLeapYear(year);

    public static int DaysInMonth(int year, int month) => DateTime.DaysInMonth(year, month);
}
