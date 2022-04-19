using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XS_Utils;

[CreateAssetMenu(menuName = "Xido Studio/Utils/StopOnLoseFocus")]
public class StopOnLoseFocus : ScriptableObject
{
    [Informacio][SerializeField] string information = "It automatically register a function to pause when loses focus.";
    private void OnEnable()
    {
        if (Application.isEditor)
            return;

        Debugar.Log("[StopOnLoseFocus] OnEnable => Application.focusChanged += FocusChange");
        Application.focusChanged += FocusChange;
    }

    private void OnDisable()
    {
        if (Application.isEditor)
            return;

        Application.focusChanged -= FocusChange;
    }

    void FocusChange(bool focus)
    {
        Debugar.Log($"[StopOnLoseFocus] FocusChange({focus})");
        Time.timeScale = focus ? 1 : 0;
    }
}
