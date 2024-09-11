using System.Globalization;

namespace Data.Command;
public class GeneralLocalizeEntity
{
    public static string GeneralLocalize(string textAr, string textEn)
    {
        var cultureInfo = Thread.CurrentThread.CurrentCulture;
        return cultureInfo.TwoLetterISOLanguageName.ToLower().Equals("ar") ? textAr : textEn;
    }
}
