using System.Drawing;

namespace PZ28.Mdi;

/// <summary>Главная форма MDI-приложения (Form1) — содержит только меню.</summary>
public sealed class MainForm : Form
{
    // Значения диалоговой формы хранятся в главной форме, пока сама форма не существует.
    public static int rgi;
    public static bool b1;
    public static bool b2;
    public static string s1 = string.Empty;

    private int _childNumber;

    /// <summary>Пункт меню «Окно → Единственная форма».</summary>
    public ToolStripMenuItem FormA { get; }

    public MainForm()
    {
        Text = "ПЗ28 — MDI-приложение";
        IsMdiContainer = true;
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = new Size(900, 620);

        FormA = new ToolStripMenuItem("Единственная форма");
        FormA.Click += (_, _) => OpenSingleWindow();

        BuildMenu();
        InitializeSettings();
    }

    /// <summary>Начальные значения настроек (соответствует обработчику события Load).</summary>
    public static void InitializeSettings()
    {
        rgi = 1;
        b1 = true;
        b2 = false;
        s1 = "10";
    }

    private void BuildMenu()
    {
        var file = new ToolStripMenuItem("Файл");
        file.DropDownItems.Add(CreateItem("Выход", (_, _) => Close()));

        var window = new ToolStripMenuItem("Окно");
        window.DropDownItems.Add(CreateItem("Создать", (_, _) => CreateChildWindow()));
        window.DropDownItems.Add(new ToolStripSeparator());
        window.DropDownItems.Add(CreateItem("Каскад", (_, _) => LayoutMdi(MdiLayout.Cascade)));
        window.DropDownItems.Add(CreateItem("Мозаика горизонтально", (_, _) => LayoutMdi(MdiLayout.TileHorizontal)));
        window.DropDownItems.Add(CreateItem("Мозаика вертикально", (_, _) => LayoutMdi(MdiLayout.TileVertical)));
        window.DropDownItems.Add(new ToolStripSeparator());
        window.DropDownItems.Add(CreateItem("Закрыть текущее", (_, _) => CloseCurrentChild()));
        window.DropDownItems.Add(CreateItem("Закрыть все", (_, _) => CloseAllChildren()));
        window.DropDownItems.Add(new ToolStripSeparator());
        window.DropDownItems.Add(FormA);

        var dialog = new ToolStripMenuItem("Диалог");
        dialog.DropDownItems.Add(CreateItem("Открыть", (_, _) => ShowSettingsDialog()));

        var help = new ToolStripMenuItem("Помощь");
        help.DropDownItems.Add(CreateItem("О программе", (_, _) => ShowAbout()));

        var menu = new MenuStrip();
        menu.Items.Add(file);
        menu.Items.Add(window);
        menu.Items.Add(dialog);
        menu.Items.Add(help);
        MainMenuStrip = menu;
        Controls.Add(menu);
    }

    private static ToolStripMenuItem CreateItem(string text, EventHandler handler)
    {
        var item = new ToolStripMenuItem(text);
        item.Click += handler;
        return item;
    }

    /// <summary>Создаёт очередное дочернее окно (окон может быть много).</summary>
    internal ChildForm CreateChildWindow()
    {
        var child = new ChildForm(++_childNumber) { MdiParent = this };
        child.Show();
        return child;
    }

    /// <summary>Открывает единственное окно и блокирует пункт меню.</summary>
    internal SingleForm OpenSingleWindow()
    {
        if (!FormA.Enabled)
            throw new InvalidOperationException("Единственное окно уже открыто.");

        var form = new SingleForm { f1 = this, MdiParent = this };
        FormA.Enabled = false;
        form.Show();
        return form;
    }

    /// <summary>Разблокирует пункт меню после закрытия единственного окна.</summary>
    internal void OnSingleFormClosed() => FormA.Enabled = true;

    internal void CloseCurrentChild() => ActiveMdiChild?.Close();

    internal void CloseAllChildren()
    {
        foreach (var child in MdiChildren.ToArray())
            child.Close();
    }

    /// <summary>Открывает диалог с текущими значениями настроек.</summary>
    internal void ShowSettingsDialog()
    {
        using var dialog = new SettingsDialog();
        dialog.LoadSettings();
        if (dialog.ShowDialog(this) == DialogResult.OK)
            dialog.SaveSettings();
    }

    internal void ShowAbout()
    {
        using var about = new AboutForm();
        about.ShowDialog(this);
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        InitializeSettings();
    }
}
