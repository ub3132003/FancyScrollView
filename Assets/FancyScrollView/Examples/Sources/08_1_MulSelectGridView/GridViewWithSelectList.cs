using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
namespace FancyScrollView.Example08_1
{

    class GridViewWithSelectList : MonoBehaviour
    {
        [SerializeField]
        GridView _gridView;

        [SerializeField]
        protected List<Cell> ApplyCellList;//todo 限制数量 根据max 
        [SerializeField]
        protected Button SubmitButton;

        protected void Start()
        {

            _gridView.OnRemoveCell += (x) => ApplyCellList.First(cell => cell.Index == x).SetVisible(false);
            _gridView.OnAddCell += (x) =>
            {


                ItemData data = _gridView.GetItemByIndex(x);
                var targetCell = ApplyCellList.First(x => x.IsVisible == false);
                targetCell.SetVisible(true);

                targetCell.Index = x;
                targetCell.UpdateContent(data);
            };
            ApplyCellList.ForEach(x =>
            {
                x.SetContext(new Context());
                x.SetVisible(false);
            });
        }
    }

}