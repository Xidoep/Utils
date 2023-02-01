using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class ConverterToXSVersionsComponentTool
{
    const string CONVERT_TO_XSBUTTON = "CONTEXT/Button/Convert to XS_Button";



    [MenuItem(CONVERT_TO_XSBUTTON, priority = 501)]
    static void To_XS_Button(MenuCommand command)
    {
        //Button button = ((Button)command.context);
        GameObject gameObject = ((Component)command.context).gameObject;
        Object.DestroyImmediate((command.context));
        //((Component)command.context).
        gameObject.AddComponent<XS_Button>();
    }

}
