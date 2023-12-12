using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Utils_RuntimeEsdeveniments : MonoBehaviour
{
    public UnityEvent enAwake;
    public UnityEvent enStart;
    public UnityEvent enUpdate;
    public UnityEvent enLateUpdate;
    public UnityEvent enPerdreFocus;
    public UnityEvent enQuit;

    void Awake()
    {
        enAwake.Invoke();
    }

    void Start()
    {
        enStart.Invoke();
    }

    void Update()
    {
        enUpdate.Invoke();
    }
    private void LateUpdate()
    {
        enLateUpdate.Invoke();
    }

    void OnApplicationFocus(bool focus)
    {
        if (!focus) enPerdreFocus.Invoke();
    }

    void OnApplicationQuit()
    {
        enQuit.Invoke();
    }
}
