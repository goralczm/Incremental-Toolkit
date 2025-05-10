using UnityEngine;
using UnityEngine.Advertisements;

namespace Monetization
{
    public class InterstitialAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        [SerializeField] private string _androidUnityId;
        [SerializeField] private string _iosUnityId;

        private string _adUnitId;

        private void Awake()
        {
#if UNITY_IOS
            _adUnitId = _iosUnityId;
#elif UNITY_ANDROID
            _adUnitId = _androidUnityId;
#else
            _adUnitId = _androidUnityId;
#endif
        }

        public void LoadInterstitialAd()
        {
            Advertisement.Load(_adUnitId, this);
        }

        public void ShowInterstitialAd()
        {
            Advertisement.Show(_adUnitId, this);
            LoadInterstitialAd();
        }

        public void OnUnityAdsAdLoaded(string placementId)
        {
            Debug.Log("Interstitial ad loaded successfully.");
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            Debug.Log("Failed to load interstitial ad.");
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            Debug.Log("Failed to show interstitial ad.");
        }

        public void OnUnityAdsShowStart(string placementId)
        {
            Debug.Log("Interstitial ad started to show.");
        }

        public void OnUnityAdsShowClick(string placementId)
        {
            Debug.Log("Interstitial ad clicked.");
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            Debug.Log("Interstitial ad show completed.");
        }
    }
}
