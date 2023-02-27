using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils_EsdevenimentDelegat : MonoBehaviour
{
    //Eina per poder registrar events d'un altre script sense haver de referenciar-lo en un segon script.
    //Ex: Un boto d'un Popup dinamic: Registres la funcio aqui i referencies Esdevenir() desde el prefab del Popup.

    System.Action esdeveniment;

    public void Esdevenir() => esdeveniment?.Invoke();

    public void Registrar(System.Action accio) => esdeveniment += accio;
    public void Desregistrar(System.Action accio) => esdeveniment -= accio;

    public void Registrar(System.Action accio1, System.Action accio2) 
    {
        Registrar(accio1);
        Registrar(accio2);
    }
    public void Desregistrar(System.Action accio1, System.Action accio2) 
    {
        Desregistrar(accio1);
        Desregistrar(accio2);
    } 
}
