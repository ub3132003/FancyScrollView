/*
 * FancyScrollView (https://github.com/setchi/FancyScrollView)
 * Copyright (c) 2020 setchi
 * Licensed under MIT (https://github.com/setchi/FancyScrollView/blob/master/LICENSE)
 */

using UnityEngine;
using UnityEngine.UI;


namespace FancyScrollView.Example08_1
{
    class Cell : FancyGridViewCell<ItemData, Context>
    {
        [SerializeField] Text message = default;
        [SerializeField] Image image = default;
        [SerializeField] Button button = default;

        public override void Initialize()
        {
            button.onClick.AddListener(() => Context.OnCellClicked?.Invoke(Index));
            //button.AddClickActionCondition(() => Context.IsLimitSelect == false);
        }

        public override void UpdateContent(ItemData itemData)
        {
            if(itemData == null)
            {
                SetConetentNull();
                return;
            }
            message.text = itemData.Index.ToString();

            //var selected = Context.SelectedIndex == Index;
            //itemData.IsOn = selected;
            
            //todo checkbox add
            image.color = Context.SelectIndexList.Contains(Index)
                ? new Color32(0, 255, 255, 100)
                : new Color32(255, 255, 255, 77);
        }
        /// <summary>
        /// 清空cell
        /// </summary>
        protected void SetConetentNull()
        {
            message.text = "";
            image.color = Color.white;
        }
        protected override void UpdatePosition(float normalizedPosition, float localPosition)
        {
            base.UpdatePosition(normalizedPosition, localPosition);

            var wave = Mathf.Sin(normalizedPosition * Mathf.PI * 2) * 65;
            transform.localPosition += Vector3.right * wave;
        }
    }
}
