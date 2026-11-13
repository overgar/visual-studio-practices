using System.Text;

namespace PZ26;

internal static class SelfTest
{
    private static int _passed;

    public static void Run()
    {
        _passed = 0;
        var temp = Path.Combine(Path.GetTempPath(), "pz26_selftest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(temp);

        var textPath = Path.Combine(temp, "sample.txt");
        EditorFile.WriteText(textPath, "Привет, SimpleNotepad!\r\nВторая строка.");
        Check(EditorFile.ReadText(textPath) == "Привет, SimpleNotepad!\r\nВторая строка.", "запись и чтение текста");

        var rtfPath = Path.Combine(temp, "sample.rtf");
        using (var source = new RichTextBox { Text = "RTF документ" })
        {
            _ = source.Handle;
            source.SaveFile(rtfPath, RichTextBoxStreamType.RichText);
        }
        using (var target = new RichTextBox())
        {
            _ = target.Handle;
            target.LoadFile(rtfPath, RichTextBoxStreamType.RichText);
            Check(target.Text.Trim() == "RTF документ", "сохранение и загрузка RTF");
        }

        using (var form = new MainForm())
        {
            Check(form.Controls.OfType<MenuStrip>().Count() == 1, "меню");
            Check(form.Controls.OfType<ToolStrip>().Any(t => t is not StatusStrip and not MenuStrip), "панель инструментов");
            Check(form.Controls.OfType<RichTextBox>().Count() == 1, "RichTextBox");
            Check(form.Controls.OfType<StatusStrip>().Count() == 1, "строка состояния");
            var menu = form.Controls.OfType<MenuStrip>().First();
            Check(menu.Items.Count == 4, "четыре пункта меню");
            Check(menu.Items[0].Text == "&File" && menu.Items[1].Text == "&Edit" &&
                  menu.Items[2].Text == "F&ormat" && menu.Items[3].Text == "&Help", "File, Edit, Format, Help");
            var fileItems = ((ToolStripMenuItem)menu.Items[0]).DropDownItems;
            Check(fileItems.Count == 6, "File: New, Open, Save, Save As, разделитель, Exit");
        }

        try { Directory.Delete(temp, true); } catch { }
        Console.WriteLine($"ПЗ26: самопроверка пройдена, проверок — {_passed}.");
    }

    private static void Check(bool condition, string name)
    {
        if (!condition)
            throw new InvalidOperationException($"Самопроверка не пройдена: {name}.");
        _passed++;
        Console.WriteLine($"OK  {name}");
    }
}
