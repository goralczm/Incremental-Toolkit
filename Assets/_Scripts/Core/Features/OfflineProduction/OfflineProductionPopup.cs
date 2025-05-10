using Core.Utility;
using System;
using System.Text;
using TMPro;
using UnityEngine;
using Utilities;

namespace Core.Features.OfflineProduction
{
    public class OfflineProductionPopup : MonoBehaviour
    {
        [SerializeField] private OfflineProduction _offlineProduction;
        [SerializeField] private UITweener _tweener;

        [SerializeField] private TextMeshProUGUI _earnedText;
        [SerializeField] private TextMeshProUGUI _durationText;

        private void Start()
        {
            if (!_offlineProduction.WasOfflineEnough()) return;

            _tweener.Show();

            _earnedText.SetText($"Earned: {_offlineProduction.GetEarnedCurrency().FormatToLowestNumber()}");
            _durationText.SetText($"You were offline for {FormatDuration(_offlineProduction.GetOfflineDuration())}");
        }

        private string FormatDuration(TimeSpan duration)
        {
            StringBuilder output = new StringBuilder();

            if (duration.Hours > 0)
            {
                output.Append($"{duration.Hours}h");

                if (duration.Minutes > 0)
                    output.Append(" ");
            }

            if (duration.Minutes > 0)
                output.Append($"{duration.Minutes}m");

            return output.ToString();
        }
    }
}
