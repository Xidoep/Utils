using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Xido Studio/Utils/StopOnLoseFocus")]
public class StopOnLoseFocus : ScriptableObject
{
    private void OnEnable()
    {
        Application.focusChanged += FocusChange;
    }

    private void OnDisable()
    {
        Application.focusChanged -= FocusChange;
    }

    void FocusChange(bool focus)
    {
        Time.timeScale = focus ? 1 : 0;
    }
}
