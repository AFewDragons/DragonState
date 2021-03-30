using System;
using UnityEngine;

namespace AFewDragons
{
    public abstract class GlobalStateBase : ScriptableObject { }

    public abstract class GlobalStateBase<T> : GlobalStateBase
    {
        public string StateName;

        public bool UseDefault = true;

        public T Default;

        public virtual T Get()
        {
            return GlobalStateManager.Get(StateName, UseDefault ? Default : default(T));
        }

        public virtual void Set(T value)
        {
            GlobalStateManager.Set(StateName, value);
        }
    }
}