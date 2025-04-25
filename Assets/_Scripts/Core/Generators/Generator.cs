using System;
using UnityEngine;
using Zenject;

namespace Core.Generators
{
    public class Generator : MonoBehaviour
    {
        public class OnGeneratorLeveledUpArgs : EventArgs
        {
            public Generator Generator;
        }

        public class OnGeneratorChangedStateArgs : EventArgs
        {
            public Generator Generator;
            public bool IsActive;
        }

        [Header("Settings")] [SerializeField] private int _tier = 1;
        [SerializeField] private int _level = 1;
        [SerializeField] private int _unlockThreshold = 10;
        [SerializeField] private Color _notEnoughCurrencyColor;
        [SerializeField] private Color _enoughCurrencyColor;
        [SerializeField] private float _multiplier = 1;

        [Inject] private Bank _bank;
        [SerializeField] private SpriteRenderer _rend;

        public static event EventHandler<OnGeneratorLeveledUpArgs> OnGeneratorLeveledUp;
        public static event EventHandler<OnGeneratorChangedStateArgs> OnGeneratorChangedState;

        private void Start()
        {
            Bank.OnCurrencyChanged += (sender, args) =>
            {
                if (args.Currency >= GetCost())
                    _rend.color = _enoughCurrencyColor;
                else
                    _rend.color = _notEnoughCurrencyColor;
            };

            _rend.color = _notEnoughCurrencyColor;
        }

        private void OnEnable()
        {
            OnGeneratorChangedState?.Invoke(this,
                new OnGeneratorChangedStateArgs { Generator = this, IsActive = true });
        }

        private void OnDisable()
        {
            OnGeneratorChangedState?.Invoke(this,
                new OnGeneratorChangedStateArgs { Generator = this, IsActive = false });
        }

        public void AddLevel() => _level++;

        public bool BuyUpgrade()
        {
            int cost = GetCost();
            if (_bank.HasEnough(cost))
            {
                AddLevel();
                _bank.RemoveCurrency(cost);
                OnGeneratorLeveledUp?.Invoke(this, new OnGeneratorLeveledUpArgs { Generator = this });
                return true;
            }

            return false;
        }

        public void ProduceCurrency()
        {
            _bank.AddCurrency(_bank.ApplyMultiplier(GetProduction()));
        }

        public float GetProduction()
        {
            float baseProduction = Mathf.Max(1, Mathf.CeilToInt(1 * Mathf.Pow(6.5f, _tier - 1)));
            float productionMultiplier = 1.07f + .15f * (_tier - 1);

            return baseProduction * Mathf.Pow(productionMultiplier, _level - 1) * _multiplier;
        }

        public int GetCost()
        {
            float baseCost = Mathf.Pow(10, _tier) + _tier * 2f;
            float costMultiplier = 1.15f + .26f * (_tier - 1);

            return Mathf.CeilToInt(baseCost * Mathf.Pow(costMultiplier, _level));
        }

        public int GetPreviousTierGeneratorsThreshold() => _unlockThreshold;

        private void OnMouseDown()
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    while (BuyUpgrade()) ;
                }
                else
                    BuyUpgrade();
            }
            else
                ProduceCurrency();
        }

        public int GetLevel() => _level;

        public void SetLevel(int level) => _level = level;

        public int GetTier() => _tier;

        public void AddMultiplier(float amount) => _multiplier += amount;

        public void RemoveMultiplier(float amount) => _multiplier -= amount;
    }
}
