using Core.Generators;
using UnityEngine;
using Zenject;

namespace Core.Installers
{
    public class GeneratorsContollerInstaller : MonoInstaller<GeneratorsContollerInstaller>
    {
        [SerializeField] private GeneratorsController _generatorsController;

        public override void InstallBindings()
        {
            Container.Bind<GeneratorsController>().FromInstance(_generatorsController).AsSingle();
        }
    }
}