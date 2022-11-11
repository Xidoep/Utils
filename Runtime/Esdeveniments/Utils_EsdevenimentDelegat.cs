using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils_EsdevenimentDelegat : MonoBehaviour
{
    System.Action esdeveniment;

    public void Esdevenir() => esdeveniment?.Invoke();

    public void Registrar(System.Action accio) => esdeveniment += accio;
    public void Desregistrar(System.Action accio) => esdeveniment -= accio;

}
