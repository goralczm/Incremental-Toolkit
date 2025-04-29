using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core
{
    public class ItemDisplay : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private Image _image;
        
        [Inject] private Inventory _inventory;

        private IItem _item;

        private void Awake()
        {
            // TODO: remove and fix dependency injection
            _inventory = FindFirstObjectByType<Inventory>();
        }

        public void SetItem(IItem item)
        {
            _item = item;
            UpdateVisuals();
        }
        
        public void Use()
        {
            _item.Use();
            _inventory.RemoveItem(_item);
        }

        public void UpdateVisuals()
        {
            _image.sprite = _item.GetIcon();
        }
    }
}
