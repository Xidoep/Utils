using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using XS_Utils;

/// <summary>
/// Put this in a TextMeshPro to print bundle Version on enable
/// </summary>
public class Utils_VersionUI : MonoBehaviour
{
    const string VERSION_KEY = "version";

    TMP_Text text;
    private void OnEnable()
    {
        if (text == null) text = GetComponent<TMP_Text>();

        if (text == null)
        {
            Debugar.Log("This object doesn't have a TMP_Text atached on it. I can't print the version in it!!!");
            return;
        }

        text.text = $"v{Application.version}";
    }
}
