//using Sirenix.OdinInspector;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Assertions;
//using UnityEngine.UI;

//namespace ListView
//{
//    [RequireComponent(typeof(FlexibleVertocalOrHorizontalLayout))]
//    public abstract class RectListView<TItemData, TContext> : ListView<TItemData, TContext> where TContext : class, IUIRectContext, new()
//    {
//        FlexibleVertocalOrHorizontalLayout _cachedLayout;
//        [InlineEditor]
//        public LayoutElementSO ChildLayoutSO ;
//        private LayoutElement _defaultChildLayout;

//        [SerializeField] protected float reuseCellMarginCount = 0f;

//        #region mothor
//        protected abstract float CellSize { get; }

//        protected FlexibleVertocalOrHorizontalLayout Layout => _cachedLayout ?? (_cachedLayout = GetComponent<FlexibleVertocalOrHorizontalLayout>());
//        protected float ScrollLength => (CellSize + spacing) * ItemsSource.Count;

//        protected float PaddingHeadLength { get; }

//        protected float spacing => Layout.spacing;
//        protected float paddingHead => Layout.padding.top;
//        protected float paddingTail => Layout.padding.bottom;
//        float MaxPosition => ItemsSource.Count * (spacing +_defaultChildLayout.minWidth) + paddingHead + paddingTail;

//        #endregion
//        protected virtual void OnValidate()
//        {
//            //没有考虑布局影响单元
//            if(Layout.ScrollDirection == ScrollDirection.Vertical)
//            {
//                Assert.IsFalse(Layout.childForceExpandHeight);
//                Assert.IsFalse(Layout.childControlHeight);
//            }
//            if(Layout.ScrollDirection == ScrollDirection.Horizontal)
//            {
//                Assert.IsFalse(Layout.childForceExpandWidth);
//                Assert.IsFalse(Layout.childControlWidth);
//            }

//        }
//        protected override void Initialize()
//        {
//            base.Initialize();
            
//            Context.ScrollDirection = Layout.ScrollDirection;
//            //Context.SetLayoutElement = () =>
//            //{

//            //};
//            Context.CalculateScrollSize = () =>
//            {
//                float layoutSize = 0;
//                float reuseMargin = 0;//未实现
//                for (int i = 0; i < pool.Count; i++)
//                {
//                    var interval = CellSize + spacing;
//                    layoutSize += interval;
//                }

//                return (layoutSize,reuseMargin);
//            };
//        }
//        protected override void Relayout()
//        {
//            base.Relayout();
//            //RectTransform rect = GetComponent<RectTransform>();
//            //rect.sizeDelta = _cachedLayout.ScrollDirection == ScrollDirection.Horizontal ?
//            //    new Vector2(ScrollLength, rect.sizeDelta.y) :
//            //    new Vector2(rect.sizeDelta.x, ScrollLength);
                


//        }
//        protected void AdjustCellIntervalAndScrollOffset()
//        {
//            var totalSize = ViewportSize + (CellSize + spacing) * (1f + reuseCellMarginCount * 2f);
//            cellInterval = (CellSize + spacing) / totalSize;
//        }

//        /// <inheritdoc/>
//        protected override void UpdateContents(IList<TItemData> items)
//        {
//            AdjustCellIntervalAndScrollOffset();
//            base.UpdateContents(items);

//            SetTotalCount(items.Count);
            
//        }
//    }

//}