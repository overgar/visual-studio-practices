using System.Drawing;

namespace PZ28.Mdi;

/// <summary>Диалоговое окно настроек: радиокнопки, кнопки выбора и строка редактирования.</summary>
public sealed class SettingsDialog : Form
{
    public RadioButton rB1 = null!;
    public RadioButton rB2 = null!;
    public CheckBox cB1 = null!;
    public CheckBox cB2 = null!;
    public TextBox tB1 = null!;

    public SettingsDialog()
    {
        Text = "Диалог";
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterParent;
        MaximizeBox = false;
        MinimizeBox = false;
        ShowInTaskbar = false;
        ClientSize = new Size(340, 240);

        var conditionGroup = new GroupBox
        {
            Text = "Условие отбора",
            Location = new Point(12, 12),
            Size = new Size(316, 80)
        };
        rB1 = new RadioButton
        {
            Text = "Числа больше заданного",
            Location = new Point(12, 22),
            AutoSize = true
        };
        rB2 = new RadioButton
        {
            Text = "Числа меньше заданного",
            Location = new Point(12, 48),
            AutoSize = true
        };
        conditionGroup.Controls.Add(rB1);
        conditionGroup.Controls.Add(rB2);

        var operationsGroup = new GroupBox
        {
            Text = "Операции",
            Location = new Point(12, 100),
            Size = new Size(316, 80)
        };
        cB1 = new CheckBox
        {
            Text = "Сумма",
            Location = new Point(12, 22),
            AutoSize = true
        };
        cB2 = new CheckBox
        {
            Text = "Произведение",
            Location = new Point(12, 48),
            AutoSize = true
        };
        operationsGroup.Controls.Add(cB1);
        operationsGroup.Controls.Add(cB2);

        var caption = new Label
        {
            Text = "Заданное значение:",
            Location = new Point(12, 192),
            AutoSize = true
        };
        tB1 = new TextBox
        {
            Location = new Point(146, 189),
            Width = 84,
            TextAlign = HorizontalAlignment.Right
        };

        var ok = new Button
        {
            Text = "OK",
            DialogResult = DialogResult.OK,
            Location = new Point(148, 204),
            Size = new Size(84, 26)
        };
        ok.Click += (_, _) =>
        {
            SaveSettings();
            DialogResult = DialogResult.OK;
            Close();
        };

        var cancel = new Button
        {
            Text = "CANCEL",
            DialogResult = DialogResult.Cancel,
            Location = new Point(240, 204),
            Size = new Size(84, 26)
        };
        cancel.Click += (_, _) =>
        {
            DialogResult = DialogResult.Cancel;
            Close();
        };

        Controls.Add(conditionGroup);
        Controls.Add(operationsGroup);
        Controls.Add(caption);
        Controls.Add(tB1);
        Controls.Add(ok);
        Controls.Add(cancel);

        AcceptButton = ok;
        CancelButton = cancel;
    }

    /// <summary>Переносит текущие значения из главной формы в элементы диалога.</summary>
    public void LoadSettings()
    {
        rB1.Checked = MainForm.rgi == 1;
        rB2.Checked = MainForm.rgi == 2;
        cB1.Checked = MainForm.b1;
        cB2.Checked = MainForm.b2;
        tB1.Text = MainForm.s1;
    }

    /// <summary>Сохраняет значения элементов диалога в главную форму.</summary>
    public void SaveSettings()
    {
        MainForm.rgi = rB2.Checked ? 2 : 1;
        MainForm.b1 = cB1.Checked;
        MainForm.b2 = cB2.Checked;
        MainForm.s1 = tB1.Text;
    }
}
