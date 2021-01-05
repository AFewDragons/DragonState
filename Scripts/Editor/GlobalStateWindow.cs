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
            GlobalStateWindow window = (GlobalStateWindow)EditorWindow.GetWindow(typeof(GlobalStateWindow));
            window.Show();
        }

        private void Awake()
        {
            Debug.Log("Start");
            GlobalStateManager.AddListener(StateChanged);
        }

        private void OnDestroy()
        {
            GlobalStateManager.RemoveListener(StateChanged);
        }

        private void StateChanged(string key, object value)
        {
            Debug.Log("Repaint");
            Repaint();
        }

        void OnGUI()
        {
            if (EditorApplication.isPlaying)
            {
                foreach (var state in GlobalStateManager.GetState().State)
                {
                    EditorGUILayout.LabelField(state.Key, state.Value.ToString());
                }
            }
            else
            {
                EditorGUILayout.LabelField("Game not running.");
            }
            
        }
    }
}