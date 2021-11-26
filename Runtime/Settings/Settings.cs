using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "XS/Utils/Settings", fileName = "Settings")]
public class Settings : ScriptableObject
{
    [SerializeField] [Range(0.75f, 1.5f)] float uiSize  = 1;
    System.Action<float> enUiSize;

    public void UiSize_Set(float valor)
    {
        uiSize = valor;
        enUiSize?.Invoke(uiSize);
    }
    public void UiSize_AddEvent(System.Action<float> action) => enUiSize += action;
    public void UiSize_AddAndInvokeEvent(System.Action<float> action) 
    {
        action.Invoke(uiSize);
        UiSize_AddEvent(action);
    } 
    public void UiSize_RemoveEvent(System.Action<float> action) => enUiSize -= action;



    private void OnValidate()
    {
        enUiSize?.Invoke(uiSize);
    }

}
