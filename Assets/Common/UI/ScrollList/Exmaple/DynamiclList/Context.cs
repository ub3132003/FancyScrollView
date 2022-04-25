using System;

namespace ListView.Example1
{
    public class UIRectContext : IUIRectContext
    {
        //Action  IUIRectContext.SetLayoutElement { get; set; }
        ScrollDirection IUIRectContext.ScrollDirection { get; set; }
        Func<(float ScrollSize, float ReuseMargin)> IUIRectContext.CalculateScrollSize { get; set; }

 
    }
    public class Context : UIRectContext
    {
        public int SelectedIndex = -1;
        //public Action<int> OnCellClicked;
        public Action<Cell> OnCellClicked;
        public Action OnClickedFadeComplete;

        

    }
}

