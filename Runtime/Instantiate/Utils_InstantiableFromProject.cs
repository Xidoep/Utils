using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils_InstantiableFromProject : MonoBehaviour
{
    //Posa aquest script a l'element del projecet que vulguis instanciar. Així només cal que el referenciis desde un boto
    //Així es poden instanciar gameobjects desde botons, o desde scripts més facilment.

    public void Instantiate() => InstantiateReturn();
    public GameObject InstantiateReturn() => Instantiate(gameObject);

    public void Instantiate(Transform parent) => InstantiateReturn(parent);
    public GameObject InstantiateReturn(Transform parent) => Instantiate(gameObject, parent);

    public void Instantiate(Vector3 position) => InstantiateReturn(position);
    public GameObject InstantiateReturn(Vector3 position) => Instantiate(gameObject, position, Quaternion.identity);

    public void Instantiate(Vector3 position, Quaternion rotation) => InstantiateReturn(position, rotation);
    public GameObject InstantiateReturn(Vector3 position, Quaternion rotation) => Instantiate(gameObject, position, rotation);

    public void Instantiate(Vector3 position, Quaternion rotation, Transform parent) => InstantiateReturn(position, rotation, parent);
    public GameObject InstantiateReturn(Vector3 position, Quaternion rotation, Transform parent) => Instantiate(gameObject, position, rotation, parent);
}
