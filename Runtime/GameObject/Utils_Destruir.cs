using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Xido Studio/Utils/Destruir")]
public class Utils_Destruir : MonoBehaviour
{
    public float temps;

    private void OnEnable()
    {
        if (temps == 0)
            return;

        Destruir(temps);
    }

    public void Destruir() => Destruir(temps);
    public void Destruir(float temps)
    {
        Destroy(gameObject, temps);
    }
}
