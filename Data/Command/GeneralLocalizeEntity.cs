using System.Globalization;

namespace Data.Command
{
    public class GeneralLocalizeEntity
    {

        public string GeneralLocalize(string textAr, string textEn)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            if (cultureInfo.TwoLetterISOLanguageName.ToLower().Equals("ar"))
            {
                return textAr;
            }
            else
            {
                return textEn;
            }
        }
    }
}
