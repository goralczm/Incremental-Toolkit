using Core.Utility;
using TMPro;
using UnityEngine;

namespace Core.UI
{
    public class CurrencyDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        private void Awake()
        {
            Bank.OnCurrencyChanged += delegate(object sender, Bank.OnCurrencyChangedEventArgs e)
            {
                _text.SetText(GetDisplayValue(e.Currency));
            };
        }

        public string GetDisplayValue(float value)
        {
            return value.FormatToLowestNumber();
            //return $"Currency: {value.LimitDecimalPoints(2)}";
        }
    }
}