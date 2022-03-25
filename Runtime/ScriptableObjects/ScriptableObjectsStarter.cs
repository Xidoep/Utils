using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectsStarter : MonoBehaviour
{
    [SerializeField] ScriptableObject[] scriptableObjects;
    private void Awake()
    {
        for (int i = 0; i < scriptableObjects.Length; i++)
        {
            scriptableObjects[i].ToString();
        }
    }
}
