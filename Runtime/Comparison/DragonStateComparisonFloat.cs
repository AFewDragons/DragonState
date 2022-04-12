using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AFewDragons
{
    public class DragonStateComparisonFloat : DragonStateComparisonBase
    {
        public DragonStateFloat comparison;
        public DragonStateComparisonNumber comparisonType;
        public float compareValue;

        public override bool Check()
        {
            if(comparison == null)
            {
                return false;
            }
            switch (comparisonType)
            {
                case DragonStateComparisonNumber.Equals:
                    return comparison.Get() == compareValue;
                case DragonStateComparisonNumber.LessThan:
                    return comparison.Get() < compareValue;
                case DragonStateComparisonNumber.GreaterThan:
                    return comparison.Get() > compareValue;
                case DragonStateComparisonNumber.LessThanOrEqual:
                    return comparison.Get() <= compareValue;
                case DragonStateComparisonNumber.GreaterThanOrEqual:
                    return comparison.Get() >= compareValue;
                default:
                    return comparison.Get() == compareValue;
            }
        }

        public override string GetEditorName()
        {
            return $"Float: {comparison?.name ?? ""}";
        }
    }
}