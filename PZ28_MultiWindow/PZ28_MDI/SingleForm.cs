using System.Drawing;

namespace PZ28.Mdi;

/// <summary>Окно, которое создаётся из главного в единственном экземпляре (Form4).</summary>
public sealed class SingleForm : Form
{
    /// <summary>Ссылка на главное окно.</summary>
    public MainForm? f1;

    public SingleForm()
    {
        Text = "Единственная форма";
        ClientSize = new Size(440, 230);
        StartPosition = FormStartPosition.CenterParent;

        var label = new Label
        {
            Text = "Это окно может быть открыто только в одном экземпляре.\n\n" +
                   "Пока оно открыто, пункт меню «Окно → Единственная форма» заблокирован.\n" +
                   "После закрытия окна пункт меню снова становится доступным.",
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter,
            Padding = new Padding(16)
        };

        Controls.Add(label);
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        base.OnFormClosed(e);
        f1?.OnSingleFormClosed();
    }
}
