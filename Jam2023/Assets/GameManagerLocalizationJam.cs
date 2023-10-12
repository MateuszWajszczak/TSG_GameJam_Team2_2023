using System;
using UnityEngine;
using UnityEngine.UI;
using Assets.SimpleLocalization.Scripts;

public class GameManagerLocalizationJam : MonoBehaviour
{
    public Text FormattedText;
    public string LanguageOverride = null;

    public void Awake()
    {
        LocalizationManager.Read();

        switch (Application.systemLanguage)
        {
            case SystemLanguage.English:
                LocalizationManager.Language = "English";
                break;
            default:
                LocalizationManager.Language = "English";
                break;
        }

        //List of supported Languages: English, German
        if (LanguageOverride == "English")
        {
            LocalizationManager.Language = LanguageOverride;
        }
    }

    public void SetLocalization(string localization)
    {
        LocalizationManager.Language = localization;
    }
}
