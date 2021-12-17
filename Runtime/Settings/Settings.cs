using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "XS/Utils/Settings", fileName = "Settings")]
public class Settings : ScriptableObject
{
    public Variable<float> interfaceSize; //1
    [Tooltip("Entre: 0.75f i 1.5f")]public void InterfaceSize(float value) => interfaceSize.Set(value);



    private void OnValidate()
    {
        interfaceSize.Event_Invoke();
    }



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
