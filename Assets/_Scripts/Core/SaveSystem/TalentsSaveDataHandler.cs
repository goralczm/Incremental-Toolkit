using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core
{
    public class TalentsSaveDataHandler : MonoBehaviour, ISavable
    {
        private List<TalentButton> _talentButtons = new();

        private void Awake()
        {
            _talentButtons = FindObjectsByType<TalentButton>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();

            GameProgression.OnProgressLoaded += (sender, args) =>
            {
                foreach (TalentButton talentButton in _talentButtons)
                {
                    if (args.Data.TryGetData(talentButton.GetTalentName(), out object wasUsed))
                    {
                        if (Convert.ToBoolean(wasUsed))
                            talentButton.ExecuteEffect();
                    }
                }
            };

            GameProgression.DataToSave.Add(this);
        }

        public List<(string, object)> SaveData()
        {
            List<(string, object)> data = new();

            foreach (TalentButton talentButton in _talentButtons)
                data.Add((talentButton.GetTalentName(), talentButton.WasUsed()));

            return data;
        }
    }
}
