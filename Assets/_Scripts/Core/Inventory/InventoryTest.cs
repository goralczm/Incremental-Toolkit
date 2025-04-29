using UnityEngine;
using Zenject;

namespace Core
{
    public class InventoryTest : MonoBehaviour
    {
        [SerializeField] private ConsumableDefinition _consumable;
        
        [Inject] private Inventory _inventory;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                _inventory.AddItem(_consumable);
        }
    }
}
