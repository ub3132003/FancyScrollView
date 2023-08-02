using System;
using System.Collections.Generic;
using UnityEngine;

namespace ListView.Example1
{

    /// <summary>
    /// 动态元素大小列表,均匀元素
    /// </summary>
    class DynamicListView : ListView<ItemData, Context>
    {
        [SerializeField] GameObject cellPrefab = default;
        [SerializeField] float cellSize = 100;
        Action<int> onSelectionChanged;


        public void RegistOnSelectionChanged(Action<int> callback)
        {
            onSelectionChanged += callback;
        }
        public void RegistOnCilck(Action<Cell> callback)
        {
            Context.OnCellClicked += callback;
        }
        protected override GameObject CellPrefab => cellPrefab;


        protected override void Initialize()
        {
            base.Initialize();
            Context.OnCellClicked += RemoveItem;
        }


        public void UpdateData(IList<ItemData> items)
        {
            UpdateContents(items);
        }
    }
}