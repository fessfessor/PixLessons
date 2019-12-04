using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ObjectPooler))]
public class PoolEditor : Editor
{


    public override void OnInspectorGUI() {

        ObjectPooler pooler =(ObjectPooler)target;


        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add")) {
            pooler.Add();
        }
        if (GUILayout.Button("<--")) {
            pooler.Prev();
        }
        if (GUILayout.Button("-->")) {
            pooler.Next();
        }
        if (GUILayout.Button("Delete")) {
            pooler.Delete();
        }
        
        GUILayout.EndHorizontal();

        


        base.OnInspectorGUI();
    }


}





