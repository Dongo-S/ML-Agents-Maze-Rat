using UnityEngine;
using System.Collections;
using UnityEditor;


[CustomEditor(typeof(Generator))]
public class SomeScriptEditor : Editor
{
    public override void OnInspectorGUI()
    { 
        
        Generator scriptTarget = (Generator)target;


        DrawDefaultInspector();
     

        if (GUILayout.Button("Generate Maze"))
        {
            scriptTarget.GenerateNewMaze();
        }
       
    }
}