 

using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ListView.Example1
{

     class Example1 : MonoBehaviour
    {
        [SerializeField] DynamicListView scrollView = default;

        [SerializeField] Text selectedItemInfo = default;
        [SerializeField] Button button;

        public List<ItemData> itemData;
        public int dataCount = 10;
        void Start()
        {
            button.onClick.AddListener(PushData);
            scrollView.RegistOnSelectionChanged(OnSelectionChanged);

            var items = Enumerable.Range(0, dataCount)
                .Select(i => new ItemData($"Cell {i}"))
                .ToList();
            itemData = items;
            PushData();
        }

        void OnSelectionChanged(int index)
        {
            selectedItemInfo.text = $"Selected item info: index {index}";
        }
        public void PushData()
        {
            scrollView.UpdateData(itemData);
        }

    }
}
