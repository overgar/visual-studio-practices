using System.Drawing;

namespace PZ22;

internal static class SelfTest
{
    private static int _passed;

    public static void Run()
    {
        _passed = 0;
        var tempDir = Path.Combine(Path.GetTempPath(), "pz22_selftest");
        Directory.CreateDirectory(tempDir);
        var photo = Path.Combine(tempDir, "photo.png");
        using (var bitmap = new Bitmap(10, 10))
            bitmap.Save(photo, System.Drawing.Imaging.ImageFormat.Png);

        var valid = new PatientData("Иванов", "Иван", "г. Москва, ул. Мира, д. 1", new DateTime(2000, 1, 1),
            "Мужской", false, "Примечание", photo);
        Check(PatientData.TryValidate(valid, out var error) && error.Length == 0, "корректная анкета");
        Check(!PatientData.TryValidate(valid with { LastName = " " }, out error) && error.Contains("фамилия"),
            "пустая фамилия");
        Check(!PatientData.TryValidate(valid with { Note = "" }, out error) && error.Contains("примечание"),
            "пустое примечание");
        Check(!PatientData.TryValidate(valid with { PhotoPath = "" }, out error) && error.Contains("фотография"),
            "нет фотографии");
        Check(!PatientData.TryValidate(valid with { PhotoPath = Path.Combine(tempDir, "none.png") }, out error),
            "файл фотографии не существует");
        Check(PatientData.TryValidate(valid with { Subscribed = true }, out _), "checkbox не обязателен");

        using (var form = new Form1())
        {
            var textBoxes = form.Controls.Find("txtLastName", true).Length + form.Controls.Find("txtFirstName", true).Length +
                            form.Controls.Find("txtAddress", true).Length + form.Controls.Find("txtNote", true).Length;
            var groupBoxes = 0;
            var buttons = 0;
            var datePickers = 0;
            var checkBoxes = 0;
            var comboBoxes = 0;
            var pictureBoxes = 0;
            void Walk(Control control)
            {
                foreach (Control child in control.Controls)
                {
                    if (child is GroupBox) groupBoxes++;
                    if (child is Button) buttons++;
                    if (child is DateTimePicker) datePickers++;
                    if (child is CheckBox) checkBoxes++;
                    if (child is ComboBox) comboBoxes++;
                    if (child is PictureBox) pictureBoxes++;
                    Walk(child);
                }
            }
            Walk(form);
            Check(textBoxes == 4, "на форме 4 TextBox");
            Check(groupBoxes == 3, "на форме 3 GroupBox");
            Check(buttons == 2, "на форме 2 Button");
            Check(datePickers == 1, "на форме DateTimePicker");
            Check(checkBoxes == 1, "на форме CheckBox");
            Check(comboBoxes == 1 && form.cmbGender.Items.Count == 2, "ComboBox с двумя вариантами пола");
            Check(pictureBoxes == 1, "на форме PictureBox");
        }

        try { File.Delete(photo); } catch { }
        try { Directory.Delete(tempDir, true); } catch { }
        Console.WriteLine($"ПЗ22: самопроверка пройдена, проверок — {_passed}.");
    }

    private static void Check(bool condition, string name)
    {
        if (!condition)
            throw new InvalidOperationException($"Самопроверка не пройдена: {name}.");
        _passed++;
        Console.WriteLine($"OK  {name}");
    }
}
