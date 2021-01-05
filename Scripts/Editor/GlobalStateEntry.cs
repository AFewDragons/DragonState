﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AFewDragons
{
    [CustomEditor(typeof(GlobalStateBase), true, isFallback = false)]
    public class GlobalStateEntry : Editor
    {
        private void OnEnable()
        {
            //serializedObject.FindProperty
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            var name = serializedObject.FindProperty("StateName");
            var value = serializedObject.FindProperty("Comparitor");
            EditorGUILayout.PropertyField(name);
            
            if (value.type == "int")
            {
                var intComparitorType = serializedObject.FindProperty("IntComparitorType");
                EditorGUILayout.PropertyField(intComparitorType, new GUIContent("Comparison"));
            }
            EditorGUILayout.PropertyField(value);

            if (EditorApplication.isPlaying)
            {
                var runningValue = GlobalStateManager.Get<object>(name.stringValue, null);
                EditorGUILayout.LabelField($"Value is: {runningValue}");
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}