using System.Globalization;
using System.Threading;
using System.Web;

namespace SchoolDiarySystem.Models.Validations
{
    public class ChangeLanguage
    {
        public static HttpCookie Language(string lngName)
        {
            if (!string.IsNullOrEmpty(lngName))
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(lngName);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(lngName);
            }

            HttpCookie cookie = new HttpCookie("Language")
            {
                Value = lngName
            };
            return cookie;
        }
    }
}