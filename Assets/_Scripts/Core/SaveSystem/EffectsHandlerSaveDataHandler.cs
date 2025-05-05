using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(EffectsHandler))]
    public class EffectsHandlerSaveDataHandler : MonoBehaviour, ISavable
    {
        [SerializeField] private string _UUID = Guid.NewGuid().ToString();

        private EffectsHandler _effectsHandler;

        private void Awake()
        {
            _effectsHandler = GetComponent<EffectsHandler>();

            GameProgression.OnProgressLoaded += (sender, args) =>
            {
                if (args.Data.TryGetData($"Effects JSON {_UUID}", out object effectsAsJson))
                {
                    string effectsString = Convert.ToString(effectsAsJson);

                    var settings = new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    };

                    var effects = JsonConvert.DeserializeObject<List<IMultiplierEffect>>(effectsString, settings);

                    foreach (var effect in effects)
                        _effectsHandler.AddEffect(effect);
                }
            };

            GameProgression.DataToSave.Add(this);
        }

        public List<(string, object)> SaveData()
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };

            string effectsAsJson = JsonConvert.SerializeObject(_effectsHandler.GetEffects(), settings);

            return new List<(string, object)> { ($"Effects JSON {_UUID}", effectsAsJson) };
        }
    }
}
