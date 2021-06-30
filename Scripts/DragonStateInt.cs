using System;
using UnityEngine;

namespace AFewDragons
{
    [CreateAssetMenu(fileName = "Int State", menuName = "Dragon State/Int")]
    public class DragonStateInt : DragonStateBase<int> {

        public int Min = int.MinValue;

        public int Max = int.MaxValue;

        public override void Set(int value)
        {
            value = Mathf.Clamp(value, Min, Max);
            base.Set(value);
        }

        public void Add(int value)
        {
            Set(Get() + value);
        }

        public void Subtract(int value)
        {
            Set(Get() - value);
        }
    }
}