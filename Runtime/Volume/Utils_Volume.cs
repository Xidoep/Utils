using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Utils_Volume : MonoBehaviour
{
    public enum Types { box, spfere }

    Collider col;
    BoxCollider boxCollider;
    SphereCollider sphereCollider;

    
    [SerializeField] Types types;
    [SerializeField] LayerMask FindedObjectLayer;
    [SerializeField] [Range(0, 30)] float range;
    [SerializeField] float multiply;
    [SerializeField] UnityEvent<float> function;
    [Header("Debug")]
    [SerializeField] Color color = Color.red;

    bool InOutterVolumen;
    Collider[] colliders;
    float value;
    //Vector3 boxSize => Vector3.Scale(boxCollider.size, transform.localScale) + (Vector3.one * (range * 2));

    bool IsIntoInnerVolume => value == 1;
    bool IsBewteenVolumes => value == Mathf.Clamp01(value);
    public UnityAction<float> Accio { set => function.AddListener(value); }
    Vector3 boxSize => transform.localScale;
    float shpereSize => transform.localScale.magnitude * 0.3f;

    void OnEnable()
    {
        SetColliders();
        colliders = new Collider[1];
    }
    void Update()
    {
        if (!InOutterVolumen)
        {
            if(CheckForColliders(out colliders) > 0)
            {
                value = Valor();
                if (IsIntoInnerVolume)
                    return;

                if (IsBewteenVolumes) 
                    InOutterVolumen = true;
            }
            return;
        }

        if(!IsBewteenVolumes || IsIntoInnerVolume)
            InOutterVolumen = false;

        if (value != Valor())
        {
            value = Valor();
            function.Invoke(Mathf.Clamp01(value) * multiply);
        }

        Debug.DrawLine(colliders[0].transform.position, col.ClosestPoint(colliders[0].transform.position), Color.red * (1 - value) + Color.green * value);
    }

    int CheckForColliders(out Collider[] colliders)
    {
        colliders = this.colliders;
        switch (types)
        {
            case Types.spfere:
                return Physics.OverlapSphereNonAlloc(transform.position, shpereSize + range, colliders, FindedObjectLayer);
            default:
                return Physics.OverlapBoxNonAlloc(transform.position, boxSize + (Vector3.one * (range * 2)), colliders, transform.rotation, FindedObjectLayer);
        }
    }

    //Return a valuer bewteen 0 and 1, beeing 0 de fardest point to the volume and 1 the clostest.
    float Valor() => (range - Vector3.Distance(colliders[0].transform.position, col.ClosestPoint(colliders[0].transform.position))) / range;

    void OnValidate()
    {
        SetColliders();
    }

    void SetColliders()
    {
        switch (types)
        {
            case Types.spfere:
                if (boxCollider != null && boxCollider.enabled) boxCollider.enabled = false;
                if (sphereCollider == null) sphereCollider = GetComponent<SphereCollider>();
                if (sphereCollider == null) sphereCollider = gameObject.AddComponent<SphereCollider>();


                if (!sphereCollider.enabled) sphereCollider.enabled = true;
                if (!sphereCollider.isTrigger) sphereCollider.isTrigger = true;
                if (sphereCollider.radius != 0.5f) sphereCollider.radius = 0.5f;
                if (col != (Collider)sphereCollider) col = (Collider)sphereCollider;
                break;
            default:
                if (sphereCollider != null && sphereCollider.enabled) sphereCollider.enabled = false;
                if (boxCollider == null) boxCollider = GetComponent<BoxCollider>();
                if (boxCollider == null) boxCollider = gameObject.AddComponent<BoxCollider>();


                if (!boxCollider.enabled) boxCollider.enabled = true;
                if (!boxCollider.isTrigger) boxCollider.isTrigger = true;
                if (boxCollider.size != Vector3.one) boxCollider.size = Vector3.one;
                if (col != (Collider)boxCollider) col = (Collider)boxCollider;
                break;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(color.r, color.g, color.b, 0.5f);
        switch (types)
        {
            case Types.spfere:
                Gizmos.DrawWireSphere(transform.position, shpereSize + range);
                Gizmos.DrawSphere(transform.position, shpereSize);
                break;
            default:
                Gizmos.DrawWireCube(transform.position, boxSize + (Vector3.one * (range * 2)));
                Gizmos.DrawCube(transform.position, boxSize);
                break;
        }
        
    }

}
