using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using XS_Utils;

public class EnEnable_Temps : MonoBehaviour
{
    [SerializeField] float temps;
    [SerializeField] bool frameDependent;
    [SerializeField] UnityEvent esdeveniment;

    public UnityEvent Esdeveniment => esdeveniment;

    private void OnEnable() 
    {
        if(!frameDependent)
            XS_Coroutine.StartCoroutine_Ending(temps, Invocar);
        else
            XS_Coroutine.StartCoroutine_Ending_FrameDependant(temps, Invocar);
    } 

    void Invocar() => esdeveniment.Invoke();
}
