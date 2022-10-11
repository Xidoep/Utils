using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XS_Utils;


public class XS_Button : Button
{
    public UnityEvent onEnter;
    public UnityEvent onExit;

    public void Interactable(bool interactable) => this.interactable = interactable;

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        onEnter.Invoke();
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        onExit.Invoke();
    }

}
