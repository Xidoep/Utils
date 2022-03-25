using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using XS_Utils;

public class EnEnable_Temps : MonoBehaviour
{
    [SerializeField] float temps;
    [SerializeField] UnityEvent esdeveniment;



    private void OnEnable() => XS_Coroutine.StartCoroutine_Ending(temps, Invocar);

    void Invocar() => esdeveniment.Invoke();
}
