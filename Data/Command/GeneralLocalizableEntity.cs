using System.Globalization;

namespace Data.Command
{
    public class GeneralLocalizableEntity
    {

        public string GeneralLocalizable(string textar, string texten)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            if (cultureInfo.TwoLetterISOLanguageName.ToLower().Equals("ar"))
            {
                return textar;
            }
            else
            {
                return texten;
            }
        }
    }
}
