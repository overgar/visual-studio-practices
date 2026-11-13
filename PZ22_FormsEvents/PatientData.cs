namespace PZ22;

/// <summary>Данные анкеты, которыми обмениваются две формы.</summary>
internal sealed record PatientData(
    string LastName,
    string FirstName,
    string Address,
    DateTime BirthDate,
    string Gender,
    bool Subscribed,
    string Note,
    string PhotoPath)
{
    public static bool TryValidate(PatientData data, out string error)
    {
        var missing = new List<string>();
        if (string.IsNullOrWhiteSpace(data.LastName)) missing.Add("фамилия");
        if (string.IsNullOrWhiteSpace(data.FirstName)) missing.Add("имя");
        if (string.IsNullOrWhiteSpace(data.Address)) missing.Add("адрес");
        if (string.IsNullOrWhiteSpace(data.Gender)) missing.Add("пол");
        if (string.IsNullOrWhiteSpace(data.Note)) missing.Add("примечание");
        if (string.IsNullOrWhiteSpace(data.PhotoPath)) missing.Add("фотография");
        else if (!File.Exists(data.PhotoPath)) missing.Add("существующий файл фотографии");

        error = missing.Count == 0
            ? string.Empty
            : "Заполните поля: " + string.Join(", ", missing) + ".";
        return missing.Count == 0;
    }
}
