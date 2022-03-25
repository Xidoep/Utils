using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XS_Utils;

public class Prova2 : MonoBehaviour
{
    [ExposedField(typeof(Prova2))]
    private int hola = 1;
    [ExposedField(typeof(Prova2))]
    private string adeu = "ei";
    public ExposedValueSelector exposedValue;
    // Start is called before the first frame update
    
    [ContextMenu("Print")]
    void Print()
    {
        Debugar.Log(exposedValue.fieldName.ToString());
    }
}
