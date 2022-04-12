using System;
using UnityEngine;
using UnityEngine.Events;

namespace AFewDragons
{
    public abstract class DragonStateBase : ScriptableObject {
        public string StateName;
        public bool UseDefault = true;

        public new virtual System.Type GetType()
        {
            return null;
        }

        public virtual object ObjectValue()
        {
            return null;
        }
    }

    public abstract class DragonStateBase<T> : DragonStateBase
    {
        
        

        public T Default;
        protected DragonStateEvent<T> updateEvent = new DragonStateEvent<T>();

        public virtual T Get()
        {
            return DragonStateManager.Get(StateName, UseDefault ? Default : default(T));
        }

        public override object ObjectValue()
        {
            return Get();
        }

        public virtual void Set(T value)
        {
            DragonStateManager.Set(StateName, value);
            updateEvent.Invoke(value);
        }

        public virtual void AddListener(UnityAction<T> action)
        {
            updateEvent.AddListener(action);
        }

        public virtual void RemoveListener(UnityAction<T> action)
        {
            updateEvent.RemoveListener(action);
        }

        public override Type GetType()
        {
            return typeof(T);
        }

        protected class DragonStateEvent<U> : UnityEvent<U> { }
    }
}