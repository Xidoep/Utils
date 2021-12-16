using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "XS/Utils/Settings", fileName = "Settings")]
public class Settings : ScriptableObject
{

    /*[SerializeField] [Range(0.75f, 1.5f)] float uiSize  = 1;
System.Action<float> enUiSize;

public void UiSize_Set(float valor)
{
uiSize = valor;
enUiSize?.Invoke(uiSize);
}
public float UISize_Get() => uiSize;

public void UiSize_AddEvent(System.Action<float> action) => enUiSize += action;
public void UiSize_AddAndInvokeEvent(System.Action<float> action) 
{
action.Invoke(uiSize);
UiSize_AddEvent(action);
} 
public void UiSize_RemoveEvent(System.Action<float> action) => enUiSize -= action;


private void OnValidate()
{
tamanyInterficie?.Event_Invoke();
enUiSize?.Invoke(uiSize);
}

*/
    public Variable<float> interfaceSize; //1
     [Tooltip("Entre: 0.75f i 1.5f")]public void InterfaceSize(float value) => interfaceSize.Set(value);


    [System.Serializable]
    public class Variable<T>
    {
        public T value;
        System.Action<T> onValueChange;

        public void Set(T value)
        {
            this.value = value;
            onValueChange?.Invoke(this.value);
        }
        public T Get() => value;



        public void Event_Add(System.Action<T> action) => onValueChange += action;
        public void Event_InvokeAndAdd(System.Action<T> action)
        {
            action?.Invoke(value);
            Event_Add(action);
        }
        public void Event_Invoke() => onValueChange?.Invoke(value);
        public void Event_Remove(System.Action<T> action) => onValueChange -= action;

    }
}
