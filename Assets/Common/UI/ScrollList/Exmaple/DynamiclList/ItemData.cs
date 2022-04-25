using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ListView.Example1
{
    [System.Serializable]
    public class ItemData
    {
        public string Message;

        public ItemData(string message)
        {
            Message = message;
        }
    }
}

