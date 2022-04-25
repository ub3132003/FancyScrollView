namespace ListView
{
     
    /// <summary>
    /// Layout class for arranging child elements side by side.
    /// </summary>
    public class HorizontalLayout : FlexibleVertocalOrHorizontalLayout
    {
        protected HorizontalLayout()
        { }
        private ScrollDirection scrollDirection = ScrollDirection.Horizontal;
        public override ScrollDirection ScrollDirection => scrollDirection;

        /// <summary>
        /// Called by the layout system. Also see ILayoutElement
        /// </summary>
        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();
            CalcAlongAxis(0, false);
        }

        /// <summary>
        /// Called by the layout system. Also see ILayoutElement
        /// </summary>
        public override void CalculateLayoutInputVertical()
        {
            CalcAlongAxis(1, false);
        }

        /// <summary>
        /// Called by the layout system. Also see ILayoutElement
        /// </summary>
        public override void SetLayoutHorizontal()
        {
            SetChildrenAlongAxis(0, false);
        }

        /// <summary>
        /// Called by the layout system. Also see ILayoutElement
        /// </summary>
        public override void SetLayoutVertical()
        {
            SetChildrenAlongAxis(1, false);
        }
    }
}
