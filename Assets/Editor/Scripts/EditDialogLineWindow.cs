using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EditDialogLineWindow : EditorWindow
{
    public int stringIndex;
    public string stringContent;
    public SerializedProperty image;
    public string characterName;
    public Dialog dialog;
    public bool confirm;



    private void OnGUI()
    {
        titleContent = new GUIContent("Edit Dialog Line");

        /*GUILayout.Label("Index");

        EditorGUILayout.TextField(stringIndex.ToString());*/

        /*GUI.enabled = false;
        EditorGUILayout.TextField(stringIndex.ToString());
        GUI.enabled = true;*/

        GUILayout.Label("Character Name");

        EditorGUILayout.TextField(characterName);

        //GUILayout.Label("Image");

        if (image != null)
        {
            EditorGUILayout.ObjectField(image);
        }
        

        GUILayout.Label("Content");
        stringContent = EditorGUILayout.TextArea(stringContent, GUILayout.ExpandHeight(true));

        GUILayout.Space(25f);

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("OK"))
        {
            confirm = true;
            dialog.strings[stringIndex].line = stringContent;
            Close();
        }

        if (GUILayout.Button("Cancel"))
        {
            Close();
        }

    }
}
