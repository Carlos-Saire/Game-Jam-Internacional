using UnityEngine;
using UnityEngine.Localization.Settings;
using System.Collections;

public class LanguageSelector : MonoBehaviour
{
    private bool active = false;

    public void ChangeLanguage(string languageCode)
    {
        if (active) return; 
        StartCoroutine(SetLanguage(languageCode));
    }

    private IEnumerator SetLanguage(string languageCode)
    {
        active = true;

        yield return LocalizationSettings.InitializationOperation;

        var selectedLocale = LocalizationSettings.AvailableLocales.GetLocale(languageCode);

        if (selectedLocale != null)
        {
            LocalizationSettings.SelectedLocale = selectedLocale;
            Debug.Log("Idioma cambiado a: " + selectedLocale.LocaleName);
        }
        else
        {
            Debug.LogWarning("No se encontró el idioma: " + languageCode);
        }

        active = false;
    }
}