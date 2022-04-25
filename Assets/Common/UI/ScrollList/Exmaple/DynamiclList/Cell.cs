using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

namespace ListView.Example1
{
    //[RequireComponent(typeof(LayoutElement))]
    public class Cell : UICell<ItemData,Context>
    {

        [SerializeField] CanvasGroup cellAlpha;
 
        [SerializeField] TextMeshProUGUI message = default;
        [SerializeField] Image image = default;
        [SerializeField] CustomButton button = default;
        [SerializeField] Toggle toggle = default;
        [SerializeField] CanvasGroup HightLightCanvas;
        [SerializeField] Image focusOutLine;
        [SerializeField] Image focusArrow;

        [SerializeField] AlphaTweenSO focusOutLineTween;
        [SerializeField] AlphaTweenSO imagePressTween;
        [SerializeField] MoveTweenSO arrowMoveTween;
        [SerializeField] MoveTweenSO ToggleMarkMoveTween;
        [SerializeField] AlphaTweenSO ToggleMarkAlphaTween;
        //单元消失动画
        [SerializeField] AlphaTweenSO CellFadeAwayTween;

        private int outlineTweenId;
        private int arrowTweenId;

        public override void Initialize()
        {
            button.onClick.AddListener(() => Context.OnCellClicked?.Invoke(this));
            button.onClick.AddListener(OnCilcked);
            button.OnPress += OnPressCell;
            button.OnNormal += () =>
            {
                HightLightCanvas.alpha = toggle.isOn ? 1 : 0;
                DOTween.Pause(outlineTweenId);
            };

            button.OnHighlight += () => {
                HightLightCanvas.alpha = 1; 
                DOTween.Play(outlineTweenId);
            };

            //Tweener t = focusOutLine.DOFade(0, 1); //transform.DOMove(Vector3.zero, 2); 
            //t.SetLoops(-1, LoopType.Yoyo);
            outlineTweenId = focusOutLineTween.Fade(focusOutLine);
            arrowTweenId = arrowMoveTween.Move(focusArrow.rectTransform);
            DOTween.Pause(outlineTweenId);
            DOTween.Pause(arrowTweenId);
            //button.OnPointerEnter()
        }
        public void OnPressCell()
        {
            //image.DOFade(1f, 1f).From();
            imagePressTween.Fade(image);
            //imagePressTween.Fade()
            //DOTween.ToAlpha(button.colors.pressedColor, 
            //    x=>button.colors.pressedColor=x, 0,1).SetTarget(button.colors.pressedColor);

            HightLightCanvas.alpha = 0;
            ToggleMarkMoveTween.Move(toggle.GetComponent<RectTransform>());
            ToggleMarkAlphaTween.Fade(toggle.graphic.GetComponent<Image>());
        }
        //public void setToggleGroup(ToggleGroup toggleGroup)
        //{
        //    toggle.group = toggleGroup;
        //}
        public override void UpdateContent(ItemData itemData)
        {
            message.text = itemData.Message;

            var selected = Context.SelectedIndex == Index;
            toggle.isOn = selected;
            HightLightCanvas.alpha = selected ? 1 : 0;
            if (toggle.isOn)
            {
                DOTween.Play(outlineTweenId);
                DOTween.Play(arrowTweenId);
            }
            else
            {
                DOTween.Pause(outlineTweenId);
                DOTween.Pause(arrowTweenId);
            }

            cellAlpha.alpha = 1;// cellAlpha.alpha ==0 ? 1 : cellAlpha.alpha;
        }

        //protected override void UpdatePosition(float normalizedPosition, float localPosition)
        //{

        //    //base.UpdatePosition(normalizedPosition, localPosition);



        //}
        public override void SetVisible(bool visible)
        {
            //禁用，出现动画
            if (visible )
            {
                //GetComponent<CanvasGroup>().DOFade(1, 1);
                //base.SetVisible(true);
            }
            else
            {
                //CellFadeAwayTween.Fade(GetComponent<CanvasGroup>(), ()=>base.SetVisible(false));
                //GetComponent<CanvasGroup>().DOFade(0, 1);
                DOTween.Pause(outlineTweenId);
                DOTween.Pause(arrowTweenId);
            }
            base.SetVisible(visible);


        }


        public void OnCilcked()
        {

            var t = GetComponent<CanvasGroup>().DOFade(0.1f, 0.3f);
            t.OnComplete(() => {
                Context.OnClickedFadeComplete?.Invoke();
            });
        }
    }

}

