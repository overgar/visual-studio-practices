using System.Text;

namespace PZ23;

internal sealed record InsuranceRecord(
    string FullName,
    string City,
    string Street,
    string House,
    string Apartment,
    string Gender,
    int TermMonths,
    string InsuranceType)
{
    public string Address => $"г. {City}, ул. {Street}, д. {House}, кв. {Apartment}";

    public static bool TryValidate(InsuranceRecord record, out string error)
    {
        var missing = new List<string>();
        if (string.IsNullOrWhiteSpace(record.FullName)) missing.Add("фамилия и имя");
        if (string.IsNullOrWhiteSpace(record.City)) missing.Add("город");
        if (string.IsNullOrWhiteSpace(record.Street)) missing.Add("улица");
        if (string.IsNullOrWhiteSpace(record.House)) missing.Add("дом");
        if (string.IsNullOrWhiteSpace(record.Apartment)) missing.Add("квартира");
        if (string.IsNullOrWhiteSpace(record.Gender)) missing.Add("пол");
        if (string.IsNullOrWhiteSpace(record.InsuranceType)) missing.Add("вид страхования");
        if (record.TermMonths <= 0) missing.Add("срок страхования");

        error = missing.Count == 0 ? string.Empty : "Не заполнены: " + string.Join(", ", missing) + ".";
        return missing.Count == 0;
    }

    public string ToFileBlock()
    {
        var builder = new StringBuilder();
        builder.AppendLine("Страхователь: " + FullName.Trim());
        builder.AppendLine("Адрес: " + Address);
        builder.AppendLine("Пол: " + Gender);
        builder.AppendLine($"Срок страхования: {TermMonths} мес.");
        builder.AppendLine("Вид страхования: " + InsuranceType);
        builder.AppendLine(new string('-', 50));
        return builder.ToString();
    }
}
