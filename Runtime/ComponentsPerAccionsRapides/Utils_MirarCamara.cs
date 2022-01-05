using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XS_Utils;

[AddComponentMenu("Xido Studio/Utils/MirarCamara")]
public class Utils_MirarCamara : MonoBehaviour
{
    [Informacio] public string Info = "Agafa la mateixa rotacio que la 'Main Camera'";
    Camera cam;

    private void Awake()
    {
        if (cam == null) cam = Camera.main;
    }

    void OnEnable()
    {
        if(cam == null) cam = Camera.main;
    }

    void Update()
    {
        transform.LookAtTarget(cam.gameObject);
    }
}
