/*
 * FancyScrollView (https://github.com/setchi/FancyScrollView)
 * Copyright (c) 2020 setchi
 * Licensed under MIT (https://github.com/setchi/FancyScrollView/blob/master/LICENSE)
 */

using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace FancyScrollView.Example10
{

    class Example10 : MonoBehaviour
    {
        [SerializeField] ScrollView scrollView = default;
 
        [SerializeField] Text selectedItemInfo = default;

 
        public List<ItemData> itemData;
        public int dataCount=10;
        void Start()
        {
 
            scrollView.RegistOnSelectionChanged(OnSelectionChanged);

            var items = Enumerable.Range(0, dataCount)
                .Select(i => new  ItemData($"Cell {i}"))
                .ToArray();

            scrollView.UpdateData(items);
            scrollView.SelectCell(0);
        }

        void OnSelectionChanged(int index)
        {
            selectedItemInfo.text = $"Selected item info: index {index}";
        }

        [Button]
        public void UpdateItem()
        {
            scrollView.UpdateData(itemData);
        }
    }
}
