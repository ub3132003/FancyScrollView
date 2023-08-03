/*
 * FancyScrollView (https://github.com/setchi/FancyScrollView)
 * Copyright (c) 2020 setchi
 * Licensed under MIT (https://github.com/setchi/FancyScrollView/blob/master/LICENSE)
 */

using EasingCore;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace FancyScrollView.Example08_1
{
    class GridView : FancyGridView<ItemData, Context>
    {
        class CellGroup : DefaultCellGroup { }

        [SerializeField] Cell cellPrefab = default;

        [Tooltip("可选择的最多选项数量")]
        public int MaxSelectAmount;
        [ShowInInspector]
        public List<int> SelectIndexList => Context.SelectIndexList;
        [Tooltip("达到选择上限时")]
        public UnityEvent<int> OnSelectMax;

        protected override void SetupCellTemplate() => Setup<CellGroup>(cellPrefab);
        /// <summary>
        /// 添加后调用
        /// </summary>
        public Action<int> OnAddCell;
        /// <summary>
        /// 移除前调用
        /// </summary>
        public Action<int> OnRemoveCell;


        protected override void Initialize()
        {
            base.Initialize();

            Context.OnCellClicked += (x) =>
            {
                if (SelectIndexList.Contains(x))
                {
                    OnRemoveCell?.Invoke(x);
                    SelectIndexList.Remove(x);
                    Context.IsLimitSelect = false;
                }
                else
                {
                    if (SelectIndexList.Count < MaxSelectAmount)
                    {
                        SelectIndexList.Add(x);
                        OnAddCell?.Invoke(x);
                    }
                    else
                    {
                        Context.IsLimitSelect = true;
                        OnSelectMax?.Invoke(x);
                    }
                }
                Refresh();
            };
        }
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

        public float SpacingY
        {
            get => spacing;
            set
            {
                spacing = value;
                Relayout();
            }
        }

        public float SpacingX
        {
            get => startAxisSpacing;
            set
            {
                startAxisSpacing = value;
                Relayout();
            }
        }
        /// <summary>
        /// 设置选项
        /// </summary>
        /// <param name="index"></param>
        public void UpdateSelection(int index)
        {
            //允许重复选择
            //if (Context.SelectedIndex == index)
            //{
            //    return;
            //}

            Context.SelectedIndex = index;
            Refresh();
        }

        public void RegisterOnCellClicked(Action<int> callback)
        {
            Context.OnCellClicked += callback;
        }

        public void ScrollTo(int index, float duration, Ease easing, Alignment alignment = Alignment.Middle)
        {
            UpdateSelection(index);
            ScrollTo(index, duration, easing, GetAlignment(alignment));
        }

        public void JumpTo(int index, Alignment alignment = Alignment.Middle)
        {
            UpdateSelection(index);
            JumpTo(index, GetAlignment(alignment));
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
    }
}
