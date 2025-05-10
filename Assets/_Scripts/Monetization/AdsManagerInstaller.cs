using UnityEngine;
using Zenject;

namespace Monetization
{
    public class AdsManagerInstaller : MonoInstaller<AdsManagerInstaller>
    {
        [SerializeField] private AdsManager _adsManager;

        public override void InstallBindings()
        {
            Container.Bind<AdsManager>().FromInstance(_adsManager).AsSingle();
        }
    }
}
