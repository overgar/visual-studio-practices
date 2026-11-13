using System.Drawing;

namespace PZ27;

internal static class TextPrinter
{
    /// <summary>
    /// Рисует очередную страницу текста и возвращает true, если нужна следующая страница.
    /// </summary>
    public static bool DrawNextPage(Graphics graphics, string text, Font font, Rectangle bounds, ref int charIndex)
    {
        if (charIndex >= text.Length)
            return false;

        using var format = new StringFormat(StringFormatFlags.LineLimit)
        {
            Trimming = StringTrimming.Word,
            Alignment = StringAlignment.Near,
            LineAlignment = StringAlignment.Near
        };

        var remaining = text[charIndex..];
        graphics.MeasureString(remaining, font, bounds.Size, format, out var charsFitted, out _);
        if (charsFitted <= 0)
            charsFitted = Math.Min(1, remaining.Length);

        graphics.DrawString(remaining[..charsFitted], font, Brushes.Black, bounds, format);
        charIndex += charsFitted;
        return charIndex < text.Length;
    }
}
