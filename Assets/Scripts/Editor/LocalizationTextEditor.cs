using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

#if UNITY_EDITOR
public class LocalizationTextEditor : EditorWindow
{
    public LocalizationData localizationData;

    [MenuItem("Window/Localized Text Editor")]
    static void Init() {
        GetWindow(typeof(LocalizationTextEditor)).Show();
    }


    private void OnGUI() {
        if(localizationData != null) {
            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty serializedProperty = serializedObject.FindProperty("localizationData");
            EditorGUILayout.PropertyField(serializedProperty, true);
            serializedObject.ApplyModifiedProperties();

            if (GUILayout.Button("Save")) {
                SaveGameData();
            }
        }

        if (GUILayout.Button("Load")) {
            LoadGameData();
        }

        if (GUILayout.Button("Create")) {
            CreateNewData();
        }

    }


    private void LoadGameData() {
        string filePath = EditorUtility.OpenFilePanel("Select localization data file", Application.streamingAssetsPath, "json");
        if (!string.IsNullOrEmpty(filePath)) {
            string dataJson = File.ReadAllText(filePath);
            localizationData = JsonUtility.FromJson<LocalizationData>(dataJson);
        }
    }

    private void SaveGameData() {
        string filePath = EditorUtility.SaveFilePanel("Save localization file", Application.streamingAssetsPath, "", "json");

        if (!string.IsNullOrEmpty(filePath)) {
            string dataJson = JsonUtility.ToJson(localizationData);
            File.WriteAllText(filePath, dataJson);
        }
    }

    private void CreateNewData() {
        localizationData = new LocalizationData();
    }


}
#endif
