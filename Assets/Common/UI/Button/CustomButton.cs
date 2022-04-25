using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomButton : Button
{
    public UnityAction OnPress =default;
    public UnityAction OnHighlight =default;
    public UnityAction OnNormal=default;
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);

    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);

    }

    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        base.DoStateTransition(state, instant);
        switch (state)
        {
            case SelectionState.Normal:
                OnNormal?.Invoke();
                break;
            case SelectionState.Highlighted:
                OnHighlight?.Invoke();
                break;
            case SelectionState.Pressed:
                OnPress?.Invoke();
                break;
            case SelectionState.Selected:
                //Debug.Log("Select");
                break;
            case SelectionState.Disabled:
                break;
            default:
                break;
        }
    }
}
