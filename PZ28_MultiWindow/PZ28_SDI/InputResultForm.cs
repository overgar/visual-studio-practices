using System.Drawing;

namespace PZ28.Sdi;

/// <summary>Форма для ввода исходных данных и вывода результата (Form3).</summary>
public sealed class InputResultForm : Form
{
    /// <summary>Ссылка на диалоговую форму режимов (Form2).</summary>
    public ModeForm? f22;

    public TextBox textBox1 = null!;
    public Button button2 = null!;
    private readonly ListBox _listBox = new();
    private readonly Label _result = new();
    private readonly Label _count = new();

    public InputResultForm()
    {
        Text = "Ввод исходных данных и вывод результата";
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = new Size(460, 360);
        MinimumSize = new Size(420, 320);

        var caption = new Label
        {
            Text = "Введите целые числа через пробел:",
            Location = new Point(16, 16),
            AutoSize = true
        };

        textBox1 = new TextBox
        {
            Location = new Point(16, 40),
            Width = 420,
            Text = "5 12 -3 8 20"
        };

        button2 = new Button
        {
            Text = "Вычислить",
            Location = new Point(16, 76),
            Size = new Size(110, 28)
        };
        button2.Click += (_, _) => Compute();

        _count = new Label
        {
            Text = "Отобрано чисел: 0",
            Location = new Point(140, 82),
            AutoSize = true
        };

        var listCaption = new Label
        {
            Text = "Числа, прошедшие отбор:",
            Location = new Point(16, 116),
            AutoSize = true
        };

        _listBox = new ListBox
        {
            Location = new Point(16, 138),
            Size = new Size(150, 150),
            IntegralHeight = false
        };

        var resultCaption = new Label
        {
            Text = "Результат:",
            Location = new Point(184, 116),
            AutoSize = true
        };

        _result = new Label
        {
            Text = "Нажмите «Вычислить».",
            Location = new Point(184, 138),
            Size = new Size(252, 100),
            BorderStyle = BorderStyle.FixedSingle
        };

        var close = new Button
        {
            Text = "Закрыть",
            Location = new Point(356, 310),
            Size = new Size(88, 28)
        };
        close.Click += (_, _) => Close();

        Controls.Add(caption);
        Controls.Add(textBox1);
        Controls.Add(button2);
        Controls.Add(_count);
        Controls.Add(listCaption);
        Controls.Add(_listBox);
        Controls.Add(resultCaption);
        Controls.Add(_result);
        Controls.Add(close);
    }

    internal string ResultText => _result.Text;

    internal IReadOnlyList<string> OutputItems =>
        _listBox.Items.Cast<object>().Select(item => item.ToString() ?? string.Empty).ToArray();

    /// <summary>Выполняет вычисление по текущим значениям элементов формы.</summary>
    internal void Compute()
    {
        int[] numbers;
        try
        {
            numbers = Calculator.Parse(textBox1.Text);
        }
        catch (FormatException)
        {
            _result.Text = "Ошибка: введите целые числа через пробел.";
            return;
        }

        if (numbers.Length == 0)
        {
            _result.Text = "Введите хотя бы одно число.";
            return;
        }

        var threshold = 0;
        if (f22 is not null && !int.TryParse(f22.textBox1.Text, out threshold))
        {
            _result.Text = "Ошибка: заданное значение в форме режимов должно быть целым числом.";
            return;
        }

        var greater = f22?.rB1.Checked ?? true;
        var filtered = Calculator.Filter(numbers, threshold, greater);

        _listBox.Items.Clear();
        foreach (var number in filtered)
            _listBox.Items.Add(number);
        _count.Text = $"Отобрано чисел: {filtered.Length}";

        var parts = new List<string>();
        if (f22?.cB1.Checked ?? true)
            parts.Add($"Сумма = {Calculator.Sum(filtered)}");
        if (f22?.cB2.Checked ?? false)
            parts.Add($"Произведение = {Calculator.Product(filtered)}");

        _result.Text = parts.Count == 0
            ? "Не выбрана ни одна операция (Сумма / Произведение)."
            : string.Join(";   ", parts);
    }
}
