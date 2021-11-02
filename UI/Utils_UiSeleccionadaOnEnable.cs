using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Xido Studio/Utils/UI Seleccionada Enable")]
public class Utils_UiSeleccionadaOnEnable : MonoBehaviour
{
    [Informacio] public string Info = "Selecciona l'element quan s'activa.";
    [Informacio] public string Rao = "Aixì no es perd el cursor navegant amb Gamepad";
    public Selectable selectable;

    private void OnEnable()
    {
        selectable.Select();
    }
}
