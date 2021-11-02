using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XS_Utils;

public class Utils_DisableTemps : AnimacioPerCodi_Base
{
    [SerializeField] bool enEnable;
    [SerializeField] float tempsDisable;
    [SerializeField] [Tooltip("Animacio que fara mentre espera a Disoldres")] AnimacioPerCodi.Animacio animacio;

    [SerializeField] AnimacioPerCodi_All.T_All[] transformacions;
    internal override Transformacions[] GetTransformacions => transformacions;

    private void OnEnable()
    {
        if (!enEnable)
            return;

        if (tempsDisable == 0)
            return;

        Disable(tempsDisable);
    }

    public void Disable() 
    { 
        gameObject.SetActive(false, tempsDisable);

        StartCoroutine(animacio.Play(transform));
        Play();

    }
    public void Disable(float temps) 
    {
        gameObject.SetActive(false, temps);

        StartCoroutine(animacio.Play(transform));
        Play();
    }

    /*public override void Transformar(float temps) 
    {
        if (GetTransformacions.Length == 0)
            return;

        GetTransformacions[0].Transformar(transform, temps);
    } */
}
