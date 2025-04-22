using TMPro;
using UnityEngine;

public class CurrencyDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currencyText;

    private void Awake()
    {
        Bank.OnCurrencyChanged += delegate(object sender, Bank.OnCurrencyChangedEventArgs e)
        {
            UpdateDisplay(e.Currency);
        };
    }

    private void UpdateDisplay(float amount)
    {
        _currencyText.SetText($"Currency: {amount.LimitDecimalPoints(2)}");
    }
}
