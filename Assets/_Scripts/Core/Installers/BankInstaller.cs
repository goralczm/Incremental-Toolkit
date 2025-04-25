using UnityEngine;
using Zenject;

namespace Core.Installers
{
    public class BankInstaller : MonoInstaller<BankInstaller>
    {
        [SerializeField] private Bank _bank;

        public override void InstallBindings()
        {
            Container.Bind<Bank>().FromInstance(_bank).AsSingle();
        }
    }
}