using System;
using System.Collections.Generic;
using Core.Generators;
using UnityEngine;
using Zenject;

namespace Core
{
    public class GeneratorsSaveDataHandler : MonoBehaviour, ISavable
    {
        [Inject] private GeneratorsController _generatorsController;

        private void Awake()
        {
            GameProgression.OnProgressLoaded += (sender, args) =>
            {
                foreach (Generator generator in _generatorsController.GetAllGenerators())
                {
                    if (args.Data.TryGetData($"Generator Tier {generator.GetTier()} Level", out object level))
                        generator.SetLevel(Convert.ToInt32(level));
                    
                    if (args.Data.TryGetData($"Generator Tier {generator.GetTier()} State", out object state))
                        generator.gameObject.SetActive(Convert.ToBoolean(state));
                }
            };
            
            GameProgression.DataToSave.Add(this);
        }

        public List<(string, object)> SaveData()
        {
            List<(string, object)> data = new();

            foreach (Generator generator in _generatorsController.GetAllGenerators())
            {
                data.Add(($"Generator Tier {generator.GetTier()} Level", generator.GetLevel()));
                data.Add(($"Generator Tier {generator.GetTier()} State", generator.gameObject.activeSelf));
            }

            return data;
        }
    }
}
