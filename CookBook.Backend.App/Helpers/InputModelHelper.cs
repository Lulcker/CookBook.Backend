using System.Text.RegularExpressions;

namespace CookBook.Backend.App.Helpers;

public class InputModelHelper
{
    /// <summary>
    /// Метод для применения метода Trim() ко всем свойствам в модели
    /// </summary>
    public static void TrimStringProperties<T>(T model, bool isUppercaseFirstLetter = true)
    {
        if (model == null) return;
        
        foreach (var property in model.GetType().GetProperties())
        {
            if (property.PropertyType != typeof(string) || !property.CanRead || !property.CanWrite) continue;
            
            var currentValue = (string?)property.GetValue(model);

            if (currentValue == null) continue;
            
            currentValue = currentValue.Trim();
            currentValue = Regex.Replace(currentValue, @"\s+", " ");
                
            if (isUppercaseFirstLetter)
                currentValue = string.Concat(currentValue[0].ToString().ToUpperInvariant() + currentValue[1..]);
                
            property.SetValue(model, currentValue);
        }
    }
}