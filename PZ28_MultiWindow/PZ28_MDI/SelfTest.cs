using System.Reflection;

namespace PZ28.Mdi;

internal static class SelfTest
{
    private static int _passed;

    internal static void Run()
    {
        MainForm.InitializeSettings();
        Check(MainForm.rgi == 1, "начальный номер радиокнопки (rgi = 1)");
        Check(MainForm.b1, "начальное значение b1 = true");
        Check(!MainForm.b2, "начальное значение b2 = false");
        Check(MainForm.s1 == "10", "начальное значение строки редактирования");

        using (var main = new MainForm())
        {
            Check(main.IsMdiContainer, "главная форма является MDI-контейнером");

            var menu = main.Controls.OfType<MenuStrip>().Single();
            Check(menu.Items.Count == 4, "четыре раздела меню: Файл, Окно, Диалог, Помощь");
            Check(menu.Items[0].Text == "Файл", "раздел Файл");
            Check(menu.Items[1].Text == "Окно", "раздел Окно");
            Check(menu.Items[2].Text == "Диалог", "раздел Диалог");
            Check(menu.Items[3].Text == "Помощь", "раздел Помощь");

            var windowItems = ((ToolStripMenuItem)menu.Items[1]).DropDownItems;
            var windowTexts = windowItems.OfType<ToolStripMenuItem>().Select(i => i.Text).ToArray();
            Check(windowTexts.Contains("Создать"), "Окно → Создать");
            Check(windowTexts.Contains("Каскад"), "Окно → Каскад");
            Check(windowTexts.Contains("Мозаика горизонтально"), "Окно → Мозаика горизонтально");
            Check(windowTexts.Contains("Мозаика вертикально"), "Окно → Мозаика вертикально");
            Check(windowTexts.Contains("Закрыть текущее"), "Окно → Закрыть текущее");
            Check(windowTexts.Contains("Закрыть все"), "Окно → Закрыть все");
            Check(main.FormA.Text == "Единственная форма", "Окно → Единственная форма");
            Check(main.FormA.Enabled, "пункт «Единственная форма» доступен");

            var fileItems = ((ToolStripMenuItem)menu.Items[0]).DropDownItems;
            Check(fileItems.OfType<ToolStripMenuItem>().Any(i => i.Text == "Выход"), "Файл → Выход");
            var dialogItems = ((ToolStripMenuItem)menu.Items[2]).DropDownItems;
            Check(dialogItems.OfType<ToolStripMenuItem>().Any(i => i.Text == "Открыть"), "Диалог → Открыть");

            // Диалог настроек: загрузка текущих и сохранение новых значений.
            using (var dialog = new SettingsDialog())
            {
                dialog.LoadSettings();
                Check(dialog.rB1.Checked && !dialog.rB2.Checked, "в диалог загружен номер радиокнопки");
                Check(dialog.cB1.Checked && !dialog.cB2.Checked, "в диалог загружены кнопки выбора");
                Check(dialog.tB1.Text == "10", "в диалог загружена строка редактирования");

                dialog.rB2.Checked = true;
                dialog.cB1.Checked = false;
                dialog.cB2.Checked = true;
                dialog.tB1.Text = "25";
                dialog.SaveSettings();

                Check(MainForm.rgi == 2, "из диалога сохранён номер радиокнопки");
                Check(!MainForm.b1 && MainForm.b2, "из диалога сохранены кнопки выбора");
                Check(MainForm.s1 == "25", "из диалога сохранена строка редактирования");
            }

            MainForm.InitializeSettings();

            // Дочерние окна: создаются многократно.
            main.CreateControl();
            var first = main.CreateChildWindow();
            var second = main.CreateChildWindow();
            Check(main.MdiChildren.Length == 2, "созданы два дочерних окна");
            Check(first.MdiParent == main && second.MdiParent == main, "дочерние окна принадлежат главной форме");
            Check(first.Text.Contains("№1"), "заголовок первого дочернего окна");
            Check(first.Notes.Text.Contains("дочернее окно"), "содержимое дочернего окна");

            // Единственное окно: пункт меню блокируется и разблокируется при закрытии.
            var single = main.OpenSingleWindow();
            Check(!main.FormA.Enabled, "пункт «Единственная форма» заблокирован");
            Check(single.MdiParent == main && single.f1 == main, "единственное окно связано с главной формой");
            Check(main.MdiChildren.Contains(single), "единственное окно добавлено в дочерние");

            var onClosed = typeof(SingleForm).GetMethod(
                "OnFormClosed", BindingFlags.Instance | BindingFlags.NonPublic)!;
            onClosed.Invoke(single, new object[] { new FormClosedEventArgs(CloseReason.UserClosing) });
            Check(main.FormA.Enabled, "после закрытия пункт меню снова доступен");

            single.Dispose();
            first.Dispose();
            second.Dispose();
        }

        using (var about = new AboutForm())
        {
            Check(about.Controls.OfType<Button>().Any(b => b.Text == "OK"), "в форме «О программе» есть кнопка OK");
        }

        Console.WriteLine($"ПЗ28 (MDI): самопроверка пройдена, проверок — {_passed}.");
    }

    private static void Check(bool condition, string name)
    {
        if (!condition)
            throw new InvalidOperationException($"Самопроверка не пройдена: {name}.");
        _passed++;
        Console.WriteLine($"OK  {name}");
    }
}
