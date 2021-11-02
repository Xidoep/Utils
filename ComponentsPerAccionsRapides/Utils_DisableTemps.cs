using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XS_Utils;

public class Utils_DisableTemps : MonoBehaviour
{
    [SerializeField] bool enEnable;
    [SerializeField] float tempsDisable;
    private void OnEnable()
    {
        if (!enEnable)
            return;

        if (tempsDisable == 0)
            return;

        Disable(tempsDisable);
    }
    public void Disable() => gameObject.SetActive(false, tempsDisable);
    public void Disable(float temps) => gameObject.SetActive(false, temps);
}
