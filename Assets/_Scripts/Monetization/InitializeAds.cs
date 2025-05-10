using UnityEngine;
using UnityEngine.Advertisements;

namespace Monetization
{
    public class InitializeAds : MonoBehaviour, IUnityAdsInitializationListener
    {
        [SerializeField] private string _androidGameId;
        [SerializeField] private string _iosGameId;
        [SerializeField] private bool _isTesting;

        private string _gameId;

        private void Awake()
        {
        #if UNITY_IOS
            _gameId = _iosGameId;
        #elif UNITY_ANDROID
            _gameId = _androidGameId;
        #else
            _gameId = _androidGameId;

            if (!Advertisement.isInitialized && Advertisement.isSupported)
                Advertisement.Initialize(_gameId, _isTesting, this);
        }
        #endif

        public void OnInitializationComplete()
        {

        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {

        }
    }
}
