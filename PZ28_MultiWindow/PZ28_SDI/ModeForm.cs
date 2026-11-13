using System.Drawing;

namespace PZ28.Sdi;

/// <summary>Форма для определения режимов работы программы (Form2).</summary>
public sealed class ModeForm : Form
{
    // Поля объявлены public, чтобы к ним был доступ из других форм (аналог Modifiers = public).
    public RadioButton rB1 = null!;
    public RadioButton rB2 = null!;
    public CheckBox cB1 = null!;
    public CheckBox cB2 = null!;
    public TextBox textBox1 = null!;
    public Button button1 = null!;

    public ModeForm()
    {
        Text = "Режимы работы";
        ControlBox = false;
        StartPosition = FormStartPosition.Manual;
        Location = new Point(660, 140);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        ShowInTaskbar = false;
        ClientSize = new Size(320, 232);

        var conditionGroup = new GroupBox
        {
            Text = "Условие отбора",
            Location = new Point(12, 12),
            Size = new Size(296, 80)
        };
        rB1 = new RadioButton
        {
            Text = "Числа больше заданного",
            Location = new Point(12, 22),
            AutoSize = true,
            Checked = true
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
            Size = new Size(296, 80)
        };
        cB1 = new CheckBox
        {
            Text = "Сумма",
            Location = new Point(12, 22),
            AutoSize = true,
            Checked = true
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
        textBox1 = new TextBox
        {
            Text = "0",
            Location = new Point(146, 189),
            Width = 86,
            TextAlign = HorizontalAlignment.Right
        };
        button1 = new Button
        {
            Text = "Скрыть",
            Location = new Point(238, 187),
            Size = new Size(70, 26)
        };
        button1.Click += (_, _) => Hide();

        Controls.Add(conditionGroup);
        Controls.Add(operationsGroup);
        Controls.Add(caption);
        Controls.Add(textBox1);
        Controls.Add(button1);
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        // Форма не уничтожается: при попытке закрытия она только скрывается.
        if (e.CloseReason == CloseReason.UserClosing)
        {
            e.Cancel = true;
            Hide();
            return;
        }

        base.OnFormClosing(e);
    }
}
