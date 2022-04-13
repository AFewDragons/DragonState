using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AFewDragons
{
    public class DragonStateComparisonWindowEditor : EditorWindow
    {
        private Editor dragonComparisonEditor; 

        public static void ShowWindow(Editor editor)
        {
            var window = (DragonStateComparisonWindowEditor)GetWindow(typeof(DragonStateComparisonWindowEditor));
            window.Set(editor);
        }

        public void Set(Editor editor)
        {
            dragonComparisonEditor = editor;
        }

        void OnGUI()
        {
            if(dragonComparisonEditor != null)
            {
                dragonComparisonEditor.OnInspectorGUI();
            }
        }
    }
}