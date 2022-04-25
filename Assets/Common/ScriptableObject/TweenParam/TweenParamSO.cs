using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TweenParamSO : ScriptableObject
{
    TweenParams tweenParams;
    public Ease ease;

    public TweenParams TweenParams { 
        get
        {
            if (tweenParams == null) 
            {
                tweenParams = new TweenParams();
            }
            return tweenParams;
        }  
    }

 
    public virtual void SetParams(Tweener tweener)
    {
        tweener.SetId(tweener.GetHashCode());
    }
}
