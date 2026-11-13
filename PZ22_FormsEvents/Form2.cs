using System.Drawing;

namespace PZ22;

public partial class Form2 : Form
{
    private Bitmap? _photo;

    public Form2()
    {
        InitializeComponent();
    }

    private Form1? OwnerForm => Owner as Form1;

    private void Form2_Shown(object? sender, EventArgs e)
    {
        RefreshFromOwner();
    }

    private void btnRefresh_Click(object? sender, EventArgs e)
    {
        RefreshFromOwner();
    }

    private void RefreshFromOwner()
    {
        if (OwnerForm is not { } owner)
        {
            lblFio.Text = "Нет ссылки на первую форму.";
            return;
        }

        lblFio.Text = $"{owner.txtLastName.Text.Trim()} {owner.txtFirstName.Text.Trim()}".Trim();
        lblBirth.Text = "Дата рождения: " + owner.dtpBirthDate.Value.ToString("dd.MM.yyyy");
        lblGender.Text = "Пол: " + (owner.cmbGender.SelectedItem?.ToString() ?? "не указан");
        lblAddress.Text = "Адрес: " + owner.txtAddress.Text.Trim();
        lblSubscribed.Text = "Согласие на обработку: " + (owner.chkSubscribed.Checked ? "да" : "нет");
        txtNote.Text = owner.txtNote.Text;
        lblNoteCaption.Text = "Примечание (можно изменить и записать обратно):";

        _photo?.Dispose();
        _photo = null;
        picPhoto.Image = null;
        if (File.Exists(owner.PhotoPath))
        {
            try
            {
                _photo = new Bitmap(owner.PhotoPath);
                picPhoto.Image = _photo;
            }
            catch
            {
                picPhoto.Image = null;
            }
        }
    }

    private void btnWriteBack_Click(object? sender, EventArgs e)
    {
        if (OwnerForm is { } owner)
        {
            owner.txtNote.Text = txtNote.Text;
            MessageBox.Show(this, "Примечание записано в первую форму.", "Обмен данными",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private void btnClose_Click(object? sender, EventArgs e) => Close();

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _photo?.Dispose();
            components?.Dispose();
        }
        base.Dispose(disposing);
    }
}
