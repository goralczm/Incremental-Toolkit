using UnityEngine;

namespace Monetization
{
    public class AdsManager : MonoBehaviour
    {
        public InterstitialAds InterstitialAds;
        public RewardedAds RewardedAds;

        public bool IsPlaying => RewardedAds.IsPlaying;

        private void Awake()
        {
            InterstitialAds.LoadInterstitialAd();
            RewardedAds.LoadRewardedAd();
        }
    }
}
