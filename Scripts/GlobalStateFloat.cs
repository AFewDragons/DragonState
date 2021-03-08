using System;
using UnityEngine;

namespace AFewDragons
{
    [CreateAssetMenu(fileName = "Float State", menuName = "Global State/Float")]
    public class GlobalStateFloat : GlobalStateBase<float> {
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