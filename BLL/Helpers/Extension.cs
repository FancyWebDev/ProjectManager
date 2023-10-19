using System.Globalization;

namespace BLL.Helpers;

public static class Extension 
{
    private const string DateFormat = "yyyy-MM-dd";

    public static DateOnly ConvertToDateOnly(this string date) =>
        DateOnly.ParseExact(date, DateFormat, CultureInfo.InvariantCulture);

    public static string ConvertToString(this DateOnly date) =>
        date.ToString(DateFormat);
}