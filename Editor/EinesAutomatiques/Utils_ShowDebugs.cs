using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Utils_ShowDebugs : MonoBehaviour
{
    [MenuItem("XidoStudio/Show Debugs", priority = 15)]
    public static void Show()
    {
        show = !show;
    }

    [MenuItem("XidoStudio/Show Debugs", true)]
    public static bool ShowValidate()
    {
        Menu.SetChecked("XidoStudio/Show Debugs", show);
        Debug.unityLogger.logEnabled = show;
        return true;
    }

    static bool show;

    private void Awake()
    {
        Debug.unityLogger.logEnabled = show;
    }
}
