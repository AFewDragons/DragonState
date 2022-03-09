using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AFewDragons
{
    public class DragonStateComparisonBool : DragonStateComparisonBase
    {
        public DragonStateBool comparison;
        public bool compareValue;

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
            return $"Comparison Bool: {comparison?.name ?? ""}";
        }
    }
}