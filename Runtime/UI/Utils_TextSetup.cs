using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Just setup a text from outside without possibility to direct reference
/// </summary>
public class Utils_TextSetup : MonoBehaviour
{
    [SerializeField] TextMeshPro texte;

    public void Setup(string text) => texte.text = text;
    public void Setup(int numero, string format = "0") => texte.text = numero.ToString(format);
    public void Setup(float numero, string format = "0.0") => texte.text = numero.ToString(format);
}
