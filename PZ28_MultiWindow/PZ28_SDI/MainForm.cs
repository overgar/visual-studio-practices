using System.Drawing;

namespace PZ28.Sdi;

/// <summary>Главная форма SDI-приложения (Form1) — содержит только меню.</summary>
public sealed class MainForm : Form
{
    private readonly ModeForm _modeForm = new();

    public MainForm()
    {
        Text = "ПЗ28 — SDI-приложение";
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = new Size(720, 460);
        BuildMenu();
    }

    internal ModeForm ModeWindow => _modeForm;

    private void BuildMenu()
    {
        var file = new ToolStripMenuItem("&File");
        file.DropDownItems.Add(CreateItem("&Quit", (_, _) => Close()));

        var windows = new ToolStripMenuItem("&Windows");
        windows.DropDownItems.Add(CreateItem("&New", (_, _) => OpenInputForm()));
        windows.DropDownItems.Add(CreateItem("&Open Dialog", (_, _) => ShowModeForm()));

        var help = new ToolStripMenuItem("&Help");
        help.DropDownItems.Add(CreateItem("&About", (_, _) => ShowAbout()));

        var menu = new MenuStrip();
        menu.Items.Add(file);
        menu.Items.Add(windows);
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

    internal InputResultForm OpenInputForm()
    {
        var form = new InputResultForm { f22 = _modeForm };
        form.Show();
        return form;
    }

    internal void ShowModeForm() => _modeForm.Show();

    internal void ShowAbout()
    {
        using var about = new AboutForm();
        about.ShowDialog(this);
    }

    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        // В момент запуска приложения на экране появляются главная форма и форма режимов.
        _modeForm.Show();
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        if (!_modeForm.IsDisposed)
            _modeForm.Dispose();
        base.OnFormClosed(e);
    }
}
