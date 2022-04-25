/*
 * FancyScrollView (https://github.com/setchi/FancyScrollView)
 * Copyright (c) 2020 setchi
 * Licensed under MIT (https://github.com/setchi/FancyScrollView/blob/master/LICENSE)
 */

using System;
using UnityEngine.UI;
namespace ListView
{
    public enum ScrollDirection
    {
        Horizontal,
        Vertical,
    }

    public interface IUIRectContext 
    {
        ScrollDirection ScrollDirection { get; set; }
        Func<(float ScrollSize, float ReuseMargin)> CalculateScrollSize { get; set; }

        //Action  SetLayoutElement { get; set; }
    }
}
