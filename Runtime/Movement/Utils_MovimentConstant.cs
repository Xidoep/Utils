using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Xido Studio/Utils/MovimentConstant")]
public class Utils_MovimentConstant : MonoBehaviour
{
    [Informacio] public string Info = "Es mou en local cap a la direccio donada";
    public Vector3 direccio;
    public float velocitat;

    void Update()
    {
        transform.localPosition += direccio * Time.deltaTime * velocitat;
    }
}
