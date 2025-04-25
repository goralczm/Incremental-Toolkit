using Core;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TimeControl))]
public class TimeControlEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        GUILayout.Space(10);
        GUILayout.Label("Debug");
        GUILayout.Label($"Time elapsed: {Time.time}");
    }
}
