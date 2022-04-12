using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AFewDragons
{
    public class DragonStateComparisonString : DragonStateComparisonBase
    {
        public DragonStateString comparison;
        public string compareValue;

        public override bool Check()
        {
            if(comparison == null)
            {
                return false;
            }
            return comparison.Get() == compareValue;
        }

        public override string GetEditorName()
        {
            return $"String: {comparison?.name ?? ""}";
        }
    }
}