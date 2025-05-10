using Monetization;
using System;
using UnityEngine;
using Zenject;

namespace Core.Features.OfflineProduction
{
    public class OfflineProduction : MonoBehaviour
    {
        [Inject] private Bank _bank;
        [Inject] private AdsManager _adsManager;

        private float _earned;
        private TimeSpan _offlineDuration;

        private void Start()
        {
            SimulateOfflineProduction();
        }

        private void OnApplicationQuit()
        {
            if (_earned == 0)
            {
                PlayerPrefs.SetString("LastOnline", DateTime.Now.ToBinary().ToString());
                PlayerPrefs.Save();
            }
        }

        private void SimulateOfflineProduction()
        {
            if (PlayerPrefs.HasKey("LastOnline"))
            {
                long temp = Convert.ToInt64(PlayerPrefs.GetString("LastOnline"));
                DateTime lastOnline = DateTime.FromBinary(temp);
                _offlineDuration = DateTime.Now - lastOnline;

                _earned = GetCurrencyBasedOnOfflineTime(_offlineDuration);
            }
        }

        private float GetCurrencyBasedOnOfflineTime(TimeSpan duration)
        {
            float secondsOffline = (float)duration.TotalSeconds;
            float productionRate = _bank.GetTotalProduction();

            secondsOffline = Mathf.Clamp(secondsOffline, 0, 60 * 60 * 2); // Max 2 hours

            float earned = secondsOffline * productionRate;

            return earned;
        }

        public void ApplyOfflineEarnings()
        {
            _bank.AddCurrency(_earned);
            _earned = 0;
        }

        public void ShowRewardedAdToApplyDoubleOfflineEarnings()
        {
            _adsManager.RewardedAds.OnUserRewarded += OnRewardedAdWatched;
            _adsManager.RewardedAds.ShowRewardedAd();
        }

        private void OnRewardedAdWatched()
        {
            _adsManager.RewardedAds.OnUserRewarded -= OnRewardedAdWatched;
            _bank.AddCurrency(_earned * 2);
            _earned = 0;
        }

        public float GetEarnedCurrency() => _earned;

        public TimeSpan GetOfflineDuration() => _offlineDuration;

        public bool WasOfflineEnough()
        {
            return _offlineDuration.TotalSeconds >= 60;
        }
    }
}
