using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class DialogEditor : EditorWindow
{
    [MenuItem("Tools/Dialog Editor")]

    private static void OpenWindow()
    {
        GetWindow<DialogEditor>();
    }

    private Vector2 scrollPosition;
    private Vector2 scrollPositionLine;
    private int selectedDialogIndex;
    private string[] dialogsAssetsFound;
    private string[] dialogsAssetsFoundLabel;

    string imageName = "null";

    private void OnGUI()
    {
        titleContent = new GUIContent("Dialog Editor");

        if (GUILayout.Button("New Dialog"))
        {
            NewDialog();
        }

        GetAllDialogs();

        if (dialogsAssetsFound.Length == 0)
        {
            EditorGUILayout.HelpBox("No Languages Found", MessageType.Error);
            return;
        }

        selectedDialogIndex = EditorGUILayout.Popup("Dialogs",
            selectedDialogIndex, dialogsAssetsFoundLabel);

        GUILayout.Label(dialogsAssetsFound[selectedDialogIndex]);

        Dialog dialog = AssetDatabase.LoadAssetAtPath<Dialog>(dialogsAssetsFound[selectedDialogIndex]);

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        GUILayout.Label("DIALOG LIST");
        GUILayout.Space(5f);

        //DISPLAY STRINGS
        for (int i = 0; i < dialog.strings.Count; i++)
        {
            if (dialog.strings[i].image != null)
            {
                imageName = dialog.strings[i].image.ToString();
            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField(dialog.strings[i].characterName, 
                imageName != null ? imageName : "null");

            GUILayout.Space(15f);

            //GUILayout.Label(dialog.strings[i].characterName); 

            //scrollPositionLine = EditorGUILayout.BeginScrollView(scrollPositionLine);

            GUILayout.Label(dialog.strings[i].line);

            //EditorGUILayout.EndScrollView();

            //EDIT STRING
            if (GUILayout.Button("e", EditorStyles.miniButtonLeft, GUILayout.Width(25f)))
            {
                EditDialogLineWindow editDialogLineWindow = EditorWindow.GetWindow<EditDialogLineWindow>();

                editDialogLineWindow.stringIndex = i;
                editDialogLineWindow.stringContent = dialog.strings[i].line;
                editDialogLineWindow.characterName = dialog.strings[i].characterName;
                //editDialogLineWindow.image = dialog.strings[i].image;
                editDialogLineWindow.dialog = dialog;
                //editDialogLineWindow.image = dialog.strings[i].image.;
            }

            //REMOVE STRING
            if (GUILayout.Button("-", EditorStyles.miniButtonRight, GUILayout.Width(25f)))
            {
                if (EditorUtility.DisplayDialog("Confirm",
                    $"Do you really want to remove the string {dialog.strings[i].line}?",
                    "YES",
                    "NO"))
                {
                    RemoveString(dialog.strings[i].line);
                    break;
                }

            }

            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndScrollView();
    }

    private void GetAllDialogs()
    {
        dialogsAssetsFound = AssetDatabase.FindAssets("t: Dialog");
        dialogsAssetsFoundLabel = new string[dialogsAssetsFound.Length];

        for (int i = 0; i < dialogsAssetsFound.Length; i++)
        {
            dialogsAssetsFound[i] = 
                AssetDatabase.GUIDToAssetPath(dialogsAssetsFound[i]);
            dialogsAssetsFoundLabel[i] = 
                Path.GetFileName(dialogsAssetsFound[i]);

            //Debug.Log($"Trovato: {dialogsAssetsFound[i]}");
        }
    }

    private void NewDialog()
    {
        string path = EditorUtility.SaveFilePanelInProject("New Dialog Asset",
            "NewDialog",
            "asset",
            "New Dialog Saved!");

        if (!string.IsNullOrEmpty(path))
        {
            Dialog newDialog = ScriptableObject.CreateInstance<Dialog>();

            /*Dialog currentDialog =
                AssetDatabase.LoadAllAssetsAtPath<Dialog>();*/

            AssetDatabase.CreateAsset(newDialog, path);

            EditorUtility.SetDirty(newDialog);
        }
    }

    private void RemoveString(string line)
    {
        foreach (string languagePath in dialogsAssetsFound)
        {
            Dialog language = AssetDatabase.LoadAssetAtPath<Dialog>(languagePath);
            language.strings.RemoveAll(s => s.line == line);

            EditorUtility.SetDirty(language);
        }
    }
}
