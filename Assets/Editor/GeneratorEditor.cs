using Core;
using Core.Generators;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Generator))]
public class GeneratorEditor : Editor
{
    private Generator _generator;
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        _generator = (Generator)target;

        GUILayout.Space(10);
        GUILayout.Label("Debug");

        if (GUILayout.Button($"Buy upgrade")) _generator.BuyUpgrade();
        if (GUILayout.Button($"Force upgrade")) _generator.AddLevel();
        
        GUILayout.Label("Info");
        GUILayout.Label($"Cost: {_generator.GetCost()}");
        if (Application.isPlaying)
        {
            GUILayout.Label($"Production: {_generator.GetProduction()}");
            GUILayout.Label($"Multiplier: {_generator.GetMultiplier()}");
            GUILayout.Label($"Time to buy: {_generator.GetCost() / FindAnyObjectByType<Bank>().GetTotalProduction()}");
        }
    }
}
