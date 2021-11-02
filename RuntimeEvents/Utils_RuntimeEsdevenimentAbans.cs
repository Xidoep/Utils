using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[DefaultExecutionOrder(-1)]
public class Utils_RuntimeEsdevenimentAbans : MonoBehaviour
{
    public bool dontDestroyOnLoad;
    public UnityEvent enAbansAwake;

    void Awake()
    {
        if(dontDestroyOnLoad) 
            DontDestroyOnLoad(gameObject);
        
        enAbansAwake.Invoke();
    }
}
