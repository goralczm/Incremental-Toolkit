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

        public class OnPrestigeIncreasedEventArgs : EventArgs
        {
            public int Prestige;
        }

        public const float KAPPA = .0065f;
        public const float BETA = .5f;

        [SerializeField] private float _currency;
        [SerializeField] private int _prestigePoints;

        [Inject] private GeneratorsController _generatorsController;

        private EffectsHandler _effectsHandler = new();

        public static event EventHandler<OnCurrencyChangedEventArgs> OnCurrencyChanged;
        public static event EventHandler<OnPrestigeIncreasedEventArgs> OnPrestigeIncreased;

        private void Awake()
        {
            GameTick.OnTick += delegate(object sender, GameTick.OnTickEventArgs e)
            {
                if (e.tick % (int)(1f / GameTick.TICK_INTERVAL) == 0) ProduceTotalCurrency();
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
            return Mathf.FloorToInt(KAPPA * Mathf.Pow(GetCurrency(), BETA));
        }

        public void SetPrestigePoints(int prestigePoints)
        {
            _prestigePoints = prestigePoints;
        }

        public void Prestige()
        {
            List<IGenerator> generators = _generatorsController.GetAllGenerators();

            foreach (var generator in generators) generator.ResetGenerator();

            _prestigePoints += CalculatePrestigePoints();
            OnPrestigeIncreased?.Invoke(this, new OnPrestigeIncreasedEventArgs { Prestige = GetPrestigePoints() });
            SetCurrency(0);
        }

        public void AddEffect(IMultiplierEffect effect) => _effectsHandler.AddEffect(effect);
    }
}