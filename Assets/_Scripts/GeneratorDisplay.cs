using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GeneratorDisplay : MonoBehaviour
{
    [SerializeField] private Generator _generator;
    [SerializeField] private TextMeshPro _informationText;
    [SerializeField] private TextMeshPro _levelText;

    private void Update()
    {
        _informationText.SetText($"Cost: {_generator.GetCost()}\n" +
                      $"Production: {_generator.GetProduction().LimitDecimalPoints(2)}");
        
        _levelText.SetText(_generator.GetLevel().ToString());
    }
}
