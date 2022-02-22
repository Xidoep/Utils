using UnityEngine;
using UnityEngine.Events;

public class EnEnable : MonoBehaviour
{
    [SerializeField] UnityEvent esdeveniment;

    private void OnEnable() => esdeveniment.Invoke();
}
