using UnityEngine;
using Utilities.AudioSystem;
using Zenject;

namespace Audio
{
    public class AudioInstaller : MonoInstaller<AudioInstaller>
    {
        [SerializeField] private AudioSystem _audioSystem;

        public override void InstallBindings()
        {
            Container.Bind<AudioSystem>().FromInstance(_audioSystem).AsSingle();
        }
    }
}
