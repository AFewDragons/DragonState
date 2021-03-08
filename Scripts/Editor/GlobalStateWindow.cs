using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AFewDragons
{
    public class GlobalStateWindow : EditorWindow
    {
        [MenuItem("Window/Global State")]
        static void Init()
        {
            // Get existing open window or if none, make a new one:
            GlobalStateWindow window = (GlobalStateWindow)GetWindow(typeof(GlobalStateWindow));
            window.Show();
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
                if(GlobalStateManager.GetState() != null && GlobalStateManager.GetState().State != null)
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
    }
}