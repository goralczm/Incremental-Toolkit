using Unity.Services.Samples;
using UnityEngine;
using UnityEngine.Events;

namespace Community
{
    public class ServicesInitializer : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onInitEvents;

        private async void Start()
        {
            await UnityServiceAuthenticator.SignIn();
            _onInitEvents?.Invoke();
        }
    }
}
