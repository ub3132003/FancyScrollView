 

using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ListView.Example2
{

     class Example2 : MonoBehaviour
    {
        [SerializeField] StandListView scrollView = default;
        [SerializeField] Button Trigger;



        public List<Example1.ItemData> itemData;
        public int dataCount = 10;
        void Start()
        {

            scrollView.RegistOnContextCilck(OnCilckItem);
            Trigger.onClick.AddListener(PushData);
            var items = Enumerable.Range(0, dataCount)
            .Select(i => new Example1.ItemData($"Cell {i}"))
            .ToList();
            itemData = items;
            PushData();
 
        }

        void OnCilckItem(int index)
        {
            scrollView.ReleaseAll();
        }

        public void PushData()
        {
            scrollView.UpdateData(itemData);
        }
 
    }
}
