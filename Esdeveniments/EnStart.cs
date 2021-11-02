using UnityEngine;
using UnityEngine.Events;

public class EnStart : MonoBehaviour
{
    public UnityEvent esdeveniment;

    private void Start() => esdeveniment.Invoke();
}
