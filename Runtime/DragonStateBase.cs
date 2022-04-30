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
            if (string.IsNullOrWhiteSpace(StateName))
            {
                Debug.LogError($"DragonState: {this.name}'s StateName is not set. You will need to set this before it Set or Get methods can be used on this state.", this);
                return default(T);
            }
            return DragonStateManager.Get(StateName, UseDefault ? Default : default(T));
        }

        public override object ObjectValue()
        {
            return Get();
        }

        public virtual void Set(T value)
        {
            if (string.IsNullOrWhiteSpace(StateName))
            {
                Debug.LogError($"DragonState: {this.name}'s StateName is not set. You will need to set this before it Set or Get methods can be used on this state.", this);
                return;
            }
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