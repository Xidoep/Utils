using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Xido Studio/Object Pool/RecordarParent")]
public class Utils_RecordarParent : MonoBehaviour
{
    [Informacio] public string Info = "Guardar el parent per recuperarlo quan es vulgui";
    Transform parent;

    public void RecordarParent()
    {
        parent = transform.parent;
    }
    public void RecordarParent(Transform _parent)
    {
        parent = _parent;
    }
    public void RecuperarParent()
    {
        transform.parent = parent;
    }
}
