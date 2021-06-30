using System;
using UnityEngine;

namespace AFewDragons
{
    /// <summary>
    /// Compares a DragonState instance with the specific type of comparitor
    /// </summary>
    [Serializable]
    public class DragonStateComparison
    {
        [SerializeField]
        private DragonStateBase stateValue;
        [SerializeField]
        private bool isNumber;
        [SerializeField]
        private DragonStateComparisonNumber numberComparitorType;

        [SerializeField]
        private int intComparitor;

        [SerializeField]
        private long longComparitor;

        [SerializeField]
        private long floatComparitor;

        [SerializeField]
        private long doubleComparitor;

        [SerializeField]
        private long boolComparitor;

        [SerializeField]
        private long stringComparitor;

        [SerializeField]
        private UnityEngine.Object objectComparitor;

        private IComparable comparibleComparitor { get
            {
                Type type = stateValue.GetType();
                if (type == typeof(int) || type == typeof(short) ||
                type == typeof(uint) || type == typeof(ushort))
                {
                    return intComparitor;
                }
                else if (type == typeof(long) || type == typeof(ulong))
                {
                    return longComparitor;
                }
                else if (type == typeof(float))
                {
                    return floatComparitor;
                }
                else if (type == typeof(double))
                {
                    return doubleComparitor;
                }
                return 0;
            }
        }

        /// <summary>
        /// Checks if the stte matches the set comparitor.
        /// </summary>
        /// <returns>Boolean</returns>
        public bool Check()
        {
            if(stateValue != null)
            {
                if (isNumber)
                {
                    var numberValue = (IComparable)stateValue.ObjectValue();
                    switch (numberComparitorType)
                    {
                        case DragonStateComparisonNumber.Equals:
                            return numberValue.CompareTo(comparibleComparitor) == 0;
                        case DragonStateComparisonNumber.LessThan:
                            return numberValue.CompareTo(comparibleComparitor) < 0;
                        case DragonStateComparisonNumber.GreaterThan:
                            return numberValue.CompareTo(comparibleComparitor) > 0;
                        case DragonStateComparisonNumber.LessThanOrEqual:
                            return numberValue.CompareTo(comparibleComparitor) <= 0;
                        case DragonStateComparisonNumber.GreaterThanOrEqual:
                            return numberValue.CompareTo(comparibleComparitor) >= 0;
                    }
                }
                else
                {
                    return stateValue.ObjectValue().Equals(comparibleComparitor);
                }
            }
            return false;
        }
    }

    public enum DragonStateComparisonType
    {
        None,
        Boolean,
        Int,
        String,
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