using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bank : MonoBehaviour
{
    public class OnCurrencyChangedEventArgs : EventArgs
    {
        public float Currency;
    }

    [SerializeField] private float _currency;
    [SerializeField] private int _prestige;
    [SerializeField] private float _multiplier = 1f;
    
    public static event EventHandler<OnCurrencyChangedEventArgs> OnCurrencyChanged;
    
    private static List<Generator> _generators = new();

    public const float KAPPA = .0065f;
    public const float BETA = .5f;

    private void Awake()
    {
        _generators = new();
        
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
    
    public static void RegisterGenerator(Generator generator)
    {
        _generators.Add(generator);
    }

    public static void RemoveGenerator(Generator generator)
    {
        _generators.Remove(generator);
    }

    public float GetTotalProduction()
    {
        float generatorsProduction = _generators.Sum(g => g.GetProduction());
        float prestigeMultiplier = Mathf.Pow(1 + .02f, GetPrestige());
        
        return generatorsProduction * prestigeMultiplier;
    }

    public float ApplyMultiplier(float currency)
    {
        return currency * _multiplier;
    }

    public void ProduceTotalCurrency()
    {
        AddCurrency(ApplyMultiplier(GetTotalProduction()));
    }

    public int GetPrestige() => _prestige;

    public int GetPrestigePoints()
    {
        return Mathf.FloorToInt(KAPPA * Mathf.Pow(GetCurrency(), BETA));
    }
    
    public void IncreasePrestige()
    {
        Generator[] generators = FindObjectsByType<Generator>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        
        for (int i = 0; i < generators.Length; i++)
        {
            if (generators[i].GetTier() > 1)
                generators[i].gameObject.SetActive(false);
            
            generators[i].SetLevel(1);
        }
        
        _prestige += GetPrestigePoints();
        SetCurrency(0);
    }
    
    public void AddMultiplier(float amount) => _multiplier += amount;

    public void RemoveMultiplier(float amount) => _multiplier -= amount;
}
