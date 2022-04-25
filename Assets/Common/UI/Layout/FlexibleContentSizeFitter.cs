using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

//[AddComponentMenu("Layout/Content Size Fitter", 141)]
[ExecuteAlways]
[RequireComponent(typeof(RectTransform))]
/// <summary>
/// Resizes a RectTransform to fit the size of its content.
/// </summary>
/// <remarks>
/// The ContentSizeFitter can be used on GameObjects that have one or more ILayoutElement components, such as Text, Image, HorizontalLayoutGroup, VerticalLayoutGroup, and GridLayoutGroup.
/// </remarks>
public class FlexibleContentSizeFitter : UIBehaviour, ILayoutSelfController, IAnime
{
    /// <summary>
    /// The size fit modes avaliable to use.
    /// </summary>
    public enum FitMode
    {
        /// <summary>
        /// Don't perform any resizing.
        /// </summary>
        Unconstrained,
        /// <summary>
        /// Resize to the minimum size of the content.
        /// </summary>
        MinSize,
        /// <summary>
        /// Resize to the preferred size of the content.
        /// </summary>
        PreferredSize
    }

    [SerializeField] protected FitMode m_HorizontalFit = FitMode.Unconstrained;

    /// <summary>
    /// The fit mode to use to determine the width.
    /// </summary>
    public FitMode horizontalFit { get { return m_HorizontalFit; } set { if (SetPropertyUtility.SetStruct(ref m_HorizontalFit, value)) SetDirty(); } }

    [SerializeField] protected FitMode m_VerticalFit = FitMode.Unconstrained;

    /// <summary>
    /// The fit mode to use to determine the height.
    /// </summary>
    public FitMode verticalFit { get { return m_VerticalFit; } set { if (SetPropertyUtility.SetStruct(ref m_VerticalFit, value)) SetDirty(); } }

    [System.NonSerialized] private RectTransform m_Rect;
    private RectTransform rectTransform
    {
        get
        {
            if (m_Rect == null)
                m_Rect = GetComponent<RectTransform>();
            return m_Rect;
        }
    }

    public bool Anime { get => anime; set => anime = value; }
    public float AnimeXDuration { get => animeXDuration; set => animeXDuration = value; }
    public float AnimeYDuration { get => animeYDuration; set => animeYDuration = value; }
    public TweenParamSO ParamSO { get => paramSO; set => paramSO = value; }

    [SerializeField] private bool anime = false;
    [SerializeField] private float animeXDuration = 1f;
    [SerializeField] private float animeYDuration = 1f;
    [SerializeField] private TweenParamSO paramSO;
    private bool isAniming = false;
    private Tweener[] cacheTweenerArray = new Tweener[2];
    private DrivenRectTransformTracker m_Tracker;

    protected FlexibleContentSizeFitter()
    { }

    protected override void OnEnable()
    {
        base.OnEnable();
        SetDirty();
    }

    protected override void OnDisable()
    {
        m_Tracker.Clear();
        LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
        base.OnDisable();
    }

    protected override void OnRectTransformDimensionsChange()
    {
        SetDirty();
    }

    private void HandleSelfFittingAlongAxis(int axis)
    {
        FitMode fitting = (axis == 0 ? horizontalFit : verticalFit);
        if (fitting == FitMode.Unconstrained)
        {
            // Keep a reference to the tracked transform, but don't control its properties:
            m_Tracker.Add(this, rectTransform, DrivenTransformProperties.None);
            return;
        }

        m_Tracker.Add(this, rectTransform, (axis == 0 ? DrivenTransformProperties.SizeDeltaX : DrivenTransformProperties.SizeDeltaY));

        Tweener cacheTweener = cacheTweenerArray[axis];
        if (Anime)
        {
            if ((cacheTweener == null || !cacheTweener.IsActive() || !cacheTweener.IsPlaying()))
            {

                //优先完成此动画，
                var layout = GetComponent<LayoutGroup>();
                if (layout != null)
                {
                    layout.enabled = false;
                }
                isAniming = true;
                float duration = axis == 0 ? AnimeXDuration : AnimeYDuration;
                // Set size to min or preferred size
                if (fitting == FitMode.MinSize)
                {

                    var size = m_Rect.sizeDelta;

                    cacheTweener = DOTween.To(() => size[axis], x => { size[axis] = x; m_Rect.sizeDelta = size; },
                        LayoutUtility.GetMinSize(m_Rect, axis), duration);
                    cacheTweener.OnComplete(() => layout.enabled = true);
                    if (paramSO != null) cacheTweener.SetAs(paramSO.TweenParams);
                }

                else
                {

                    var size = m_Rect.sizeDelta;

                    //DOTween.To(() => oldSzie, x => rectTransform.SetSizeWithCurrentAnchors((RectTransform.Axis)axis, x), LayoutUtility.GetPreferredSize(m_Rect, axis), 1f);
                    //if (size[axis] - LayoutUtility.GetPreferredSize(m_Rect, axis) < 0.1f) return;
                    cacheTweener = DOTween.To(() => size[axis], x => { size[axis] = x; m_Rect.sizeDelta = size; },
                            LayoutUtility.GetPreferredSize(m_Rect, axis), duration); //无法停止动画 
                    cacheTweener.OnComplete(() => layout.enabled = true);
                    if (paramSO != null) cacheTweener.SetAs(paramSO.TweenParams);
                }
                cacheTweenerArray[axis] = cacheTweener;
            }

        }
        else
        {
            isAniming = false;
            // Set size to min or preferred size
            if (fitting == FitMode.MinSize)
                rectTransform.SetSizeWithCurrentAnchors((RectTransform.Axis)axis, LayoutUtility.GetMinSize(m_Rect, axis));
            else
                rectTransform.SetSizeWithCurrentAnchors((RectTransform.Axis)axis, LayoutUtility.GetPreferredSize(m_Rect, axis));
        }

    }

    /// <summary>
    /// Calculate and apply the horizontal component of the size to the RectTransform
    /// </summary>
    public virtual void SetLayoutHorizontal()
    {
        m_Tracker.Clear();
        HandleSelfFittingAlongAxis(0);
    }

    /// <summary>
    /// Calculate and apply the vertical component of the size to the RectTransform
    /// </summary>
    public virtual void SetLayoutVertical()
    {
        HandleSelfFittingAlongAxis(1);
    }

    protected void SetDirty()
    {
        if (!IsActive())
            return;

        LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
    }

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        SetDirty();
    }

#endif
}

