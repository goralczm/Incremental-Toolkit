using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Core
{
    public class BankSaveDataHandler : MonoBehaviour, ISavable
    {
        [Inject] private Bank _bank;

        private void Awake()
        {
            GameProgression.OnProgressLoaded += (sender, args) =>
            {
                if (args.Data.TryGetData("Currency", out object currency))
                    _bank.SetCurrency(Convert.ToSingle(currency));
                
                if (args.Data.TryGetData("Prestige Points", out object prestigePoints))
                    _bank.SetPrestigePoints(Convert.ToInt32(prestigePoints));
            };
            
            GameProgression.DataToSave.Add(this);
        }

        public List<(string, object)> SaveData()
        {
            return new List<(string, object)>
            {
                ("Currency", _bank.GetCurrency()),
                ("Prestige Points", _bank.GetPrestigePoints())
            };
        }
    }
}
