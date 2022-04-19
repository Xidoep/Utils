using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using XS_Utils;

public class Utils_Volume_FloatFunction : Utils_Volume
{
    [Tooltip("[OnAproaching] - Calls every fram that collider is in the blending zone, sending the value of the distance beetwen 0(far) and 1(near)")] [SerializeField] UnityEvent<float> onAproaching;
    [Tooltip("[OnFullyEntering] - Calls when the collider is completely inside the trigger")] [SerializeField] UnityEvent onFullyEntering;
    [Tooltip("[OnExit] - Calls ones when the collider gets out of the blending zone")] [SerializeField] UnityEvent onExit;
    public override void OnAproaching(float distance) => onAproaching.Invoke(distance);
    public override void OnFullyEntering() => onFullyEntering.Invoke();
    public override void OnExit() => onExit.Invoke();


    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        switch (MyTypes)
        {
            case Types.spfere:
                Gizmos.DrawWireSphere(transform.position, ShpereSize + Range);
                Gizmos.DrawSphere(transform.position, ShpereSize);
                break;
            default:
                Gizmos.DrawWireCube(transform.position, BoxSize + (Vector3.one * (Range * 2)));
                Gizmos.DrawCube(transform.position, BoxSize);
                break;
        }

    }
}
