namespace PZ28.Sdi;

internal static class SelfTest
{
    private static int _passed;

    internal static void Run()
    {
        // Логика вычислений.
        var numbers = Calculator.Parse("5 12 -3 8 20");
        Check(numbers.Length == 5, "разбор строки в массив");
        Check(Calculator.Parse("   ").Length == 0, "пустая строка — пустой массив");
        Check(Calculator.Parse("1,2;3 4").SequenceEqual(new[] { 1, 2, 3, 4 }), "разделители , ; и пробел");

        var greater = Calculator.Filter(numbers, 5, greater: true);
        Check(greater.SequenceEqual(new[] { 12, 8, 20 }), "отбор чисел больше заданного");
        var less = Calculator.Filter(numbers, 5, greater: false);
        Check(less.SequenceEqual(new[] { -3 }), "отбор чисел меньше заданного");
        Check(Calculator.Sum(greater) == 40, "сумма отобранных чисел");
        Check(Calculator.Product(new[] { 2, 3, 4 }) == 24, "произведение чисел");
        Check(Calculator.Product(Array.Empty<int>()) == 1, "произведение пустого набора");

        // Структура SDI-приложения.
        using (var main = new MainForm())
        {
            var menu = main.Controls.OfType<MenuStrip>().Single();
            Check(menu.Items.Count == 3, "три раздела меню: File, Windows, Help");
            Check(Plain(menu.Items[0].Text) == "File", "раздел File");
            Check(Plain(menu.Items[1].Text) == "Windows", "раздел Windows");
            Check(Plain(menu.Items[2].Text) == "Help", "раздел Help");

            var fileItems = ((ToolStripMenuItem)menu.Items[0]).DropDownItems;
            Check(fileItems.OfType<ToolStripMenuItem>().Any(i => Plain(i.Text) == "Quit"), "File → Quit");

            var windowsItems = ((ToolStripMenuItem)menu.Items[1]).DropDownItems;
            Check(windowsItems.OfType<ToolStripMenuItem>().Any(i => Plain(i.Text) == "New"), "Windows → New");
            Check(windowsItems.OfType<ToolStripMenuItem>().Any(i => Plain(i.Text) == "Open Dialog"), "Windows → Open Dialog");

            var helpItems = ((ToolStripMenuItem)menu.Items[2]).DropDownItems;
            Check(helpItems.OfType<ToolStripMenuItem>().Any(i => Plain(i.Text) == "About"), "Help → About");

            var mode = main.ModeWindow;
            Check(!mode.ControlBox, "у формы режимов убрана кнопка закрытия");
            Check(mode.StartPosition == FormStartPosition.Manual, "форма режимов позиционируется вручную");
            Check(mode.rB1 is not null && mode.rB2 is not null, "радиокнопки rB1 и rB2");
            Check(mode.cB1 is not null && mode.cB2 is not null, "кнопки выбора cB1 и cB2");
            Check(mode.textBox1 is not null && mode.button1.Text == "Скрыть", "строка редактирования и кнопка «Скрыть»");
        }

        // Форма ввода/вывода: отбор «больше» и обе операции.
        using (var mode = new ModeForm())
        using (var form = new InputResultForm { f22 = mode })
        {
            mode.rB1.Checked = true;
            mode.cB1.Checked = true;
            mode.cB2.Checked = true;
            mode.textBox1.Text = "5";
            form.textBox1.Text = "5 12 -3 8 20";
            form.Compute();

            Check(form.OutputItems.Count == 3, "в списке три числа, прошедших отбор");
            Check(form.ResultText.Contains("Сумма = 40"), "выведена сумма");
            Check(form.ResultText.Contains("Произведение = 1920"), "выведено произведение");

            // Отбор «меньше» и только сумма.
            mode.rB2.Checked = true;
            mode.cB1.Checked = true;
            mode.cB2.Checked = false;
            form.Compute();
            Check(form.OutputItems.Count == 1 && form.OutputItems[0] == "-3", "отбор «меньше» дал одно число");
            Check(form.ResultText == "Сумма = -3", "выведена только сумма");

            // Неверный ввод.
            form.textBox1.Text = "abc";
            form.Compute();
            Check(form.ResultText.StartsWith("Ошибка"), "сообщение об ошибке при неверном вводе");
        }

        using (var about = new AboutForm())
        {
            Check(about.Controls.OfType<Button>().Any(b => b.Text == "OK"), "в форме «О программе» есть кнопка OK");
        }

        Console.WriteLine($"ПЗ28 (SDI): самопроверка пройдена, проверок — {_passed}.");
    }

    private static string Plain(string? text) => (text ?? string.Empty).Replace("&", string.Empty);

    private static void Check(bool condition, string name)
    {
        if (!condition)
            throw new InvalidOperationException($"Самопроверка не пройдена: {name}.");
        _passed++;
        Console.WriteLine($"OK  {name}");
    }
}
