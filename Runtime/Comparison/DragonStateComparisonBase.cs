using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AFewDragons
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class DragonStateComparisonBase : ScriptableObject
    {
        public virtual void Initialize() { }

        public virtual bool Check() { return false; }

        public virtual string GetEditorName()
        {
            return GetType().Name;
        }
    }
}