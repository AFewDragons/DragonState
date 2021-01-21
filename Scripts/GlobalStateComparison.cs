using System;

namespace AFewDragons
{
    [Serializable]
    public class GlobalStateComparison
    {
        public string StateName;
        public GlobalStateComparisonType Type;

        public GlobalStateBool BoolValue;
        public GlobalStateInt IntValue;
        public GlobalStateString StringValue;

        public bool BooleanComparitor;
        public int IntComparitor;
        public GlobalStateComparisonNumber IntComparitorType;
        public string StringComparitor;

        public bool Check()
        {
            switch (Type)
            {
                case GlobalStateComparisonType.Boolean:
                    return BoolValue.Get() == BooleanComparitor;
                case GlobalStateComparisonType.Int:
                    var value = IntValue.Get();
                    switch (IntComparitorType)
                    {
                        case GlobalStateComparisonNumber.Equals:
                            return value == IntComparitor;
                        case GlobalStateComparisonNumber.LessThan:
                            return value < IntComparitor;
                        case GlobalStateComparisonNumber.GreaterThan:
                            return value > IntComparitor;
                        case GlobalStateComparisonNumber.LessThanOrEqual:
                            return value <= IntComparitor;
                        case GlobalStateComparisonNumber.GreaterThanOrEqual:
                            return value >= IntComparitor;
                    }
                    break;
                case GlobalStateComparisonType.String:
                    return StringValue.Get() == StringComparitor;
                case GlobalStateComparisonType.None:
                    return true;
            }
            return false;
        }
    }

    public enum GlobalStateComparisonType
    {
        None,
        Boolean,
        Int,
        String,
    }

    public enum GlobalStateComparisonNumber
    {
        Equals,
        LessThan,
        GreaterThan,
        LessThanOrEqual,
        GreaterThanOrEqual,
    }
}