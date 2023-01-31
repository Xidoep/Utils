using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-10)]
public class Utils_MainCamera_Acces : MonoBehaviour
{
    static Camera camara;
    static Canvas canvas;

    private void OnEnable()
    {
        camara = gameObject.GetComponent<Camera>();
        canvas = gameObject.GetComponentInChildren<Canvas>();
    }
    public static Camera Camera => camara;
    public static Canvas Canvas => canvas;
}
