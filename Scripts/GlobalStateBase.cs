using System;
using UnityEngine;

namespace AFewDragons
{
    public class GlobalStateBase : ScriptableObject { }

    public class GlobalStateBase<T> : GlobalStateBase
    {
        public string StateName;

        public virtual T Get()
        {
            return GlobalStateManager.Get(StateName, default(T));
        }

        public virtual void Set(T value)
        {
            GlobalStateManager.Set(StateName, value);
        }
    }
}