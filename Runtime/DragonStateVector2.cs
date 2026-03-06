using System;
using UnityEngine;

namespace AFewDragons
{
    [CreateAssetMenu(fileName = "Vector2 State", menuName = "A Few Dragons/Dragon State/Vector2")]
    public class DragonStateVector2 : DragonStateBase<Vector2> {
        private class V2
        {
            public float x; public float y;
        }

        public override Vector2 Get()
        {
            var vector = base.Get();
            return new Vector2(vector.x, vector.y);
        }

        public override void Set(Vector2 value)
        {
            base.Set(value);
        }

        public void Add(Vector2 value)
        {
            Set(Get() + value);
        }

        public void Subtract(Vector2 value)
        {
            Set(Get() - value);
        }
    }
}