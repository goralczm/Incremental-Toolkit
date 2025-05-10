using System;
using UnityEngine;
using Zenject;

namespace Core.Generators
{
    [RequireComponent(typeof(EffectsHandler))]
    public class Generator : MonoBehaviour, IGenerator
    {
        public class OnGeneratorLeveledUpArgs : EventArgs
        {
            public IGenerator Generator;
        }

        public class OnGeneratorChangedStateArgs : EventArgs
        {
            public Generator Generator;
            public bool IsActive;
        }

        [Header("Settings")]
        [SerializeField] private int _tier = 1;
        [SerializeField] private int _level = 1;
        [SerializeField] private int _unlockThreshold = 10;
        [SerializeField] private Color _notEnoughCurrencyColor;
        [SerializeField] private Color _enoughCurrencyColor;

        [Header("Instances")]
        [SerializeField] private EffectsHandler _effectsHandler;

        [Inject] private Bank _bank;


        public static event EventHandler<OnGeneratorLeveledUpArgs> OnGeneratorLeveledUp;
        public static event EventHandler<OnGeneratorChangedStateArgs> OnGeneratorChangedState;

        private void Start()
        {
            OnGeneratorChangedState?.Invoke(this,
                new OnGeneratorChangedStateArgs { Generator = this, IsActive = true });

            Bank.OnPrestige += (sender, args) =>
            {
                ResetGenerator();
            };
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

        private void Update()
        {
            _effectsHandler.Tick(Time.deltaTime);
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
            _bank.AddCurrency(_bank.GetCurrencyPerClick());
        }

        public float GetProduction()
        {
            if (_level == 0) return 0;

            float baseProduction = GetBaseProduction();
            float productionMultiplier = GetProductionMultiplier();

            return baseProduction * Mathf.Pow(productionMultiplier, _level - 1) * GetMultiplier();
        }
        
        public float GetMultiplier()
        {
            return _effectsHandler.GetTotalMultiplier();
        }

        private float GetBaseProduction()
        {
            return Mathf.Max(1, Mathf.CeilToInt(1 * Mathf.Pow(6.5f, _tier - 1)));
        }

        private float GetProductionMultiplier()
        {
            return 1.07f + .15f * (_tier - 1);
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

        public bool GetState() => gameObject.activeSelf;

        public void ResetGenerator()
        {
            _effectsHandler.ClearEffects();

            SetLevel(1);

            if (GetTier() > 1)
            {
                SetLevel(0);
                gameObject.SetActive(false);
            }
        }

        public void SetState(bool state)
        {
            gameObject.SetActive(state);
        }

        public void AddEffect(IMultiplierEffect effect) => _effectsHandler.AddEffect(effect);
    }
}
