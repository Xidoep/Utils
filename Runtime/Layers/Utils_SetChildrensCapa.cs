using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils_SetChildrensCapa : MonoBehaviour
{
    private void Awake()
    {
        SetCapa();
    }
    
    [ContextMenu("SetCapa")]void SetCapa()
    {
        foreach (var item in GetComponentsInChildren<Transform>())
        {
            item.gameObject.layer = gameObject.layer;
        }
    }
}
