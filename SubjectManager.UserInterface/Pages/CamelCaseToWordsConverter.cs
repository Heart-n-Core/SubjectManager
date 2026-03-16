namespace SubjectManager.UserInterface.Pages;

using System.Globalization;
using System.Text.RegularExpressions;

public class CamelCaseToWordsConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null)
            return null;

        return Regex.Replace(value.ToString(), "([a-z])([A-Z])", "$1 $2");
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null)
            return null;

        string text = value.ToString()!.Replace(" ", "");
        return Enum.Parse(targetType, text);
    }
    
}