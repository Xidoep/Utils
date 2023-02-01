using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XS_Utils;

public class Utils_Instantiable : MonoBehaviour
{
    [Nota("Posa aquest script a l'element del projecet que vulguis instanciar. Així només cal que el referenciis desde un boto.")]

    Vector3 position;
    Quaternion rotation;

    Vector3 Position() => position;
    Quaternion Rotation() => rotation;



    public void Instantiate() => gameObject.InstantiatePool();

    public void Instantiate(Vector3 position)
    {
        this.position = position;

        gameObject.InstantiatePool(Position);
    }

    public void Instantiate(Vector3 position, Quaternion rotation)
    {
        this.position = position;
        this.rotation = rotation;

        gameObject.InstantiatePool(Position, Rotation);
    }

    public void Instantiate(Vector3 position, Quaternion rotation, Transform transform)
    {
        this.position = position;
        this.rotation = rotation;

        gameObject.InstantiatePool(Position, Rotation, transform);
    }
    public void Instantiate(Vector3 position, Quaternion rotation, float uniformScale)
    {
        this.position = position;
        this.rotation = rotation;

        gameObject.InstantiatePool(Position, Rotation).transform.localScale = Vector3.one * uniformScale;
    }
}
