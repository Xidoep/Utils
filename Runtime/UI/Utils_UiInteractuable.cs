using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Xido Studio/Utils/UI Interactuable")]
public class Utils_UiInteractuable : MonoBehaviour
{
    [Informacio] public string Info = "Canvia un UI seleccionable, si es interactuable o no";
    public Selectable selectable;

    public void Interactuable(bool interactuable)
    {
        selectable.interactable = interactuable;
    }
}
