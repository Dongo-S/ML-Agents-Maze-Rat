using UnityEngine;
using System.Collections;
using UnityEditor;


[CustomEditor(typeof(Generator))]
[CanEditMultipleObjects]
public class GenerateMazeEditor : Editor
{

    private Generator[] scriptTargets;


    private void OnEnable()
    {

        Object[] monoObjects = targets;
        scriptTargets = new Generator[monoObjects.Length];
        for (int i = 0; i < monoObjects.Length; i++)
        {
            scriptTargets[i] = monoObjects[i] as Generator;
        }
    }


    public override void OnInspectorGUI()
    { 
        DrawDefaultInspector();
     

        if (GUILayout.Button("Generate Maze"))
        {
            for (int i = 0; i < scriptTargets.Length; i++)
            {
                scriptTargets[i].GenerateNewMaze();
            }
        }

            serializedObject.ApplyModifiedProperties();

        

    }
}