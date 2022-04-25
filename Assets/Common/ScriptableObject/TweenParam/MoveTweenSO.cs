using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

public class MoveTweenSO : TweenParamSO
{
    public Vector3 EndPosition;
    public float Duartion;
    public bool BackOnComplete=false;

    [Header("from")]
    public bool UseFrom;

#if ODIN_INSPECTOR
    [ShowIf("UseFrom")]
#endif
    public float fromValue;

    [Header("Ease")]
    public Ease ease;
    
    [Header("Loop")]
    /// <summary>
    /// -1 无线循环,>0 循环次数
    /// </summary>
    public int LoopTimes = 1;
    public LoopType loopType=LoopType.Restart;





    private List<Tweener> tweenerPool=new List<Tweener>();
 
    private TweenParams tweenParams = new TweenParams();
    public int Move(RectTransform obj)
    {

        var item = obj.DOAnchorPos(EndPosition, Duartion);
        tweenerPool.Add(item);
        item.OnKill(() => tweenerPool.Remove(item));
        SetParams(item);

        if(BackOnComplete)
            item.OnComplete(() => obj.anchoredPosition = item.startValue  );
        return item.intId;      
    }
    public int Move(Transform obj)
    {

        var item = obj.DOMove(EndPosition, Duartion);
        tweenerPool.Add(item);
        item.OnKill(() => tweenerPool.Remove(item));
        SetParams(item);

        if (BackOnComplete)
            item.OnComplete(() => obj.transform.position = item.startValue);
        return item.intId;
    }

    public override void SetParams(Tweener tweener)
    {
        base.SetParams(tweener);
        tweenParams.SetAutoKill(true);
        tweenParams.SetLoops(LoopTimes, loopType);
        tweenParams.SetEase(ease);

        if (UseFrom)
        {
            tweener.From();
        }

        tweener.SetAs(tweenParams);
    }
    public async void Clear()
    {
        foreach (var item in tweenerPool)
        {
            item.Kill();
            await item.AsyncWaitForKill();
        }
        
    }
 
 

}
