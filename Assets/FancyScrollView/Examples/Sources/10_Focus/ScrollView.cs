/*
 * FancyScrollView (https://github.com/setchi/FancyScrollView)
 * Copyright (c) 2020 setchi
 * Licensed under MIT (https://github.com/setchi/FancyScrollView/blob/master/LICENSE)
 */

using System;
using System.Collections.Generic;
using UnityEngine;
using EasingCore;
using System.Linq;
using UnityEngine.EventSystems;
using DG.Tweening;
namespace FancyScrollView.Example10
{
    enum Alignment
    {
        Upper,
        Middle,
        Lower,
    }

    class ScrollView : FancyScrollRect<ItemData, Context>
    {
        [SerializeField] Scroller scroller = default;
        [SerializeField] GameObject cellPrefab = default;
        Action<int> onSelectionChanged;

        [SerializeField] float cellSize=100;
        /// <summary>
        /// 鼠标不在ui上时可以用滚轮移动
        /// </summary>
        [SerializeField] bool outUIWheelable = true;
        [SerializeField] bool WheelInvert = true;
        [SerializeField] bool KeepCenter = false;

        public int DataCount => ItemsSource.Count;
         protected override GameObject CellPrefab => cellPrefab;

        protected override float CellSize => cellSize;

        public float PaddingTop
        {
            get => paddingHead;
            set
            {
                paddingHead = value;
                Relayout();
            }
        }

        public float PaddingBottom
        {
            get => paddingTail;
            set
            {
                paddingTail = value;
                Relayout();
            }
        }

        public float Spacing
        {
            get => spacing;
            set
            {
                spacing = value;
                Relayout();
            }
        }


        private void Update()
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                OnWheelOutUI();
                OnCilckOutUI();
            }
        }
        protected override void Initialize()
        {
            base.Initialize();

            RegistOnCellClicked(RemoveItem);
            
            //scroller.OnValueChanged(UpdatePosition);
            //scroller.OnSelectionChanged(UpdateSelection);
        }

        void UpdateSelection(int index)
        {
            if (Context.SelectedIndex == index)
            {
                return;
            }

            Context.SelectedIndex = index;
            Refresh();

            onSelectionChanged?.Invoke(index);
        }

        public void UpdateData(IList<ItemData> items)
        {
            UpdateContents(items);
 
        }

        public void RegistOnSelectionChanged(Action<int> callback)
        {
            onSelectionChanged += callback;
        }

        public void RegistOnCellClicked(Action<int> callback)
        {
            Context.OnCellClicked += callback;
        }
        public void SelectNextCell()
        {
            SelectCell(Context.SelectedIndex + 1);
        }

        public void SelectPrevCell()
        {
            SelectCell(Context.SelectedIndex - 1);
        }
        float GetAlignment(Alignment alignment)
        {
            switch (alignment)
            {
                case Alignment.Upper: return 0.0f;
                case Alignment.Middle: return 0.5f;
                case Alignment.Lower: return 1.0f;
                default: return GetAlignment(Alignment.Middle);
            }
        }

        public void ScrollTo(int index, float duration, EasingCore.Ease easing, Alignment alignment = Alignment.Middle)
        {
            UpdateSelection(index);
            ScrollTo(index, duration, easing, GetAlignment(alignment));
        }

        public void SelectCell(int index)
        {
            if (index < 0 || index >= ItemsSource.Count || index == Context.SelectedIndex)
            {
                return;
            }

            UpdateSelection(index);
            Debug.Log($"currentPosition {currentPosition } index {index}");
            //var a = cellInterval * (index + 1) + scrollOffset;
            //Debug.Log($"p {a}");
            if (KeepCenter)
            {
                ScrollTo(index, 0.35f, EasingCore.Ease.OutCubic);
            }//未完成
            else
            {
                throw new System.NotImplementedException();

                //if (a >= 1)
                //{
                //    ScrollTo(index, 0.35f, Ease.OutCubic);
                //}
                //else if(a< cellInterval * 2 + scrollOffset)
                //{
                //    ScrollTo(index, 0.35f, Ease.OutCubic);
                //}
               //currentPosition
            }
 
        }
        
        public void OnWheelOutUI()
        {

            if (outUIWheelable)
            {
                var delta = Input.GetAxis("Mouse ScrollWheel");
                delta = WheelInvert ? delta *= -1f : delta; 
                if (delta > 0)
                {
                    SelectNextCell();
                }
                else if(delta < 0)
                {
                    SelectPrevCell();
                }
                
            }
        }

        public void OnCilckOutUI()
        {

            bool leftCilck = Input.GetMouseButtonUp(0);
            if (leftCilck)
            {
                var index = this.Context.SelectedIndex;
                RemoveItem(index);
            }
            
        }

        //没有scroll view 参与的
        public void RemoveItem(int index)
        {
            var min = 0; var max = DataCount - 1;
            if (index < min || index > max)
            {
                return;
            }

            var next = 0;
            if (index == max)
            {
                //SelectPrevCell();
                //ScrollTo(index-1, 0.3f, Ease.InOutQuint, Alignment.Middle);
                next = index - 1;
            }
            else {
                //ScrollTo(index+1, 0.3f, Ease.InOutQuint, Alignment.Middle);
                next =index + 1;
                

                //next 和之后的单元上升

            }
            //UpdateSelection(next);
            //SelectCell(next);
            
            var a = ItemsSource.ToList();
            a.RemoveAt(index);

            DrawnCell(index);
            //Context.OnClickedFadeComplete +=()=> ItemsSource = a;
            //UpdateContents

        }
        /// <summary>
        /// cell 互相靠拢填补空位
        /// </summary>
        /// <param name="dataIndex"></param>
        public void DrawnCell(int dataIndex)
        {
            
            //SelectCell(next);
            var (scrollSize, reuseMargin) = ((IFancyScrollRectContext)Context).CalculateScrollSize();
            var start = 0.5f * scrollSize;
            var end = -start;

            var p = CircularPosition(dataIndex, DataCount) / pool.Count;
            var selectCellId = CircularIndex(dataIndex, pool.Count);
            for (var i = 0; i < pool.Count; i++)
            {
                var cellid = CircularIndex(dataIndex + i, pool.Count);
                var cell = pool[cellid]; //遍历顺序，从选择项开始

                var offset = (cellSize + spacing) / 2;

                Vector3 offsetV3;

                //跳过位置 在上方的和本身
                // 当前cell 在被选择位置的 上方 》0 
                float cellUpDown = cell.transform.localPosition.y - pool[selectCellId].transform.localPosition.y;
                if (cellid == selectCellId) {
                    continue;
                }
                else if (cellUpDown >0)
                {
                    offsetV3 = cell.transform.localPosition;
                    offsetV3.y += -offset;
                    cell.transform.DOLocalMove(offsetV3, 0.3f);

                }
                else
                {
                    var firstPosition = CircularIndex((i + selectCellId), DataCount) * cellInterval;


                    var localPosition = Mathf.Lerp(start, end, firstPosition);
                    var pend = ((IFancyScrollRectContext)Context).ScrollDirection == ScrollDirection.Horizontal
                    ? new Vector2(-localPosition, 0)
                    : new Vector2(0, localPosition);

                    offsetV3 = cell.transform.localPosition;
                    offsetV3.y += offset;
                    cell.transform.DOLocalMove(offsetV3, 0.3f);

                    //cell.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.3f).From(Vector3.one);
                }


                //if (dataIndex < 0 || dataIndex >= ItemsSource.Count)// || position > 1f)
                //{
                //    cell.SetVisible(false);
                //    continue;
                //}

                //if (  cell.Index != dataIndex || !cell.IsVisible)
                //{
                //    cell.Index = dataIndex;
                //    cell.SetVisible(true);
                //    cell.UpdateContent(ItemsSource[dataIndex+i]);
                //}
  
            }

               




        }
        float CircularPosition(float p, int size) => size < 1 ? 0 : p < 0 ? size - 1 + (p + 1) % size : p % size;
    }
}
