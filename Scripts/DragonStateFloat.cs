using System;
using UnityEngine;

namespace AFewDragons
{
    [CreateAssetMenu(fileName = "Float State", menuName = "Dragon State/Float")]
    public class DragonStateFloat : DragonStateBase<float> {

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