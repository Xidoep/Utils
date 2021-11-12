using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Utils_PaletaColors : MonoBehaviour
{
    //PUBLIQUES
    [SerializeField] Color color;
    [Range(0, 1)] public float randomHue = 0.01f;
    [Range(0, 0.4f)] public float randomSaturacio = 0.1f;
    [Range(0, 0.5f)] public float randomIluminacio = 0.1f;
    [Space(10)]
    [Range(0.05f, 0.25f)] public float separacio = 0.1f;

    //PROPIETATS
    public Color Color
    {
        set
        {
            if(color != value)
            {
                color = value;
            }
        }
    }
    public Vector3 HSV
    {
        get
        {
            Color.RGBToHSV(color, out float hue, out float saturacio, out float iluminacio);
            return new Vector3(hue, saturacio, iluminacio);
        }
    }



    //FUNCIONS
    /// <summary>
    /// Retorna 2 colors.
    /// [0]principal, [1]complementari.
    /// </summary>
    /// <returns></returns>
    public Color[] Contraris()
    {
        return new Color[]
            {
                Principal(),
                Complementari()
            };
    }
    
    /// <summary>
    /// Retorna 3 colors. 
    /// [0]principal, [1]adjacentsD, [2]adjacentsE.
    /// </summary>
    public Color[] Adjacents()
    {
        return new Color[]
           {
                Principal(),
                Adjacent(true),
                Adjacent(false)
           };
    }

    /// <summary>
    /// Retorna 3 colors. 
    /// [0]principal, [1]complementarisD, [2]complementarisE.
    /// </summary>
    public Color[] GetTris()
    {
        return new Color[]
            {
                Principal(),
                Complementari(true),
                Complementari(false)
            };
    }

    /// <summary>
    /// Retorna 4 colors. 
    /// [0]adjacentsD, [1]adjacentsE, [2]complementarisD, [3]complementarisE.
    /// </summary>
    public Color[] GetTetras()
    {
        return new Color[]
            {
                Adjacent(true),
                Adjacent(false),
                Complementari(true),
                Complementari(false)
            };
    }

    /// <summary>
    /// Retorna 5 colors. 
    /// [0]adjacentsD[1], [1]adjacentsD[0], [2]principal, [2]adjacentsE[0], [2]adjacentsE[1].
    /// </summary>
    public Color[] GetBano()
    {
        return new Color[]
           {
                Adjacent(true,true),
                Adjacent(true),
                Principal(),
                Adjacent(false),
                Adjacent(false,true)
           };
    }

    public Color Principal()
    {
        Vector3 hsv = HSV;
        return Color.HSVToRGB(
            Mathf.Repeat(hsv.x + Random.Range(-randomHue, +randomHue), 1),
            hsv.y + Random.Range(-randomSaturacio, +randomSaturacio),
            hsv.z + Random.Range(-randomIluminacio, +randomIluminacio), true);
    }
    public Color Adjacent(bool Dreta, bool segon = false)
    {
        Vector3 hsv = HSV;
        return Color.HSVToRGB(
            Mathf.Repeat(hsv.x + Random.Range(-randomHue, +randomHue) + (Dreta ? separacio : -separacio) * (segon ? 2 : 1), 1),
            hsv.y + Random.Range(-randomSaturacio, +randomSaturacio),
            hsv.z + Random.Range(-randomIluminacio, +randomIluminacio), true);
    }
    public Color Complementari()
    {
        Vector3 hsv = HSV;
        return Color.HSVToRGB(
            Mathf.Repeat((hsv.x + 0.5f + Random.Range(-randomHue, +randomHue)), 1),
            hsv.y + Random.Range(-randomSaturacio, +randomSaturacio),
            hsv.z + Random.Range(-randomIluminacio, +randomIluminacio), true);
    }
    public Color Complementari(bool Dreta, bool segon = false)
    {
        Vector3 hsv = HSV;
        return Color.HSVToRGB(
            Mathf.Repeat((hsv.x + 0.5f + Random.Range(-randomHue, +randomHue) + (Dreta ? separacio : -separacio) * (segon ? 2 : 1)), 1),
            hsv.y + Random.Range(-randomSaturacio, +randomSaturacio),
            hsv.z + Random.Range(-randomIluminacio, +randomIluminacio), true);
    }

 
}
