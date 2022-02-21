using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Expose interatable of an UI element to be called form outside
/// </summary>
public class Utils_UiInteractuable : MonoBehaviour
{
    [Informacio] public string Info = "Canvia un UI seleccionable, si es interactuable o no";
    [SerializeField] Selectable selectable;

    public void Interactuable(bool interactuable)
    {
        selectable.interactable = interactuable;
    }
}
