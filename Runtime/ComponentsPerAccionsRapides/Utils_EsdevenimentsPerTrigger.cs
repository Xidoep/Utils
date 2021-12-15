using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using XS_Utils;

public class Utils_EsdevenimentsPerTrigger : MonoBehaviour
{
    public LayerMask layerMask;
    public float radi;
    public UnityEvent onEnter;
    public UnityEvent onExit;

    Collider[] colliders;
    Collider[] _tmp;

    void Update()
    {
        _tmp = Physics.OverlapSphere(transform.position, radi, layerMask);
        if(colliders.Length < _tmp.Length && colliders.Length == 0) //Invoca una sola vegada si no tenia colliders previament
        {
            colliders = _tmp;
            onEnter?.Invoke();
        }
        else if(_tmp.Length == 0)
        {
            colliders = _tmp;
            onExit?.Invoke();
        }

       
    }
}
