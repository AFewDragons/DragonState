using System;
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

    /// <summary>
    /// Compares a DragonState instance with the specific type of comparitor
    /// </summary>
    [Serializable]
    public class DragonStateComparison
    {
        [SerializeField]
        private List<DragonStateComparisonBase> comparisonList = new List<DragonStateComparisonBase>();

        /// <summary>
        /// Checks if the stte matches the set comparitor.
        /// </summary>
        /// <returns>Boolean</returns>
        public bool Check()
        {
            foreach (var item in comparisonList)
            {
                if (!item.Check()) return false;
            }
            return true;
        }
    }

    public enum DragonStateComparisonNumber
    {
        Equals,
        LessThan,
        GreaterThan,
        LessThanOrEqual,
        GreaterThanOrEqual,
    }
}