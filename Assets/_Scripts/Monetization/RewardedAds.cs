using UnityEngine;
using UnityEngine.Advertisements;

namespace Monetization
{
    public class RewardedAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        public bool IsPlaying { get; private set; }

        [SerializeField] private string _androidUnityId;
        [SerializeField] private string _iosUnityId;

        private string _adUnitId;

        public delegate void RewardedAdEvent();
        public event RewardedAdEvent OnUserRewarded;

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

        public void LoadRewardedAd()
        {
            Advertisement.Load(_adUnitId, this);
        }

        public void ShowRewardedAd()
        {
            Advertisement.Show(_adUnitId, this);
            LoadRewardedAd();
        }

        public void OnUnityAdsAdLoaded(string placementId)
        {
            Debug.Log("Rewarded ad loaded successfully.");
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            Debug.Log("Failed to load Rewarded ad.");
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            Debug.Log("Failed to show Rewarded ad.");
        }

        public void OnUnityAdsShowStart(string placementId)
        {
            Debug.Log("Rewarded ad started to show.");
            IsPlaying = true;
        }

        public void OnUnityAdsShowClick(string placementId)
        {
            Debug.Log("Rewarded ad clicked.");
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            Debug.Log("Rewarded ad show completed.");
            if (placementId == _adUnitId && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
            {
                Debug.Log("User rewarded for watching the ad.");
                OnUserRewarded?.Invoke();
            }
            IsPlaying = false;
        }
    }
}
