using System.Drawing;

namespace PZ25;

public sealed class MainForm : Form
{
    private readonly DateTimePicker _dtpFirst = new();
    private readonly DateTimePicker _dtpSecond = new();
    private readonly NumericUpDown _numDays = new();
    private readonly TextBox _txtOutput = new();
    private readonly Label _lblNow = new();
    private readonly System.Windows.Forms.Timer _timer = new();

    public MainForm()
    {
        Text = "ПЗ25 — даты и время";
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = new Size(780, 650);

        BuildControls();
        UpdateNow();
        _timer.Interval = 1000;
        _timer.Tick += (_, _) => UpdateNow();
        _timer.Start();
    }

    private void BuildControls()
    {
        _lblNow.Location = new Point(18, 14);
        _lblNow.Size = new Size(744, 24);
        _lblNow.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        _lblNow.Text = DateTime.Now.ToString("dddd, d MMMM yyyy, HH:mm:ss", DateTimeLogic.Ru);
        Controls.Add(_lblNow);

        var lblFirst = new Label { Text = "Дата 1:", Location = new Point(22, 58), AutoSize = true };
        Controls.Add(lblFirst);
        _dtpFirst.Format = DateTimePickerFormat.Custom;
        _dtpFirst.CustomFormat = "dd.MM.yyyy HH:mm:ss";
        _dtpFirst.Location = new Point(110, 54);
        _dtpFirst.Size = new Size(190, 23);
        _dtpFirst.Value = new DateTime(2015, 7, 20, 18, 30, 25);
        Controls.Add(_dtpFirst);

        var lblSecond = new Label { Text = "Дата 2:", Location = new Point(330, 58), AutoSize = true };
        Controls.Add(lblSecond);
        _dtpSecond.Format = DateTimePickerFormat.Custom;
        _dtpSecond.CustomFormat = "dd.MM.yyyy HH:mm:ss";
        _dtpSecond.Location = new Point(418, 54);
        _dtpSecond.Size = new Size(190, 23);
        _dtpSecond.Value = new DateTime(2015, 7, 20, 15, 30, 25);
        Controls.Add(_dtpSecond);

        var lblDays = new Label { Text = "Дней:", Location = new Point(22, 96), AutoSize = true };
        Controls.Add(lblDays);
        _numDays.Location = new Point(110, 92);
        _numDays.Minimum = -36500;
        _numDays.Maximum = 36500;
        _numDays.Value = 3;
        _numDays.Size = new Size(90, 23);
        Controls.Add(_numDays);

        AddButton("Показать форматы", 22, 132, 170, (_, _) =>
            _txtOutput.Text = DateTimeLogic.FormatAll(_dtpFirst.Value));
        AddButton("Разность дат", 202, 132, 150, (_, _) =>
            _txtOutput.Text = DateTimeLogic.DescribeDifference(_dtpFirst.Value, _dtpSecond.Value));
        AddButton("Добавить дни", 362, 132, 150, (_, _) =>
        {
            var result = _dtpFirst.Value.AddDays((double)_numDays.Value);
            _txtOutput.Text = $"Исходная дата: {_dtpFirst.Value:dd.MM.yyyy HH:mm:ss}\r\n" +
                              $"После AddDays({_numDays.Value}): {result:dd.MM.yyyy HH:mm:ss}\r\n" +
                              $"День недели: {result.ToString("dddd", DateTimeLogic.Ru)}";
        });
        AddButton("Возраст", 522, 132, 110, (_, _) =>
        {
            var (years, months, days) = DateTimeLogic.Age(_dtpFirst.Value.Date, DateTime.Today);
            _txtOutput.Text = $"Дата рождения: {_dtpFirst.Value:dd.MM.yyyy}\r\n" +
                              $"На {DateTime.Today:dd.MM.yyyy}: {years} г. {months} мес. {days} дн.\r\n" +
                              $"Полных лет: {years}";
        });
        AddButton("Сейчас", 642, 132, 110, (_, _) =>
        {
            _dtpFirst.Value = DateTime.Now;
            _dtpSecond.Value = DateTime.Now.AddHours(-3);
        });

        var lblHint = new Label
        {
            Text = "Проверка AddHours, Subtract, AddDays и форматирования. " +
                   "Кнопка «Возраст» считает от даты 1 до текущей даты.",
            Location = new Point(22, 174),
            Size = new Size(740, 20),
            ForeColor = Color.DimGray
        };
        Controls.Add(lblHint);

        _txtOutput.Location = new Point(22, 204);
        _txtOutput.Size = new Size(736, 420);
        _txtOutput.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        _txtOutput.Multiline = true;
        _txtOutput.ReadOnly = true;
        _txtOutput.ScrollBars = ScrollBars.Both;
        _txtOutput.WordWrap = false;
        _txtOutput.Font = new Font("Consolas", 10F);
        _txtOutput.BackColor = Color.White;
        Controls.Add(_txtOutput);
    }

    private void AddButton(string text, int x, int y, int width, EventHandler handler)
    {
        var button = new Button
        {
            Text = text,
            Location = new Point(x, y),
            Size = new Size(width, 32)
        };
        button.Click += handler;
        Controls.Add(button);
    }

    private void UpdateNow()
    {
        _lblNow.Text = DateTime.Now.ToString("dddd, d MMMM yyyy, HH:mm:ss", DateTimeLogic.Ru);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _timer.Stop();
            _timer.Dispose();
        }
        base.Dispose(disposing);
    }
}
