using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.UI;

namespace ListView.Example2
{
    //[RequireComponent(typeof(LayoutElement))]
    class Cell : UICell<Example1.ItemData, Context>
    {

        [SerializeField] TextMeshProUGUI message = default;
        [SerializeField] Button button;
        public override void Initialize()
        {
            button.onClick.AddListener(() => Context.OnCellClicked?.Invoke(Index));
        }

        public override void UpdateContent(Example1.ItemData itemData)
        {
            message.text = itemData.Message;
        }


        float currentPosition = 0;


    }

}

