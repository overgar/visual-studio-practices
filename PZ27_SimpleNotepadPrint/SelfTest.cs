using System.Drawing;
using System.Drawing.Printing;

namespace PZ27;

internal static class SelfTest
{
    private static int _passed;

    public static void Run()
    {
        _passed = 0;
        var temp = Path.Combine(Path.GetTempPath(), "pz27_selftest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(temp);

        var textPath = Path.Combine(temp, "document.txt");
        EditorFile.WriteText(textPath, "Печать и сохранение\r\nВторая строка");
        Check(EditorFile.ReadText(textPath).Contains("Печать и сохранение"), "сохранение и чтение текста");

        var rtfPath = Path.Combine(temp, "document.rtf");
        using (var source = new RichTextBox { Text = "Документ для печати" })
        {
            _ = source.Handle;
            source.SaveFile(rtfPath, RichTextBoxStreamType.RichText);
        }
        using (var target = new RichTextBox())
        {
            _ = target.Handle;
            target.LoadFile(rtfPath, RichTextBoxStreamType.RichText);
            Check(target.Text.Trim() == "Документ для печати", "сохранение и загрузка RTF");
        }

        var document = new PrintDocument { DocumentName = "SimpleNotepad Document" };
        Check(document.DocumentName == "SimpleNotepad Document", "имя печатаемого документа");

        var longText = string.Join("\r\n", Enumerable.Repeat(
            "Строка текста для проверки постраничной печати. ", 120));
        var index = 0;
        using var font = new Font("Arial", 10F);
        document.PrintPage += (_, e) =>
        {
            if (e.Graphics is null)
            {
                e.HasMorePages = false;
                return;
            }
            e.HasMorePages = TextPrinter.DrawNextPage(e.Graphics, longText, font, e.MarginBounds, ref index);
        };
        var controller = new PreviewPrintController();
        document.PrintController = controller;
        document.Print();
        var pages = controller.GetPreviewPageInfo();
        Check(pages.Length >= 2, "печать разбивается на страницы");
        Check(index >= longText.Length - 2, "весь текст выведен на страницы");

        using (var form = new MainForm())
        {
            Check(form.Controls.OfType<MenuStrip>().Count() == 1, "меню");
            Check(form.Controls.OfType<RichTextBox>().Count() == 1, "RichTextBox");
            Check(form.Controls.OfType<StatusStrip>().Count() == 1, "строка состояния");
            Check(form.PrintDocument.DocumentName == "SimpleNotepad Document", "PrintDocument в форме");
            var menu = form.Controls.OfType<MenuStrip>().First();
            var fileItems = ((ToolStripMenuItem)menu.Items[0]).DropDownItems;
            Check(fileItems.Count >= 10, "File: сохранение, параметры страницы, просмотр, печать, выход");
            Check(fileItems.OfType<ToolStripMenuItem>().Any(i => i.Text?.Contains("Print") == true), "пункт Print");
        }

        try { Directory.Delete(temp, true); } catch { }
        Console.WriteLine($"ПЗ27: самопроверка пройдена, проверок — {_passed}.");
    }

    private static void Check(bool condition, string name)
    {
        if (!condition)
            throw new InvalidOperationException($"Самопроверка не пройдена: {name}.");
        _passed++;
        Console.WriteLine($"OK  {name}");
    }
}
