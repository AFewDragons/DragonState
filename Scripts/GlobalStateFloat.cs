using System;
using UnityEngine;

namespace AFewDragons
{
    [CreateAssetMenu(fileName = "Float State", menuName = "Global State/Float")]
    public class GlobalStateFloat : GlobalStateBase<float> {

        public float Min = float.MinValue;

        public float Max = float.MaxValue;

        public override void Set(float value)
        {
            value = Mathf.Clamp(value, Min, Max);
            base.Set(value);
        }

        public void Add(float value)
        {
            Set(Get() + value);
        }

        public void Subtract(float value)
        {
            Set(Get() - value);
        }
    }
}