using UnityEngine;
using Zenject;

namespace Core
{
    public class InventoryInstaller : MonoInstaller<InventoryInstaller>
    {
        [SerializeField] private Inventory _inventory;
        
        public override void InstallBindings()
        {
            Container.Bind<Inventory>().FromInstance(_inventory).AsSingle();
        }
    }
}
