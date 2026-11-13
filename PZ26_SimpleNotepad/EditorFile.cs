using System.Text;

namespace PZ26;

internal static class EditorFile
{
    public static string ReadText(string path) => File.ReadAllText(path, Encoding.UTF8);

    public static void WriteText(string path, string text) =>
        File.WriteAllText(path, text, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
}
