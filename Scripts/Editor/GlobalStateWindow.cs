using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using System.Linq;
using System;

namespace AFewDragons
{
    public class GlobalStateWindow : EditorWindow
    {
        private System.Type[] types;

        [MenuItem("Window/Global State")]
        static void Init()
        {
            // Get existing open window or if none, make a new one:
            GlobalStateWindow window = (GlobalStateWindow)GetWindow(typeof(GlobalStateWindow));
            window.Show();
        }

        private void SetTypes()
        {
            var allTypes = Assembly.GetAssembly(typeof(GlobalStateBase)).GetTypes();
            types = (from System.Type type in allTypes where type.IsSubclassOf(typeof(GlobalStateBase)) select type).Where(t => !t.Name.Contains("GlobalStateBase")).ToArray();
        }

        private void Awake()
        {
            GlobalStateManager.AddListener(StateChanged);
        }

        private void OnDestroy()
        {
            GlobalStateManager.RemoveListener(StateChanged);
        }

        private void StateChanged(string key, object value)
        {
            Repaint();
        }

        void OnGUI()
        {
            if (EditorApplication.isPlaying)
            {
                if (types == null) SetTypes();
                if (GlobalStateManager.GetState() != null && GlobalStateManager.GetState().State != null)
                {
                    string removeState = null;
                    foreach (var state in GlobalStateManager.GetState().State)
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField(state.Key, state.Value.ToString());
                        if(GUILayout.Button("Delete", GUILayout.Width(50))){
                            removeState = state.Key;
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                    //New state
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("");
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Add new type", GUILayout.Width(110));
                    var dropdownRect = EditorGUILayout.GetControlRect();
                    if (EditorGUI.DropdownButton(dropdownRect, new GUIContent(newStateType ?? "Type"), FocusType.Passive)){
                        var menu = new GenericMenu();

                        foreach (var type in types)
                        {

                            menu.AddItem(new GUIContent(type.Name), type.Name == newStateType, OnStateTypeSelected, type.Name);
                        }
                        menu.DropDown(dropdownRect);
                    }
                    if (!string.IsNullOrEmpty(newStateType))
                    {
                        newStateKey = EditorGUILayout.TextField(newStateKey);
                        switch (newStateType)
                        {
                            case "GlobalStateFloat":
                                newStateValue = EditorGUILayout.FloatField(newStateValue != null ? (float)newStateValue : 0);
                                break;
                            case "GlobalStateString":
                                newStateValue = EditorGUILayout.TextField(newStateValue != null ? (string)newStateValue : "");
                                break;
                            case "GlobalStateInt":
                                newStateValue = EditorGUILayout.IntField(newStateValue != null ? (int)newStateValue : 0);
                                break;
                            case "GlobalStateBool":
                                newStateValue = GUILayout.Toggle(newStateValue != null ? (bool)newStateValue : false, GUIContent.none);
                                break;
                            default:
                                EditorGUILayout.LabelField("Not implemented.", GUILayout.Width(110));
                                break;
                        }
                        if (GUILayout.Button("Add", GUILayout.Width(45)))
                        {
                            GlobalStateManager.Set(newStateKey, newStateValue);
                            newStateType = null;
                            newStateValue = null;
                            newStateKey = "";
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                    
                    if (!string.IsNullOrEmpty(removeState))
                    {
                        GlobalStateManager.Remove(removeState);
                    }
                }
                
            }
            else
            {
                EditorGUILayout.LabelField("Game not running.");
            }
        }

        private string newStateKey = "";
        private object newStateValue;
        private string newStateType;

        private void OnStateTypeSelected(object stateType) {
            newStateType = (string)stateType;
            newStateValue = null;
        }
    }
}