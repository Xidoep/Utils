using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XS_Utils;

[AddComponentMenu("Xido Studio/Utils/MirarCamara")]
public class Utils_MirarCamara : MonoBehaviour
{
    [Informacio] public string Info = "Agafa la mateixa rotacio que la 'Main Camera'";

    void OnEnable()
    {
        if(Utils_MainCamera_Acces.Camera == null)
            Debugar.LogError("[Utils_MirarCamera] Falta un (Utils_MainCamera_Acces)");
    }

    void Update()
    {
        transform.LookAtTarget(Utils_MainCamera_Acces.Camera.gameObject);
    }
}
