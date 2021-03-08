using System;
using UnityEngine;

namespace AFewDragons
{
    [CreateAssetMenu(fileName = "Int State", menuName = "Global State/Int")]
    public class GlobalStateInt : GlobalStateBase<int> {
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