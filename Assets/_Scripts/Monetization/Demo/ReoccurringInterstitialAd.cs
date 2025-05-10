using Core;
using UnityEngine;
using Zenject;

namespace Monetization.Demo
{
    public class ReoccurringInterstitialAd : MonoBehaviour
    {
        [Inject] private AdsManager _adsManager;

        private void Start()
        {
            GameTick.OnTick += (sender, args) =>
            {
                if (args.Tick % 1800 == 0 && !_adsManager.IsPlaying) // Every 3 minutes
                {
                    _adsManager.InterstitialAds.ShowInterstitialAd();
                }
            };
        }
    }
}
