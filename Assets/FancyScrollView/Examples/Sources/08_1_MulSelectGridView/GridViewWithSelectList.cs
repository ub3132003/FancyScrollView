using FancyScrollView.Example08_1;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
namespace FancyScrollView.Example08_1
{

class GridViewWithSelectList : GridView
{

    [SerializeField]
    protected List<Cell> ApplyCellList;//todo 限制数量 根据max 
    [SerializeField]
    protected Button SubmitButton;

    protected override void Initialize()
    {
        base.Initialize();
        OnRemoveCell += (x) => ApplyCellList.First(cell => cell.Index == x).SetVisible(false);
        OnAddCell += (x) =>
        {
            var groupIndex = x / startAxisCellCount;
            ItemData data = ItemsSource[groupIndex][x % startAxisCellCount];
            var targetCell = ApplyCellList.First(x => x.IsVisible == false);
            targetCell.SetVisible(true);

            targetCell.Index = x;
            targetCell.UpdateContent(data);
        };
        ApplyCellList.ForEach(x =>
        {
            x.SetContext(Context);
            x.SetVisible(false);
        });
    }
}

}