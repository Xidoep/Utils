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
        Debugar.Log("StopOnLoseFocus - OnEnable => Application.focusChanged += FocusChange");
        Application.focusChanged += FocusChange;
    }

    private void OnDisable()
    {
        Application.focusChanged -= FocusChange;
    }

    void FocusChange(bool focus)
    {
        Debugar.Log("Change focus");
        Time.timeScale = focus ? 1 : 0;
    }
}
