using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ListView
{

    
    public abstract class UICell<TItemData, TContext> : MonoBehaviour where TContext : class, new()
    {
        /// <summary>

        /// context 数据的index
        /// </summary>
        public int Index { get; set; } = -1;


        public virtual bool IsVisible => gameObject.activeSelf;


        protected TContext Context { get; private set; }
 

        public virtual void SetContext(TContext context) => Context = context;

        public virtual void Initialize() { }


        public virtual void SetVisible(bool visible) => gameObject.SetActive(visible);


        public abstract void UpdateContent(TItemData itemData);

    }
    public sealed class NullContext { }
    public abstract class UICell<TItemData> : UICell<TItemData, NullContext>
    {
        /// <inheritdoc/>
        public sealed override void SetContext(NullContext context) => base.SetContext(context);
    }
}