using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AFewDragons
{
    public class DragonStateComparisonInt : DragonStateComparisonBase
    {
        public DragonStateInt comparison;
        public DragonStateComparisonNumber comparisonType;
        public int compareValue;

        public override bool Check()
        {
            if (comparison == null)
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
            return $"Int: {comparison?.name ?? ""}";
        }
    }
}