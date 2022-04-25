/*
 * FancyScrollView (https://github.com/setchi/FancyScrollView)
 * Copyright (c) 2020 setchi
 * Licensed under MIT (https://github.com/setchi/FancyScrollView/blob/master/LICENSE)
 */

using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace FancyScrollView.Example10
{
    [RequireComponent(typeof(CanvasGroup))]
    class Cell : FancyScrollRectCell<ItemData, Context>
    {
        [SerializeField] CanvasGroup cellAlpha;
        [SerializeField] Animator animator = default;
        [SerializeField] Text message = default;
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
        static class AnimatorHash
        {
            public static readonly int Scroll = Animator.StringToHash("scroll");
        }

        public override void Initialize()
        {
            button.onClick.AddListener(() => Context.OnCellClicked?.Invoke(Index));
            button.onClick.AddListener(OnCilcked);
            button.OnPress += OnPressCell;
            button.OnNormal += () => HightLightCanvas.alpha = toggle.isOn? 1:0;
            button.OnHighlight+=() => {
                HightLightCanvas.alpha = 1; DOTween.Play(outlineTweenId);
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
            //image.DOFade(0.6f, 2f).From(1);
            imagePressTween.Fade(image);
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
            //if(visible == false)
            //{
            //    //CellFadeAwayTween.Fade(GetComponent<CanvasGroup>(), ()=>base.SetVisible(false));
            //    GetComponent<CanvasGroup>().DOFade(0, 1);
            //}
            //else
            //{
            //    GetComponent<CanvasGroup>().DOFade(1, 1);
            //    //base.SetVisible(true);
            //}
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
