using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(EnemyBase))]
public class EnemyBaseEditor : Editor
{

    private EnemyBase enemyBase;

    private void Awake() {
        enemyBase = (EnemyBase)target;
    }

    public override void OnInspectorGUI() {
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("New Enemy")) {
            enemyBase.CreateEnemy();
        }

        if (GUILayout.Button("Remove")) {
            enemyBase.RemoveEnemy();
        }

        if (GUILayout.Button("<--")) {
            enemyBase.PrevEnemy();
        }
        if (GUILayout.Button("-->")) {
            enemyBase.NextEnemy();
        }


        GUILayout.EndHorizontal();

        base.OnInspectorGUI();
    }


}
