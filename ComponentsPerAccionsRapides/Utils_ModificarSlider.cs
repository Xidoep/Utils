using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Utils_ModificarSlider : MonoBehaviour
{
    Slider slider;

    private void OnEnable() { if (slider == null) slider = GetComponent<Slider>(); }

    public void Augmentar(int quantitat = 1) => slider.value += quantitat;
    public void Disminuir(int quantitat = 1) => slider.value -= quantitat;
}
