﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace ListView.Example2
{

    /// <summary>
    /// 动态元素大小列表,均匀元素
    /// </summary>
    class StandListView : ListView<ListView.Example1.ItemData,Context>
    {
        [SerializeField] GameObject cellPrefab = default;

        FlexibleVertocalOrHorizontalLayout layout;
 
        public void RegistOnContextCilck(Action<int> callback)
        {
            Context.OnCellClicked += callback;
        }
        protected override GameObject CellPrefab => cellPrefab;


        public void UpdateData(IList<Example1.ItemData> items)
        {
            UpdateContents(items);
        }
    }
}