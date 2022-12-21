using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils_EsdevenimentDelegatInt : MonoBehaviour
{
    //Igual que Utils_EsdevenimentDelegat, pero amb una Event amb int

    System.Action<int> esdeveniment;

    public void Esdevenir(int arg) => esdeveniment?.Invoke(arg);

    public void Registrar(System.Action<int> accio) => esdeveniment += accio;
    public void Desregistrar(System.Action<int> accio) => esdeveniment -= accio;
}
