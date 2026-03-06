using System;
using UnityEngine;

namespace AFewDragons
{
    [CreateAssetMenu(fileName = "Vector3 State", menuName = "A Few Dragons/Dragon State/Vector3")]
    public class DragonStateVector3 : DragonStateBase<Vector3>
    {
        private class V3
        {
            public float x; public float y;, public float z;
        }

        public override Vector3 Get()
        {
            var vector = base.Get();
            return new Vector3(vector.x, vector.y, vector.z);
        }

        public override void Set(Vector3 value)
        {
            base.Set(value);
        }

        public void Add(Vector3 value)
        {
            Set(Get() + value);
        }

        public void Subtract(Vector3 value)
        {
            Set(Get() - value);
        }
    }
}