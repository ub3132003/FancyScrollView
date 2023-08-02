using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;
namespace ListView
{
    [RequireComponent(typeof(LayoutGroup))]
    public abstract class ListView<TItemData, TContext> : MonoBehaviour where TContext : class, new()
    {
        [SerializeField] RectTransform viewport = default;

        /// <summary>
        /// ビューポートのサイズ.
        /// </summary>
        public float ViewportSize => scrollDirection == ScrollDirection.Horizontal
            ? viewport.rect.size.x
            : viewport.rect.size.y;

        [SerializeField] ScrollDirection scrollDirection = ScrollDirection.Vertical;

        [SerializeField, Range(1e-2f, 1f)] protected float cellInterval = 0.2f;

        [SerializeField] protected bool loop = false;

        [SerializeField] protected RectTransform cellContainer = default;

        protected bool initialized;

        protected float currentPosition;

        int totalCount;


        ObjectPool<UICell<TItemData, TContext>> pool;
        HashSet<UICell<TItemData, TContext>> inactiveCell = new HashSet<UICell<TItemData, TContext>>();

        #region mother

        protected IList<TItemData> ItemsSource { get; set; } = new List<TItemData>();
        protected abstract GameObject CellPrefab { get; }
        protected TContext Context { get; } = new TContext();
        protected virtual float CellInterval { get => cellInterval; set => cellInterval = value; }


        #endregion
        public void SetTotalCount(int totalCount) => this.totalCount = totalCount;
        protected virtual void Initialize()
        {
            pool = new ObjectPool<UICell<TItemData, TContext>>(CreatePoolItem,
                null,
               OnCellRelease
                );


        }
        protected virtual void OnCellGet(UICell<TItemData, TContext> cell)
        {
            cell.SetVisible(true);
            cell.transform.SetAsFirstSibling();
        }
        protected virtual void OnCellRelease(UICell<TItemData, TContext> cell)
        {
            cell.SetVisible(false);
            cell.transform.SetSiblingIndex(pool.CountActive - 1);
        }
        public virtual UICell<TItemData, TContext> CreatePoolItem() => CreateItem();
        protected virtual void Refresh() => UpdatePosition(currentPosition, true);
        protected virtual void Relayout() => UpdatePosition(currentPosition, false);
        protected virtual void UpdatePosition(float position) => UpdatePosition(position, false);
        protected virtual void UpdateContents(IList<TItemData> itemsSource)
        {
            ItemsSource = itemsSource;

            Refresh();
        }

        protected virtual void AddItem(TItemData itemData)
        {
            //有

            //不足时
        }
        protected virtual void RemoveItem(int index)
        {

            //pool.Release(inactiveCell[index]);
            //inactiveCell.Remove(inactiveCell[index]);
            //var currentNode = inactiveCell.First;

            //while (currentNode != null)
            //{
            //    if (currentNode.Value.Index == index)
            //    {
            //        var toRemove = currentNode;
            //        currentNode = currentNode.Next;
            //        inactiveCell.Remove(toRemove);
            //    }
            //    else
            //    {
            //        currentNode = currentNode.Next;
            //    }
            //}

        }
        protected virtual void RemoveItem(UICell<TItemData, TContext> cell)
        {
            pool.Release(cell);
            inactiveCell.Remove(cell);

        }
        void UpdatePosition(float position, bool forceRefresh)
        {
            if (!initialized)
            {
                Initialize();
                initialized = true;
            }

            UpdateCells(forceRefresh);
        }

        private UICell<TItemData, TContext> CreateItem()
        {
            Debug.Assert(CellPrefab != null);
            Debug.Assert(cellContainer != null);

            var cell = Instantiate(CellPrefab, cellContainer).GetComponent<UICell<TItemData, TContext>>();
            if (cell == null)
            {
                throw new MissingComponentException(string.Format(
                    "FancyCell<{0}, {1}> component not found in {2}.",
                    typeof(TItemData).FullName, typeof(TContext).FullName, CellPrefab.name));
            }
            var totol = pool.CountAll;
            cell.SetContext(Context);
            cell.Initialize();
            cell.name = $"cell {totol + 1}";
            cell.Index = totol;
            return cell;
        }
        void UpdateCells(bool forceRefresh)
        {
            var length = ItemsSource.Count;
            for (int i = 0; i < length; i++)
            {

                var cell = pool.Get();
                cell.SetVisible(true);
                cell.UpdateContent(ItemsSource[i]);
                inactiveCell.Add(cell);

            }
        }

        public void ReleaseAll()
        {
            var length = inactiveCell.Count;
            //for (int i = 0; i < length; i++)
            //{
            //    //pool.Release(inactiveCell[length-1-i]);
            //    //inactiveCell.RemoveAt(length-1-i);
            //}
            foreach (var item in inactiveCell)
            {
                pool.Release(item);
                inactiveCell.Remove(item);
            }

        }

        protected int CircularIndex(int i, int size) => size < 1 ? 0 :
            i < 0 ? size - 1 + (i + 1) % size : i % size;
    }

    public abstract class ListView<TItemData> : ListView<TItemData, NullContext> { }
}