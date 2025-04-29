using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Core
{
    public class StatisticsSaveDataHandler : MonoBehaviour, ISavable
    {
        [Inject] private Statistics _statistics;

        private void Awake()
        {
            GameProgression.OnProgressLoaded += (sender, args) =>
            {
                if (args.Data.TryGetData("Total Earned", out var totalEarned))
                    _statistics.SetTotalEarned(Convert.ToSingle(totalEarned));
            };

            GameProgression.DataToSave.Add(this);
        }

        public List<(string, object)> SaveData()
        {
            return new List<(string, object)> { ("Total Earned", _statistics.GetTotalEarned()) };
        }
    }
}
