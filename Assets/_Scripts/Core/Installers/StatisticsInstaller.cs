using UnityEngine;
using Zenject;

namespace Core
{
    public class StatisticsInstaller : MonoInstaller<StatisticsInstaller>
    {
        [SerializeField] private Statistics _statistics;

        public override void InstallBindings()
        {
            Container.Bind<Statistics>().FromInstance(_statistics).AsSingle();
        }
    }
}
