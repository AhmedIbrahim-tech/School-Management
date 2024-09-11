using System.Globalization;

namespace Data.Command;

public class LocalizableEntity
{
    public string? NameAr { get; set; }
    public string? NameEn { get; set; }
    

    public string GetLocalized()
    {
        var culture = Thread.CurrentThread.CurrentCulture;
        return culture.TwoLetterISOLanguageName.ToLower().Equals("ar") ? NameAr : NameEn;
    }
}
