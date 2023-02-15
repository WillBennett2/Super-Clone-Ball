using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MovingPlatforms))]
public class ObjectEditor : Editor
{ 
    SerializedProperty m_CanMove;
    SerializedProperty m_MoveSpeed;
    SerializedProperty m_StartPos;
    SerializedProperty m_EndPos;
    
    SerializedProperty m_CanSpin;
    SerializedProperty m_SpinSpeed;
    protected static bool m_ShowMovementSettings = true;
    private void OnEnable()
    {
        m_CanMove = serializedObject.FindProperty("m_CanMove");
        m_MoveSpeed = serializedObject.FindProperty("m_MoveSpeed");
        m_StartPos = serializedObject.FindProperty("m_StartPos");
        m_EndPos = serializedObject.FindProperty("m_EndPos");
        m_CanSpin = serializedObject.FindProperty("m_CanSpin");
        m_SpinSpeed = serializedObject.FindProperty("m_SpinSpeed");

    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        serializedObject.Update();

        //m_ShowMovementSettings = EditorGUILayout.BeginFoldoutHeaderGroup(m_ShowMovementSettings, "Movement Info");
        EditorGUILayout.LabelField("Movement Info",EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        m_CanMove.boolValue = EditorGUILayout.Toggle("Able to move", m_CanMove.boolValue);
        m_MoveSpeed.floatValue = EditorGUILayout.FloatField("Movement speed", m_MoveSpeed.floatValue);
        EditorGUILayout.EndHorizontal();
        m_StartPos.vector3Value = EditorGUILayout.Vector3Field("Start position", m_StartPos.vector3Value);
        m_EndPos.vector3Value = EditorGUILayout.Vector3Field("End Position", m_EndPos.vector3Value);
        //EditorGUILayout.EndFoldoutHeaderGroup();
        EditorGUILayout.Space();
        //EditorGUILayout.BeginFoldoutHeaderGroup(true, "Spin Info");
        EditorGUILayout.LabelField("Spin Info", EditorStyles.boldLabel);
        m_CanSpin.boolValue = EditorGUILayout.Toggle("Able to spin", m_CanSpin.boolValue);
        m_SpinSpeed.vector3Value = EditorGUILayout.Vector3Field("Spin speed", m_SpinSpeed.vector3Value);
        //EditorGUILayout.EndFoldoutHeaderGroup();

        serializedObject.ApplyModifiedProperties();
    }
}
