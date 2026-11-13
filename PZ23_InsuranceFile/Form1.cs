namespace PZ23;

public partial class Form1 : Form
{
    private readonly string _filePath = InsuranceFile.DefaultPath;

    public Form1()
    {
        InitializeComponent();
        cmbGender.Items.Clear();
        cmbGender.Items.AddRange(new object[] { "Мужской", "Женский" });
        cmbGender.SelectedIndex = -1;

        cmbInsuranceType.Items.Clear();
        cmbInsuranceType.Items.AddRange(new object[]
        {
            "Автострахование", "Страхование имущества", "Страхование здоровья",
            "Страхование жизни", "Страхование путешествий"
        });
        cmbInsuranceType.SelectedIndex = -1;
        lblFilePath.Text = "Файл: " + _filePath;
    }

    private void btnOk_Click(object? sender, EventArgs e)
    {
        var record = new InsuranceRecord(
            txtFullName.Text.Trim(),
            txtCity.Text.Trim(),
            txtStreet.Text.Trim(),
            txtHouse.Text.Trim(),
            txtApartment.Text.Trim(),
            cmbGender.SelectedItem?.ToString() ?? string.Empty,
            (int)numTerm.Value,
            cmbInsuranceType.SelectedItem?.ToString() ?? string.Empty);

        if (!InsuranceRecord.TryValidate(record, out var error))
        {
            lblError.Text = "Не все данные введены!";
            MessageBox.Show(this, error, "Проверка данных", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            InsuranceFile.Append(_filePath, record);
            lblError.Text = string.Empty;
            MessageBox.Show(this, "Данные записаны в файл:\n" + _filePath, "Готово",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearFields();
        }
        catch (Exception ex)
        {
            lblError.Text = "Не удалось записать файл.";
            MessageBox.Show(this, ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnCancel_Click(object? sender, EventArgs e) => Close();

    private void ClearFields()
    {
        txtFullName.Clear();
        txtCity.Clear();
        txtStreet.Clear();
        txtHouse.Clear();
        txtApartment.Clear();
        cmbGender.SelectedIndex = -1;
        cmbInsuranceType.SelectedIndex = -1;
        numTerm.Value = 12;
        lblError.Text = string.Empty;
        txtFullName.Focus();
    }
}
