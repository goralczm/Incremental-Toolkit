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
        [FormerlySerializedAs("_prestige")] [SerializeField] private int _prestigePoints;
        [SerializeField] private float _multiplier = 1f;

        [Inject] private GeneratorsController _generatorsController;

        public static event EventHandler<OnCurrencyChangedEventArgs> OnCurrencyChanged;
        public static event EventHandler<OnPrestigeIncreasedEventArgs> OnPrestigeIncreased;

        private void Awake()
        {
            GameTick.OnTick += delegate(object sender, GameTick.OnTickEventArgs e)
            {
                if (e.tick % (int)(1f / GameTick.TICK_INTERVAL) == 0) ProduceTotalCurrency();
            };
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
            float generatorsProduction = _generatorsController.GetActiveGeneratorsProduction();
            float prestigeMultiplier = GetPrestigeMultiplier();

            return generatorsProduction * prestigeMultiplier;
        }

        public float GetPrestigeMultiplier()
        {
            return Mathf.Pow(1 + .02f, GetPrestigePoints());
        }

        public float ApplyMultiplier(float currency)
        {
            return currency * _multiplier;
        }

        public void ProduceTotalCurrency()
        {
            AddCurrency(ApplyMultiplier(GetTotalProduction()));
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
            Generator[] generators =
                FindObjectsByType<Generator>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            for (int i = 0; i < generators.Length; i++)
            {
                if (generators[i].GetTier() > 1)
                    generators[i].gameObject.SetActive(false);

                generators[i].SetLevel(1);
            }

            _prestigePoints += CalculatePrestigePoints();
            OnPrestigeIncreased?.Invoke(this, new OnPrestigeIncreasedEventArgs { Prestige = GetPrestigePoints() });
            SetCurrency(0);
        }

        public void AddMultiplier(float amount) => _multiplier += amount;

        public void RemoveMultiplier(float amount) => _multiplier -= amount;
    }
}