using System.Collections;
using UnityEngine;

namespace ListView
{


    public class VerticalLayout : FlexibleVertocalOrHorizontalLayout
    {
         private ScrollDirection scrollDirection = ScrollDirection.Vertical;
        protected VerticalLayout()
        { }

        public override ScrollDirection ScrollDirection => scrollDirection;

        /// <summary>
        /// Called by the layout system. Also see I
        /// 
        /// </summary>
        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();
            CalcAlongAxis(0, true);
        }

        /// <summary>
        /// Called by the layout system. Also see ILayoutElement
        /// </summary>
        public override void CalculateLayoutInputVertical()
        {
            CalcAlongAxis(1, true);
        }

        /// <summary>
        /// Called by the layout system. Also see ILayoutElement
        /// </summary>
        public override void SetLayoutHorizontal()
        {
            SetChildrenAlongAxis(0, true);
        }

        /// <summary>
        /// Called by the layout system. Also see ILayoutElement
        /// </summary>
        public override void SetLayoutVertical()
        {
            SetChildrenAlongAxis(1, true);
        }
    }
}