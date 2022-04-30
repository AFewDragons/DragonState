using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AFewDragons {
    public static class DragonStateResources
    {
        private static Texture2D logo;
        public static Texture2D Logo {  get { return logo != null ? logo : logo = Resources.Load<Texture2D>("DragonStateLogo"); } }

        private static GUIStyle boxStyle;
        public static GUIStyle BoxStyle
        {
            get
            {
                if (boxStyle == null)
                {
                    boxStyle = new GUIStyle(GUI.skin.box);
                    boxStyle.padding = new RectOffset(5, 5, 5, 5);
                    boxStyle.border = new RectOffset(1, 1, 1, 1);
                }
                return boxStyle;
            }
        }

        private static GUIStyle errorStyle;
        public static GUIStyle ErrorStyle
        {
            get
            {
                if(errorStyle == null)
                {
                    errorStyle = new GUIStyle(GUI.skin.label);
                    errorStyle.normal.textColor = Color.red;
                }
                return errorStyle;
            }
        }
    }
}