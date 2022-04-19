using UnityEngine.Localization.Settings;
using UnityEngine.Localization;
using UnityEngine.ResourceManagement.AsyncOperations;
using TMPro;
using UnityEngine.UI;

namespace XS_Utils
{
    public static class XS_Localization
    {
        public static void SelectLanguage(this int localeIndex)
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeIndex];
        }
        public static int Languages => LocalizationSettings.AvailableLocales.Locales.Count;
    
        public static void WriteOn(this LocalizedString localizedString, TMP_Text text)
        {
            AsyncOperationHandle<string> op = localizedString.GetLocalizedStringAsync();
            if (op.IsDone)
            {
                text.text = op.Result;
            }
            else
            {
                op.Completed += (o) => text.text = op.Result;
            }
        }
        public static void WriteOn(this LocalizedString localizedString, Text text)
        {
            AsyncOperationHandle<string> op = localizedString.GetLocalizedStringAsync();
            if (op.IsDone)
            {
                text.text = op.Result;
            }
            else
            {
                op.Completed += (o) => text.text = op.Result;
            }
        }
    }
}
