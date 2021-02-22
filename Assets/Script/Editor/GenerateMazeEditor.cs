using UnityEngine;
using System.Collections;
using UnityEditor;


[CustomEditor(typeof(Generator))]
public class SomeScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Generator scriptTarget = (Generator)target;

        if (GUILayout.Button("Generate Maze"))
        {
            scriptTarget.GenerateNewMaze();
        }
       
    }
}