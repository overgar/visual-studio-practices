using System.Drawing;

namespace PZ22;

public partial class Form1 : Form
{
    private Bitmap? _preview;

    public Form1()
    {
        InitializeComponent();
        cmbGender.Items.Clear();
        cmbGender.Items.AddRange(new object[] { "Мужской", "Женский" });
        cmbGender.SelectedIndex = -1;
    }

    public string PhotoPath { get; private set; } = string.Empty;

    internal PatientData CurrentData => new(
        txtLastName.Text.Trim(),
        txtFirstName.Text.Trim(),
        txtAddress.Text.Trim(),
        dtpBirthDate.Value.Date,
        cmbGender.SelectedItem?.ToString() ?? string.Empty,
        chkSubscribed.Checked,
        txtNote.Text.Trim(),
        PhotoPath);

    private void btnAddPhoto_Click(object? sender, EventArgs e)
    {
        using var dialog = new OpenFileDialog
        {
            Title = "Выберите фотографию",
            Filter = "Изображения (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png",
            CheckFileExists = true,
            Multiselect = false
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
            return;

        try
        {
            var bitmap = new Bitmap(dialog.FileName);
            _preview?.Dispose();
            _preview = bitmap;
            picPhoto.Image = _preview;
            PhotoPath = dialog.FileName;
            lblPhoto.Text = Path.GetFileName(dialog.FileName);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, "Не удалось открыть изображение: " + ex.Message,
                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnShow_Click(object? sender, EventArgs e)
    {
        var data = CurrentData;
        if (!PatientData.TryValidate(data, out var error))
        {
            MessageBox.Show(this, error + "\n\nВторая форма не будет заполнена.",
                "Не все данные введены", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        using var form2 = new Form2();
        form2.ShowDialog(this);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _preview?.Dispose();
            components?.Dispose();
        }
        base.Dispose(disposing);
    }
}
