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
        private float floatComparitor;

        [SerializeField]
        private double doubleComparitor;

        [SerializeField]
        private bool boolComparitor;

        [SerializeField]
        private string stringComparitor;

        [SerializeField]
        private UnityEngine.Object objectComparitor;

        private IComparable comparibleComparitor
        {
            get
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

        private object comparitor
        {
            get
            {
                Type type = stateValue.GetType();
                Debug.Log(type.Name);
                if (type == typeof(bool) || type == typeof(Boolean))
                {
                    return boolComparitor;
                }
                else if (type == typeof(string) || type == typeof(String))
                {
                    return stringComparitor;
                }
                else if (type == typeof(UnityEngine.Object))
                {
                    return objectComparitor;
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
                    Debug.Log($"Value {stateValue.StateName} is {stateValue.ObjectValue()} and  {comparitor}");
                    return stateValue.ObjectValue().Equals(comparitor);
                }
            }
            return true;
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