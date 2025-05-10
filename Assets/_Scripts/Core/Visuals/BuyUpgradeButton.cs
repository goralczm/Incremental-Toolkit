using Core.Visuals;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class BuyUpgradeButton : MonoBehaviour
    {
        [SerializeField] private GeneratorView _generatorView;
        [SerializeField] private Button _buyUpgradeButton;

        private float _lastCurrency;

        private void Awake()
        {
            Bank.OnCurrencyChanged += (sender, args) =>
            {
                _buyUpgradeButton.interactable = args.Currency >= _generatorView.GetCurrentGeneratorInView().GetCost();
                _lastCurrency = args.Currency;
            };

            _generatorView.OnViewChanged += (generator) =>
            {
                _buyUpgradeButton.interactable = _lastCurrency >= generator.GetCost();
            };
        }

        public void BuyUpgrade()
        {
            _generatorView.GetCurrentGeneratorInView().BuyUpgrade();
        }
    }
}
