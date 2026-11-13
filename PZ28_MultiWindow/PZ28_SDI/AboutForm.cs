using System.Drawing;

namespace PZ28.Sdi;

/// <summary>Форма «О программе» (AboutBox1).</summary>
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
        ClientSize = new Size(380, 210);

        var title = new Label
        {
            Text = "PZ28_SDI — многооконное SDI-приложение",
            Font = new Font("Segoe UI", 11F, FontStyle.Bold),
            Location = new Point(16, 18),
            AutoSize = true
        };

        var body = new Label
        {
            Text = "Практическое занятие ПЗ28.\n" +
                   "Язык C#, Windows Forms, Visual Studio.\n\n" +
                   "Состав приложения:\n" +
                   "  • главная форма с меню;\n" +
                   "  • форма определения режимов работы;\n" +
                   "  • форма ввода исходных данных и вывода результата;\n" +
                   "  • форма «О программе».",
            Location = new Point(16, 52),
            Size = new Size(348, 112)
        };

        var ok = new Button
        {
            Text = "OK",
            DialogResult = DialogResult.OK,
            Location = new Point(276, 170),
            Size = new Size(88, 28)
        };
        ok.Click += (_, _) => Close();

        Controls.Add(title);
        Controls.Add(body);
        Controls.Add(ok);
        AcceptButton = ok;
    }
}
