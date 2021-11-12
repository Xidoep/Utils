using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Volum : MonoBehaviour
{
    //REFERENCIES
    BoxCollider boxCollider;

    //PUBLIQUES
    [SerializeField] LayerMask capa;
    [SerializeField] [Range(1, 10)] float rang;
    [SerializeField] UnityEvent<float> funcio;
    [SerializeField] Color color = Color.red;

    //PRIVADES
    bool valorar;
    Collider[] colliders;
    float valor;

    //PROPIETATS
    Vector3 boxSize => Vector3.Scale(boxCollider.size, transform.localScale) + (Vector3.one * (rang * 2));



    public UnityAction<float> Accio { set => funcio.AddListener(value); }

    void OnEnable()
    {
        boxCollider = GetComponent<BoxCollider>();
        colliders = new Collider[1];
    }

    void Update()
    {
        if (!valorar)
        {
            if (Physics.OverlapBoxNonAlloc(transform.position, boxSize / 2f, colliders, transform.rotation, capa) > 0)
            {
                if (Valor(colliders[0].transform) == 1)
                    return;

                if (Valor(colliders[0].transform) == Mathf.Clamp01(Valor(colliders[0].transform))) valorar = true;
            }
            return;
        }

        if(valor != Mathf.Clamp01(valor) || valor == 1)
        {
            valorar = false;
        }

        
        if(valor != Valor(colliders[0].transform))
        {
            valor = Valor(colliders[0].transform);
            funcio.Invoke(Mathf.Clamp01(valor));
        }

        Debug.DrawLine(colliders[0].transform.position, boxCollider.ClosestPoint(colliders[0].transform.position), new Color(valor,valor,valor,1));
    }



    float Valor(Transform t) => (rang - Vector3.Distance(t.position, boxCollider.ClosestPoint(t.position))) / rang;

    void OnValidate()
    {
        if (boxCollider == null) boxCollider = GetComponent<BoxCollider>();

        if (boxCollider == null)
        {
            boxCollider = gameObject.AddComponent<BoxCollider>();
            return;
        }

        if (!boxCollider.isTrigger) boxCollider.isTrigger = true;
    }

    void OnDrawGizmos()
    {
        if (boxCollider == null)
            return;

        Gizmos.color = new Color(color.r, color.g, color.b, 0.5f);
        Gizmos.DrawWireCube(transform.position, boxSize);
        Gizmos.DrawCube(transform.position, boxSize - (Vector3.one * (rang * 2)));
    }

}
