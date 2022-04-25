using System;
using System.Collections.Generic;
using UnityEngine;

namespace pooling
{
    public class ListPooling<T> : List<T> where T : IPooling
    {
        public bool createMoreIfNeeded = true;

        private Transform mParent;
        private Vector3 mStartPos;
        private GameObject referenceObject;
        
        public ListPooling(Func<T> createFunc,int defalutCapacity)
        {
            CreateObject = createFunc;
            
        }

        private Func<T> CreateObject;

        public ListPooling<T> Initialize(GameObject refObject, Transform parent)
        {
            return Initialize(0, refObject, parent);
        }

        public ListPooling<T> Initialize(int amount, GameObject refObject, Transform parent, bool startState = false)
        {
            return Initialize(amount, refObject, parent, Vector3.zero, startState);
        }

        public ListPooling<T> Initialize(int amount, GameObject refObject, Transform parent, Vector3 worldPos, bool startState = false)
        {
            mParent = parent;
            mStartPos = worldPos;
            referenceObject = refObject;

            Clear();

            for (var i = 0; i < amount; i++)
            {
                var obj = CreateObject();

                if (startState) obj.OnCollect();
                else obj.OnRelease();

                Add(obj);
            }

            return this;
        }
        public T Get()
        {
            var obj = Find(x => x.isUsing == false);

            if (obj == null) return obj;
            obj.OnCollect();

            return obj;
        }
 

        public void Release(T obj)
        {
			if(obj != null)
                obj.OnRelease();
        }

        public List<T> GetAllWithState(bool active)
        {
            return FindAll(x => x.isUsing == active);
        }
        //funcCreate
 
    }
}