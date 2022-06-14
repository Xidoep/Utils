using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MoveComponentTool
{
    //The "CONTEXT" part is used to make it appear on the context menu.
    //The "Compoment" part is to make it appear on all the components. If you change it for an specific, it just will appear on them.
    const string MENU_MOVE_TO_TOP_KEY = "CONTEXT/Component/Move To Top";
    const string MENU_MOVE_TO_BUTTOM_KEY = "CONTEXT/Component/Move To Mottom"; 

    //This moves the component all way to the top
    [MenuItem(MENU_MOVE_TO_TOP_KEY, priority =501)]
    public static void MoveComponentToTopMenuItem(MenuCommand command)
    {
        while (UnityEditorInternal.ComponentUtility.MoveComponentUp((Component)command.context)) ;
    }

    [MenuItem(MENU_MOVE_TO_TOP_KEY, validate = true)]
    public static bool MoveComponentToTopMenuItemValidate(MenuCommand command)
    {
        Component[] components = ((Component)command.context).gameObject.GetComponents<Component>();

        for (int i = 0; i < components.Length; i++)
        {
            if(components[i] == ((Component)command.context))
            {
                if (i == 1)
                    return false;
            }
        }
        return true;
    }

    [MenuItem(MENU_MOVE_TO_BUTTOM_KEY, priority = 501)]
    public static void MoveComponentToBottomMenuItem(MenuCommand command)
    {
        while (UnityEditorInternal.ComponentUtility.MoveComponentDown((Component)command.context)) ;
    }

    [MenuItem(MENU_MOVE_TO_BUTTOM_KEY, validate = true)]
    public static bool MoveComponentToBottomMenuItemValidate(MenuCommand command)
    {
        Component[] components = ((Component)command.context).gameObject.GetComponents<Component>();

        for (int i = 0; i < components.Length; i++)
        {
            if (components[i] == ((Component)command.context))
            {
                if (i == (components.Length-1))
                    return false;
            }
        }
        return true;
    }
}
