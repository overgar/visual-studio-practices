using System.Drawing;

namespace PZ28.Mdi;

/// <summary>Дочернее окно, которое можно создавать многократно (рис. 4.7).</summary>
public sealed class ChildForm : Form
{
    private readonly TextBox _notes = new();

    public ChildForm() : this(0)
    {
    }

    public ChildForm(int number)
    {
        Text = $"Дочернее окно №{number}";
        ClientSize = new Size(520, 340);

        _notes.Multiline = true;
        _notes.Dock = DockStyle.Fill;
        _notes.ScrollBars = ScrollBars.Both;
        _notes.AcceptsReturn = true;
        _notes.Font = new Font("Segoe UI", 10F);
        _notes.Text = $"Это дочернее окно №{number}.{Environment.NewLine}" +
                      "Окон такого типа можно создать сколько угодно.";

        var menu = new MenuStrip();
        var action = new ToolStripMenuItem("Действие");
        action.DropDownItems.Add("Очистить", null, (_, _) => _notes.Clear());
        action.DropDownItems.Add("Закрыть", null, (_, _) => Close());
        menu.Items.Add(action);
        MainMenuStrip = menu;

        Controls.Add(_notes);
        Controls.Add(menu);
    }

    internal TextBox Notes => _notes;

    internal int Number =>
        int.TryParse(Text.Replace("Дочернее окно №", string.Empty), out var number) ? number : 0;
}
