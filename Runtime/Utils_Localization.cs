using UnityEngine.Localization.Settings;

namespace XS_Utils
{
    public static class XS_Localization
    {
        public static void SelectLanguage(this int localeIndex)
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeIndex];
        }
        public static int Languages => LocalizationSettings.AvailableLocales.Locales.Count;
    }
}
