using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace Common.UI
{

    public class ToggleButton : Toggle
    {
        public UnityEvent OnCilcked;

        //protected override void OnEnable()
        //{
        //    base.OnEnable();
        //    image.color = colors.normalColor;
        //}
        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            OnCilcked.Invoke();
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            //DoStateTransition(SelectionState.Pressed, false);
           
        }

        public void SetActive(bool opt)
        {
            gameObject.SetActive(opt);
        }

        //protected override void DoStateTransition(SelectionState state, bool instant)
        //{
        //    base.DoStateTransition(state, instant);

        //    switch (state)
        //    {
        //        case SelectionState.Normal:
        //            image.color = colors.normalColor;
        //            break;
        //        case SelectionState.Highlighted:
        //            break;
        //        case SelectionState.Pressed:
        //            image.color = colors.pressedColor;
        //            break;
        //        case SelectionState.Selected:
        //            image.color = colors.selectedColor;
        //            break;
        //        case SelectionState.Disabled:
        //            break;
        //        default:
        //            break;
        //    }
        //}
 

    }

    
}