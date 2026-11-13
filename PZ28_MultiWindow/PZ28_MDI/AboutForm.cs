using System.Drawing;

namespace PZ28.Mdi;

/// <summary>Форма «О программе».</summary>
public sealed class AboutForm : Form
{
    public AboutForm()
    {
        Text = "О программе";
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterParent;
        MaximizeBox = false;
        MinimizeBox = false;
        ShowInTaskbar = false;
        ClientSize = new Size(400, 220);

        var title = new Label
        {
            Text = "PZ28_MDI — многооконное MDI-приложение",
            Font = new Font("Segoe UI", 11F, FontStyle.Bold),
            Location = new Point(16, 18),
            AutoSize = true
        };

        var body = new Label
        {
            Text = "Практическое занятие ПЗ28.\n" +
                   "Язык C#, Windows Forms, Visual Studio.\n\n" +
                   "Главное окно является MDI-контейнером и содержит меню.\n" +
                   "Подчинённые окна: диалог настроек, многократно создаваемые\n" +
                   "дочерние окна и окно в единственном экземпляре.",
            Location = new Point(16, 52),
            Size = new Size(368, 110)
        };

        var ok = new Button
        {
            Text = "OK",
            DialogResult = DialogResult.OK,
            Location = new Point(296, 180),
            Size = new Size(88, 28)
        };
        ok.Click += (_, _) => Close();

        Controls.Add(title);
        Controls.Add(body);
        Controls.Add(ok);
        AcceptButton = ok;
    }
}
