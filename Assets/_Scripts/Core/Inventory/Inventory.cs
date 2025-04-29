using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class Inventory : MonoBehaviour
    {
        public Action<List<IItem>> OnItemsChanged;

        private readonly List<IItem> _items = new();

        public void AddItem(IItem item)
        {
            _items.Add(item);
            OnItemsChanged?.Invoke(_items);
        }

        public void RemoveItem(IItem item)
        {
            _items.Remove(item);
            OnItemsChanged?.Invoke(_items);
        }
    }
}
