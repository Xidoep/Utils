using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils_EsdevenimentDelegatBool : MonoBehaviour
{
    //Igual que Utils_EsdevenimentDelegat, pero amb una Event amb bool

    System.Action<bool> esdeveniment;

    public void Esdevenir(bool arg) => esdeveniment?.Invoke(arg);

    public void Registrar(System.Action<bool> accio) => esdeveniment += accio;
    public void Desregistrar(System.Action<bool> accio) => esdeveniment -= accio;
}
