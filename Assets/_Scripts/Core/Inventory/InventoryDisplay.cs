using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Core
{
    public class InventoryDisplay : MonoBehaviour
    {
        [SerializeField] private ItemDisplay _itemDisplay;
        [SerializeField] private Transform _itemDisplayParent;

        [Inject] private Inventory _inventory;

        private readonly List<ItemDisplay> _itemDisplays = new();

        private void Awake()
        {
            _inventory.OnItemsChanged += (items) =>
            {
                for (int i = 0; i < _itemDisplays.Count; i++)
                {
                    _itemDisplays[i].gameObject.SetActive(false);
                }

                for (int i = 0; i < items.Count; i++)
                {
                    if (i >= _itemDisplays.Count)
                        _itemDisplays.Add(Instantiate(_itemDisplay, _itemDisplayParent));

                    _itemDisplays[i].SetItem(items[i]);
                    _itemDisplays[i].gameObject.SetActive(true);
                }
            };
        }
    }
}