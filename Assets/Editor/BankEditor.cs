using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Bank))]
public class BankEditor : Editor
{
    private Bank _bank;
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        _bank = (Bank)target;
        
        GUILayout.Space(10);
        GUILayout.Label("Debug");
        GUILayout.Label($"Production: {_bank.GetProduction()}");
        GUILayout.Label($"Prestige Points: {_bank.GetPrestigePoints()}");

        float ppThreshold = Mathf.Pow(Bank.KAPPA, -(1 / Bank.BETA));
        
        GUILayout.Label($"PP Threshold ~= {ppThreshold}");
        GUILayout.Label($"Time to reach 1 PP (seconds) ~= {ppThreshold / _bank.GetProduction()}");
        GUILayout.Label($"Time to reach 1 PP (minutes) ~= {ppThreshold / _bank.GetProduction() / 60f}");
        GUILayout.Label($"Time to reach 1 PP (hours) ~= {ppThreshold / _bank.GetProduction() / 360f}");
        
        if (GUILayout.Button("Prestige!"))
            _bank.IncreasePrestige();
    }
}
