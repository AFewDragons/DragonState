using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using System.Linq;
using System;

namespace AFewDragons
{
    public class DragonStateWindow : EditorWindow
    {
        private System.Type[] types;

        private Texture logo;

        [MenuItem("Window/Dragon State")]
        static void Init()
        {
            // Get existing open window or if none, make a new one:
            DragonStateWindow window = (DragonStateWindow)GetWindow(typeof(DragonStateWindow));
            window.Show();
        }

        private void SetTypes()
        {
            var allTypes = Assembly.GetAssembly(typeof(DragonStateBase)).GetTypes();
            types = (from System.Type type in allTypes where type.IsSubclassOf(typeof(DragonStateBase)) select type).Where(t => !t.Name.Contains("DragonStateBase")).ToArray();
        }

        private void Awake()
        {
            DragonStateManager.AddListener(StateChanged);
        }

        private void OnDestroy()
        {
            DragonStateManager.RemoveListener(StateChanged);
        }

        private void StateChanged(string key, object value)
        {
            Repaint();
        }

        void OnGUI()
        {
            var logoRect = EditorGUILayout.GetControlRect(false, 35);
            GUI.DrawTexture(logoRect, DragonStateResources.Logo, ScaleMode.ScaleToFit);
            if (EditorApplication.isPlaying)
            {
                if (types == null) SetTypes();
                if (DragonStateManager.GetState() != null && DragonStateManager.GetState().State != null)
                {
                    string removeState = null;
                    foreach (var state in DragonStateManager.GetState().State)
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
                            case "DragonStateFloat":
                                newStateValue = EditorGUILayout.FloatField(newStateValue != null ? (float)newStateValue : 0);
                                break;
                            case "DragonStateString":
                                newStateValue = EditorGUILayout.TextField(newStateValue != null ? (string)newStateValue : "");
                                break;
                            case "DragonStateInt":
                                newStateValue = EditorGUILayout.IntField(newStateValue != null ? (int)newStateValue : 0);
                                break;
                            case "DragonStateBool":
                                newStateValue = GUILayout.Toggle(newStateValue != null ? (bool)newStateValue : false, GUIContent.none);
                                break;
                            default:
                                EditorGUILayout.LabelField("Not implemented.", GUILayout.Width(110));
                                break;
                        }
                        if (GUILayout.Button("Add", GUILayout.Width(45)))
                        {
                            DragonStateManager.Set(newStateKey, newStateValue);
                            newStateType = null;
                            newStateValue = null;
                            newStateKey = "";
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                    
                    if (!string.IsNullOrEmpty(removeState))
                    {
                        DragonStateManager.Remove(removeState);
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