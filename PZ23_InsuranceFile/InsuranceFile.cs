using System.Text;

namespace PZ23;

internal static class InsuranceFile
{
    public const string FileName = "Страхователи.txt";

    public static string DefaultPath => Path.Combine(AppContext.BaseDirectory, FileName);

    /// <summary>Дописывает запись в файл, не перезаписывая предыдущие данные.</summary>
    public static void Append(string path, InsuranceRecord record)
    {
        var directory = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(directory))
            Directory.CreateDirectory(directory);
        File.AppendAllText(path, record.ToFileBlock(), new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
    }

    public static string[] ReadBlocks(string path)
    {
        if (!File.Exists(path))
            return Array.Empty<string>();
        return File.ReadAllText(path, Encoding.UTF8)
            .Split(new string('-', 50), StringSplitOptions.RemoveEmptyEntries)
            .Select(block => block.Trim())
            .Where(block => block.Length > 0)
            .ToArray();
    }
}
