using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Events;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

public class AlphaTweenSO : TweenParamSO
{
    public float EndAlpha;
    public float Duartion;

    [Header("from End value To Start")]
    public bool UseFrom;

//#if ODIN_INSPECTOR
//    [ShowIf("UseFrom")]
//#endif
    //public float fromValue;

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
    public int Fade(Image image, TweenCallback onComplete = null)
    {

        var item = image.DOFade(EndAlpha, Duartion); //transform.DOMove(Vector3.zero, 2); 

        SetParams(item);

        if (onComplete != null)
        {
            item.OnComplete(onComplete);
        }

        return item.intId;      
    }
    public int Fade(CanvasGroup canvasGroup, TweenCallback onComplete = null)
    {

        var item = canvasGroup.DOFade(EndAlpha, Duartion); //transform.DOMove(Vector3.zero, 2); 
        tweenerPool.Add(item);
        item.OnKill(() => tweenerPool.Remove(item));
        SetParams(item);

        if (onComplete != null)
        {
            item.OnComplete(onComplete);
        }
        return item.intId;
    }

    public override void SetParams(Tweener tweener)
    {
        base.SetParams(tweener);
        tweenParams.SetAutoKill(true);
        tweenParams.SetLoops(LoopTimes, loopType);
        tweenParams.SetEase(ease);
        if (UseFrom)
        {// bug 转换 很快时，值错误保存
            tweener.From();
        }

        tweenerPool.Add(tweener);
        tweener.OnKill(() => tweenerPool.Remove(tweener));
 


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
    [Button]
    public void Reflash()
    {
        foreach (var item in tweenerPool)
        {
            SetParams(item);
            item.Restart();
        }
    }
 

}
