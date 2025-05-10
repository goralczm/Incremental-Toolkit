using Core.Utility;
using TMPro;
using UnityEngine;
using Zenject;

namespace Core.UI
{
    public class ProductionDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _productionText;

        [Inject] private Bank _bank;

        private void Start()
        {
            Bank.OnCurrencyChanged += (sender, args) => { UpdateDisplay(_bank.GetTotalProduction()); };

            UpdateDisplay(_bank.GetTotalProduction());
        }

        private void UpdateDisplay(float amount)
        {
            //_productionText.SetText($"Production: {amount.LimitDecimalPoints(2)}/s");
            _productionText.SetText($"{amount.LimitDecimalPoints(2)}/s");
        }
    }
}
