using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using XS_Utils;

public abstract class Utils_Volume : MonoBehaviour
{
    public enum Types { box, spfere }

    Collider col;
    BoxCollider boxCollider;
    SphereCollider sphereCollider;

    [Tooltip("Multiplies the value sended by the OnAproaching function")] [SerializeField] float multiply = 1;
    [Tooltip("Set it to true if you want to fire the OnFullyEntereing event every frame")] [SerializeField] bool invokeRepitelly = false;
    [SerializeField] Types types;
    [SerializeField] LayerMask FindedObjectLayer;
    [SerializeField] [Range(0, 30)] float range;

    bool IsIntoOutter;
    Collider[] colliders;
    float value;
    bool invoke_flanc = false;
    //Vector3 boxSize => Vector3.Scale(boxCollider.size, transform.localScale) + (Vector3.one * (range * 2));

    bool IsIntoInner => value == 1;
    bool IsBewteen => value == Mathf.Clamp01(value);
    bool IsCompletelyOut => value < 0;
    public Types MyTypes => types;
    public Vector3 BoxSize => transform.localScale;
    public float ShpereSize => transform.localScale.magnitude * 0.3f;
    public float Range => range;

    public abstract void OnAproaching(float distance);
    public abstract void OnFullyEntering();
    public abstract void OnExit();



    void OnEnable()
    {
        SetColliders();
        colliders = new Collider[1];
    }
    void Update()
    {
        //First check if was market as something inside, if not stops the process.
        if (!IsIntoOutter)
        {
            //As nothing is inside, check for near colliders
            if(CheckForColliders(out colliders) > 0)
            {
                //as soon as it finds one, it starts getting the value distance
                value = Valor();

                //If collider founded is fully inside the inner trigger it will fire the events, and will stop here.
                if (IsIntoInner)
                {
                    if (!invokeRepitelly)
                    {
                        if (!invoke_flanc)
                        {
                            invoke_flanc = true;
                            OnFullyEntering();
                        }
                    }
                    else OnFullyEntering();
                    return;
                }

                //This means that the collider is fully catch, so no longer need to find for more colliders.
                if (IsBewteen) 
                    IsIntoOutter = true;
            }
            return;
        }

        //Chech if collider is out of blending zone.
        //if it is inside the trigger, it will reestart the cheching for colliders above. To check if the collider is fully inside.
        if(!IsBewteen || IsIntoInner)
        {
            IsIntoOutter = false;
            if (IsCompletelyOut)
            {
                OnExit();
            }
        }

        //Fires the funcion only when the value changes.
        if (value != Valor())
        {
            value = Valor();
            OnAproaching(Mathf.Clamp01(value) * multiply);
        }

        Debugar.DrawLine(colliders[0].transform.position, col.ClosestPoint(colliders[0].transform.position), Color.red * (1 - value) + Color.green * value);
    }

    int CheckForColliders(out Collider[] colliders)
    {
        colliders = this.colliders;
        switch (types)
        {
            case Types.spfere:
                return Physics.OverlapSphereNonAlloc(transform.position, ShpereSize + range, colliders, FindedObjectLayer);
            default:
                return Physics.OverlapBoxNonAlloc(transform.position, BoxSize + (Vector3.one * (range * 2)), colliders, transform.rotation, FindedObjectLayer);
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



}
