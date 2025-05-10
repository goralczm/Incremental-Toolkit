using System;
using Core.Generators;
using Core.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.UI
{
    public class GeneratorDisplay : MonoBehaviour
    {
        [SerializeField] private Generator _generator;
        [SerializeField] private TextMeshPro _informationText;
        [SerializeField] private TextMeshPro _levelText;
        [SerializeField] private TextMeshPro _tierText;

        private void Start()
        {
            _tierText.SetText($"Tier {_generator.GetTier()}");
        }

        private void Update()
        {
            _informationText.SetText($"Cost: {((float)_generator.GetCost()).FormatToLowestNumber()}\n" +
                                     $"Production: {_generator.GetProduction().FormatToLowestNumber()}");

            _levelText.SetText(_generator.GetLevel().ToString());
        }
    }
}
