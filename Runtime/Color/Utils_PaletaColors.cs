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

    [Apartat("Mostra")]
    [Header("Contraris")]
    [SerializeField] Color conPrincipal;
    [SerializeField] Color contrari;
    [Header("Adjacents")]
    [SerializeField] Color adjUp;
    [SerializeField] Color adjPrincipal;
    [SerializeField] Color adjDown;
    [Header("Triangle")]
    [SerializeField] Color triUp;
    [SerializeField] Color triPrincipal;
    [SerializeField] Color triDown;
    [Header("Quadrat")]
    [SerializeField] Color qua1;
    [SerializeField] Color qua2;
    [SerializeField] Color qua3;
    [SerializeField] Color qua4;

    private void OnValidate()
    {
        if (Application.isPlaying)
            return;

        Color[] colors = Contraris();
        conPrincipal = colors[0];
        contrari = colors[1];

        colors = Adjacents();
        adjPrincipal = colors[0];
        adjUp = colors[1];
        adjDown = colors[2];

        colors = Tris();
        triPrincipal = colors[0];
        triUp = colors[1];
        triDown = colors[2];

        colors = Tetras();
        qua1 = colors[0];
        qua2 = colors[1];
        qua3 = colors[2];
        qua4 = colors[3];
    }

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
    public Color[] Tris()
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
    public Color[] Tetras()
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

    Color Principal()
    {
        Vector3 hsv = HSV;
        return Color.HSVToRGB(
            Mathf.Repeat(hsv.x + Random.Range(-randomHue, +randomHue), 1),
            hsv.y + Random.Range(-randomSaturacio, +randomSaturacio),
            hsv.z + Random.Range(-randomIluminacio, +randomIluminacio), true);
    }
    Color Adjacent(bool Dreta, bool segon = false)
    {
        Vector3 hsv = HSV;
        return Color.HSVToRGB(
            Mathf.Repeat(hsv.x + Random.Range(-randomHue, +randomHue) + (Dreta ? separacio : -separacio) * (segon ? 2 : 1), 1),
            hsv.y + Random.Range(-randomSaturacio, +randomSaturacio),
            hsv.z + Random.Range(-randomIluminacio, +randomIluminacio), true);
    }
    Color Complementari()
    {
        Vector3 hsv = HSV;
        return Color.HSVToRGB(
            Mathf.Repeat((hsv.x + 0.5f + Random.Range(-randomHue, +randomHue)), 1),
            hsv.y + Random.Range(-randomSaturacio, +randomSaturacio),
            hsv.z + Random.Range(-randomIluminacio, +randomIluminacio), true);
    }
    Color Complementari(bool Dreta, bool segon = false)
    {
        Vector3 hsv = HSV;
        return Color.HSVToRGB(
            Mathf.Repeat((hsv.x + 0.5f + Random.Range(-randomHue, +randomHue) + (Dreta ? separacio : -separacio) * (segon ? 2 : 1)), 1),
            hsv.y + Random.Range(-randomSaturacio, +randomSaturacio),
            hsv.z + Random.Range(-randomIluminacio, +randomIluminacio), true);
    }

 
}
