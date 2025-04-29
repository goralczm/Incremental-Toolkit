using Core.Utility;
using TMPro;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;

namespace Core.UI
{
    public class CurrencyDisplay : DynamicValueText
    {
        private void Awake()
        {
            Bank.OnCurrencyChanged += delegate(object sender, Bank.OnCurrencyChangedEventArgs e)
            {
                SetValue(e.Currency);
            };
        }

        public override string GetDisplayValue(string value)
    {
            return $"Currency: {value}";
        }
    }
}