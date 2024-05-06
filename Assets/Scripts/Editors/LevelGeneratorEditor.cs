using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AbstractLevelGenerator), true)]
public class LevelGeneratorEditor : Editor
{
    AbstractLevelGenerator generator;

    private void Awake(){
        generator = (AbstractLevelGenerator)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Generate Level"))
        {
            generator.GenerateLevel();
        }
    }
}
