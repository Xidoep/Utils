using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils_AplicarForca : MonoBehaviour
{
    public enum Metode
    {
        inici, constant
    }

    public Rigidbody rb;
    public Metode metode;
    public Vector3 direccio;
    Vector3 Direccio
    {
        get
        {
            if (!local)
            {
                return direccio;
            }
            else
            {
                return transform.right * direccio.x + transform.up * direccio.y + transform.forward * direccio.z;
            }
        }
    }
    public float forca;
    public bool local;

    private void OnEnable()
    {
        if (!rb)
        {
            rb.GetComponent<Rigidbody>();
        }

        if (metode == Metode.inici)
            rb.AddForce(Direccio * forca, ForceMode.Impulse);
    }
    private void FixedUpdate()
    {
        if (metode != Metode.constant)
            return;

        rb.AddForce(Direccio * forca * Time.fixedDeltaTime);
    }
}
