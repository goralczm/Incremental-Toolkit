using TMPro;
using UnityEngine;
using Zenject;

public class ProductionDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _productionText;

    [Inject] private Bank _bank;
    
    private void Start()
    {
        Bank.OnCurrencyChanged += (sender, args) =>
        {
            UpdateDisplay(_bank.GetTotalProduction());
        };
        
        UpdateDisplay(_bank.GetTotalProduction());
    }

    private void UpdateDisplay(float amount)
    {
        _productionText.SetText($"Production: {amount.LimitDecimalPoints(2)}/s");
    }
}
