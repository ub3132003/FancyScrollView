/* FlexibleGridLayout.cs
 * From: Game Dev Guide - Fixing Grid Layouts in Unity With a Flexible Grid Component
 * Created: June 2020, NowWeWake
 */

using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
/// <summary>
/// 外框大小固定，单元大小可变
/// </summary>
public class FlexibleGridLayout : LayoutGroup
{
    public enum FitType
    {
        /// <summary>
        /// 平均
        /// </summary>
        UNIFORM,
        /// <summary>
        /// 按宽度伸缩排布
        /// </summary>
        WIDTH,
        /// <summary>
        /// 按高度伸缩排布
        /// </summary>
        HEIGHT,
        /// <summary>
        /// 按行数平均排布,自动换列
        /// </summary>
        FIXEDROWS,
        /// <summary>
        /// 按列数平均排布,自动换行
        /// </summary>
        FIXEDCOLUMNS,

        NONE
    }

    [Header("Flexible Grid")]
    public FitType fitType = FitType.UNIFORM;
    [Min(1)]
    public int rows;
    [Min(1)]
    public int columns;
    public Vector2 cellSize;
    public Vector2 spacing;

    public bool fitX;
    public bool fitY;

    public bool Anime=false;
    public float AnimeXDuration=1f;
    public float AnimeYDuration=1f;
    public TweenParamSO paramSO;

    protected override void OnEnable()
    {
        
    }
    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        if (fitType == FitType.WIDTH || fitType == FitType.HEIGHT || fitType == FitType.UNIFORM)
        {
            float squareRoot = Mathf.Sqrt(transform.childCount);
            rows = columns = Mathf.CeilToInt(squareRoot);
            switch (fitType)
            {
                case FitType.WIDTH:
                    fitX = true;
                    fitY = false;
                    break;
                case FitType.HEIGHT:
                    fitX = false;
                    fitY = true;
                    break;
                case FitType.UNIFORM:
                    fitX = fitY = true;
                    break;
            }
        }

        if (fitType == FitType.WIDTH || fitType == FitType.FIXEDCOLUMNS)
        {
            rows = Mathf.CeilToInt(transform.childCount / (float)columns);
        }
        if (fitType == FitType.HEIGHT || fitType == FitType.FIXEDROWS)
        {
            columns = Mathf.CeilToInt(transform.childCount / (float)rows);
        }
        if(fitType == FitType.NONE)
        {

        }

        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        float cellWidth = parentWidth / (float)columns - ((spacing.x / (float)columns) * (columns - 1))
            - (padding.left / (float)columns) - (padding.right / (float)columns);
        float cellHeight = parentHeight / (float)rows - ((spacing.y / (float)rows) * (rows - 1))
            - (padding.top / (float)rows) - (padding.bottom / (float)rows); ;

        cellSize.x = fitX ? cellWidth : cellSize.x;
        cellSize.y = fitY ? cellHeight : cellSize.y;

        int columnId = 0;
        int rowId = 0;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            rowId = i / columns;
            columnId = i % columns;

            var item = rectChildren[i];

            var xPos = (cellSize.x * columnId) + (spacing.x * columnId) + padding.left;
            var yPos = (cellSize.y * rowId) + (spacing.y * rowId) + padding.top;

            var xold = rectChildren[i].offsetMin.x;
            var yold = -rectChildren[i].offsetMax.y;
            //Debug.Log($"{i}: x{yold},{ yPos}");
            //Debug.Log($"{i}: x{xold},{ xPos}");


            if (Anime)
            {
                DOTween.To(() => xold, x => SetChildAlongAxis(item, 0, x, cellSize.x), xPos, AnimeXDuration).SetAs(paramSO.TweenParams);
                DOTween.To(() => yold, y => SetChildAlongAxis(item, 1, y, cellSize.y), yPos, AnimeYDuration).SetAs(paramSO.TweenParams);

            }
            else
            {
                SetChildAlongAxis(item, 0, xPos, cellSize.x);
                SetChildAlongAxis(item, 1, yPos, cellSize.y);
            }

        }
    }
 
    public override void CalculateLayoutInputVertical()
    {
        //throw new System.NotImplementedException();
    }

    public override void SetLayoutHorizontal()
    {
        //throw new System.NotImplementedException();
    }

    public override void SetLayoutVertical()
    {
        //throw new System.NotImplementedException();
    }
}