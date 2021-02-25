using UnityEngine;
using System.Collections;
using UnityEditor;


[CustomEditor(typeof(Generator))]
[CanEditMultipleObjects]
public class GenerateMazeEditor : Editor
{

    private Generator[] scriptTargets;

    bool disableButton = false;
    string text = "";
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

        if (scriptTargets.Length > 1)
        {
            MessageType type = MessageType.None;
            for (int i = 0; i < scriptTargets.Length; i++)
            {
                text = "";
                if (scriptTargets[i] && PrefabUtility.IsPartOfAnyPrefab(scriptTargets[i]))
                {
                    //Soy un prefab
                    disableButton = true;
                    type = MessageType.Error;
                    text = "Es un prefab, desempaca el gameObject para poder usar este botón desde el editor ya que puede eliminar gameObjects.";

                    if (!scriptTargets[i].CheckPreviousSize())
                    {
                        disableButton = false;
                        text += "\nPero puedes usarlo ahora, ya que no se ha cambiado el tamaño del Laberinto y por lo tanto no se eliminarán gameObjects.";
                        type = MessageType.Warning;
                    }

                }
            }
            EditorGUILayout.HelpBox(text, type);
            EditorGUI.BeginDisabledGroup(disableButton);
            if (GUILayout.Button("Generate Maze"))
            {
                for (int i = 0; i < scriptTargets.Length; i++)
                {
                    if (!disableButton || !PrefabUtility.IsPartOfAnyPrefab(scriptTargets[i]))
                    {
                        scriptTargets[i].GenerateNewMaze();
                    }
                }
            }
            EditorGUI.EndDisabledGroup();



        }
        else
        {
            text = "";
            if (scriptTargets[0] && PrefabUtility.IsPartOfAnyPrefab(scriptTargets[0]))
            {
                //Soy un prefab
                disableButton = true;
                MessageType type = MessageType.Error;
                text = "Es un prefab, desempaca el gameObject para poder usar este botón desde el editor ya que puede eliminar gameObjects.";

                if (!scriptTargets[0].CheckPreviousSize())
                {
                    disableButton = false;
                    text += "\nPero puedes usarlo ahora, ya que no se ha cambiado el tamaño del Laberinto y por lo tanto no se eliminarán gameObjects.";
                    type = MessageType.Warning;
                }
                EditorGUILayout.HelpBox(text, type);
            }

            EditorGUI.BeginDisabledGroup(disableButton);
            if (GUILayout.Button("Generate Maze"))
            {
                if (!disableButton || !PrefabUtility.IsPartOfAnyPrefab(scriptTargets[0]))
                {
                    scriptTargets[0].GenerateNewMaze();
                }
            }
            EditorGUI.EndDisabledGroup();
        }






        serializedObject.ApplyModifiedProperties();



    }
}