using System;
using System.Collections.Generic;
using Core.Generators;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Core
{
    public class Bank : MonoBehaviour
    {
        public class OnCurrencyChangedEventArgs : EventArgs
        {
            public float Currency;
        }

        public class OnPrestigePointsChangedEventArgs : EventArgs
        {
            public int PrestigePoints;
        }

        public class OnAvailablePrestigePointsChangedEventArgs : EventArgs
        {
            public int AvailablePrestigePoints;
        }

        public const float KAPPA = .005f;
        public const float BETA = .4f;

        [SerializeField] private float _currency;
        [SerializeField] private int _prestigePoints;

        [Inject] private GeneratorsController _generatorsController;
        [Inject] private Statistics _statistics;

        private EffectsHandler _effectsHandler = new();

        public static event EventHandler<OnCurrencyChangedEventArgs> OnCurrencyChanged;
        public static event EventHandler<OnPrestigePointsChangedEventArgs> OnPrestigePointsChanged;
        public static event EventHandler<OnAvailablePrestigePointsChangedEventArgs> OnAvailablePrestigePointsChanged;
        public static event EventHandler OnPrestige;
        public static event Action<float> OnCurrencyEarned;

        private void Awake()
        {
            GameTick.OnTick += delegate(object sender, GameTick.OnTickEventArgs e)
            {
                if (e.tick % (int)(1f / GameTick.TICK_INTERVAL) == 0) ProduceTotalCurrency();
            };

            OnCurrencyChanged += (sender, args) =>
            {
                int availablePrestigePoints = CalculatePrestigePoints();

                if (availablePrestigePoints > 0)
                {
                    OnAvailablePrestigePointsChanged?.Invoke(this, new OnAvailablePrestigePointsChangedEventArgs
                    {
                        AvailablePrestigePoints = availablePrestigePoints
                    });
                }
            };
        }

        private void Update()
        {
            _effectsHandler.Update(Time.deltaTime);
        }

        public float GetCurrency() => _currency;

        public void SetCurrency(float currency)
        {
            _currency = currency;
            OnCurrencyChanged?.Invoke(this, new OnCurrencyChangedEventArgs() { Currency = GetCurrency() });
        }

        public bool HasEnough(float amount) => GetCurrency() >= amount;

        public void RemoveCurrency(float amount)
        {
            SetCurrency(GetCurrency() - amount);
        }

        public void AddCurrency(float amount)
        {
            SetCurrency(GetCurrency() + amount);

            OnCurrencyEarned?.Invoke(amount);
        }

        public float GetCurrencyPerClick()
        {
            return _generatorsController.GetGeneratorByTier(1).GetProduction();
        }

        public float GetTotalProduction()
        {
            return ApplyMultiplier(_generatorsController.GetActiveGeneratorsProduction());
        }

        public float ApplyMultiplier(float currency)
        {
            return currency * GetPrestigeMultiplier() * _effectsHandler.GetTotalMultiplier();
        }

        public float GetPrestigeMultiplier()
        {
            return Mathf.Pow(1 + .02f, GetPrestigePoints());
        }

        public void ProduceTotalCurrency()
        {
            AddCurrency(GetTotalProduction());
        }

        public int GetPrestigePoints() => _prestigePoints;

        public int CalculatePrestigePoints()
        {
            return Mathf.FloorToInt(KAPPA * Mathf.Pow(_statistics.GetTotalEarned(), BETA));
        }

        public void SetPrestigePoints(int prestigePoints)
        {
            _prestigePoints = prestigePoints;
            OnPrestigePointsChanged?.Invoke(this, new OnPrestigePointsChangedEventArgs { PrestigePoints = GetPrestigePoints() });
        }

        public void Prestige()
        {
            _prestigePoints += CalculatePrestigePoints();
            OnPrestige?.Invoke(this, EventArgs.Empty);
            OnPrestigePointsChanged?.Invoke(this, new OnPrestigePointsChangedEventArgs { PrestigePoints = GetPrestigePoints() });
            ClearEffects();
            SetCurrency(0);
        }

        public void ClearEffects()
        {
            _effectsHandler.ClearEffects();
        }

        public void AddEffect(IMultiplierEffect effect) => _effectsHandler.AddEffect(effect);
    }
}