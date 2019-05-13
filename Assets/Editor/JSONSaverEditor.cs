using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(JSONPlayerSaver))]
public class JSONSaverEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        DrawDefaultInspector();
        JSONPlayerSaver JSONSaver = (JSONPlayerSaver)target;

        //string dataPath = (Application.streamingAssetsPath +"/PlayerData.txt");

        if (GUILayout.Button("Reset Data"))
        {
            JSONSaver.ResetData();
        }
    }
}
