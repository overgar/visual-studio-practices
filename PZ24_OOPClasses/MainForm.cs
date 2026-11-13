using System.Drawing;

namespace PZ24;

public sealed class MainForm : Form
{
    private readonly Color _originalColor;
    private readonly Size _originalClientSize;
    private readonly ComboBox _cmbColor = new();
    private readonly ComboBox _cmbSize = new();
    private readonly Button _btnToggle = new();
    private readonly Button _btnYellow = new();
    private readonly Label _lblStatus = new();
    private readonly Panel _oopPanel = new();
    private readonly TextBox _txtOutput = new();
    private readonly Button _btnExercise = new();
    private readonly Button _btnOop = new();
    private bool _settingsEnabled = true;

    public MainForm()
    {
        Text = "ПЗ24 — классы ООП, свойства, методы, события";
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = new Size(860, 620);
        MinimumSize = new Size(420, 260);
        _originalColor = BackColor;
        _originalClientSize = ClientSize;

        BuildNavigation();
        BuildExercise();
        BuildOopPanel();
        UpdateStatus("Изменение свойств разрешено. Выберите цвет или размер формы.");
    }

    private void BuildNavigation()
    {
        _btnExercise.Text = "Упражнение";
        _btnExercise.Location = new Point(12, 10);
        _btnExercise.Size = new Size(120, 32);
        _btnExercise.Enabled = false;
        _btnExercise.Click += (_, _) => ShowOop(false);
        Controls.Add(_btnExercise);

        _btnOop.Text = "Задания ООП 1–3";
        _btnOop.Location = new Point(140, 10);
        _btnOop.Size = new Size(150, 32);
        _btnOop.Click += (_, _) => ShowOop(true);
        Controls.Add(_btnOop);
    }

    private void BuildExercise()
    {
        var lblColor = new Label
        {
            Text = "Выберите цвет закраски формы",
            Location = new Point(30, 83),
            AutoSize = true
        };
        Controls.Add(lblColor);

        _cmbColor.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbColor.Location = new Point(300, 79);
        _cmbColor.Size = new Size(210, 23);
        _cmbColor.Items.AddRange(new object[] { "Красный", "Зелёный", "Синий", "Жёлтый", "Исходный" });
        _cmbColor.SelectedIndex = 4;
        _cmbColor.SelectedIndexChanged += (_, _) => ApplySelectedColor();
        Controls.Add(_cmbColor);

        var lblSize = new Label
        {
            Text = "Размер формы",
            Location = new Point(30, 128),
            AutoSize = true
        };
        Controls.Add(lblSize);

        _cmbSize.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbSize.Location = new Point(300, 124);
        _cmbSize.Size = new Size(210, 23);
        _cmbSize.Items.AddRange(new object[] { "500×150", "550×200", "600×250", "650×300", "Исходный" });
        _cmbSize.SelectedIndex = 4;
        _cmbSize.SelectedIndexChanged += (_, _) => ApplySelectedSize();
        Controls.Add(_cmbSize);

        _btnToggle.Text = "Запретить изменение свойств";
        _btnToggle.Location = new Point(30, 180);
        _btnToggle.Size = new Size(260, 40);
        _btnToggle.Click += (_, _) => ToggleSettings();
        Controls.Add(_btnToggle);

        _btnYellow.Text = "Перекрасить в жёлтый";
        _btnYellow.Location = new Point(310, 180);
        _btnYellow.Size = new Size(200, 40);
        _btnYellow.Click += (_, _) =>
        {
            BackColor = Color.Yellow;
            _cmbColor.SelectedIndex = 3;
            UpdateStatus("Форма перекрашена в жёлтый цвет (кнопка работает без ограничений).");
        };
        Controls.Add(_btnYellow);

        _lblStatus.Location = new Point(30, 245);
        _lblStatus.Size = new Size(780, 70);
        _lblStatus.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        _lblStatus.BorderStyle = BorderStyle.FixedSingle;
        _lblStatus.Padding = new Padding(8);
        Controls.Add(_lblStatus);

        var lblHint = new Label
        {
            Text = "Кнопка «Перекрасить в жёлтый» работает всегда. Остальные настройки " +
                   "блокируются и разблокируются первой кнопкой.",
            Location = new Point(30, 335),
            Size = new Size(780, 40),
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
            ForeColor = Color.DimGray
        };
        Controls.Add(lblHint);
    }

    private void BuildOopPanel()
    {
        _oopPanel.Location = new Point(0, 52);
        _oopPanel.Size = new Size(ClientSize.Width, ClientSize.Height - 52);
        _oopPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        _oopPanel.BackColor = SystemColors.Control;
        _oopPanel.Visible = false;
        Controls.Add(_oopPanel);

        var btnTask1 = new Button { Text = "Задание 1: фигуры", Location = new Point(20, 14), Size = new Size(180, 36) };
        var btnTask2 = new Button { Text = "Задание 2: покупатели", Location = new Point(210, 14), Size = new Size(180, 36) };
        var btnTask3 = new Button { Text = "Задание 3: абоненты", Location = new Point(400, 14), Size = new Size(180, 36) };
        var btnClear = new Button { Text = "Очистить", Location = new Point(590, 14), Size = new Size(100, 36) };

        btnTask1.Click += (_, _) => _txtOutput.Text = DemoData.FormatFigures(DemoData.CreateFigures());
        btnTask2.Click += (_, _) => _txtOutput.Text = DemoData.FormatBuyers(DemoData.CreateBuyers());
        btnTask3.Click += (_, _) => _txtOutput.Text = DemoData.FormatSubscribers(DemoData.CreateSubscribers());
        btnClear.Click += (_, _) => _txtOutput.Clear();

        _txtOutput.Location = new Point(20, 62);
        _txtOutput.Size = new Size(800, 470);
        _txtOutput.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        _txtOutput.Multiline = true;
        _txtOutput.ReadOnly = true;
        _txtOutput.ScrollBars = ScrollBars.Both;
        _txtOutput.WordWrap = false;
        _txtOutput.Font = new Font("Consolas", 9F);
        _txtOutput.BackColor = Color.White;

        _oopPanel.Controls.Add(btnTask1);
        _oopPanel.Controls.Add(btnTask2);
        _oopPanel.Controls.Add(btnTask3);
        _oopPanel.Controls.Add(btnClear);
        _oopPanel.Controls.Add(_txtOutput);
    }

    private void ShowOop(bool show)
    {
        _oopPanel.Visible = show;
        _btnExercise.Enabled = show;
        _btnOop.Enabled = !show;
        if (show)
            _oopPanel.BringToFront();
    }

    private void ToggleSettings()
    {
        _settingsEnabled = !_settingsEnabled;
        _cmbColor.Enabled = _settingsEnabled;
        _cmbSize.Enabled = _settingsEnabled;
        _btnToggle.Text = _settingsEnabled
            ? "Запретить изменение свойств"
            : "Разрешить изменение свойств";
        UpdateStatus(_settingsEnabled
            ? "Изменение свойств формы разрешено."
            : "Изменение свойств формы запрещено. Доступна только жёлтая перекраска.");
    }

    private void ApplySelectedColor()
    {
        if (!_settingsEnabled)
        {
            UpdateStatus("Изменение цвета запрещено. Сначала разрешите изменение свойств.");
            return;
        }

        BackColor = _cmbColor.SelectedIndex switch
        {
            0 => Color.Red,
            1 => Color.Green,
            2 => Color.Blue,
            3 => Color.Yellow,
            _ => _originalColor
        };
        UpdateStatus($"Выбран цвет: {_cmbColor.SelectedItem}.");
    }

    private void ApplySelectedSize()
    {
        if (!_settingsEnabled)
        {
            UpdateStatus("Изменение размера запрещено. Сначала разрешите изменение свойств.");
            return;
        }

        ClientSize = _cmbSize.SelectedIndex switch
        {
            0 => new Size(500, 150),
            1 => new Size(550, 200),
            2 => new Size(600, 250),
            3 => new Size(650, 300),
            _ => _originalClientSize
        };
        UpdateStatus($"Выбран размер: {_cmbSize.SelectedItem}.");
    }

    private void UpdateStatus(string text) => _lblStatus.Text = text;
}
