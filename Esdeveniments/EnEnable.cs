using UnityEngine;
using UnityEngine.Events;

public class EnEnable : MonoBehaviour
{
    public UnityEvent esdeveniment;

    private void OnEnable() => esdeveniment.Invoke();
}
