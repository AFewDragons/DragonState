using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AFewDragons
{
    public abstract class DragonStateBase : ScriptableObject
    {
        public string StateName;
        public bool UseDefault = true;

        protected UnityEvent onUpdate = new UnityEvent();

        public void AddUpdateListener(UnityAction action)
        {
            onUpdate.AddListener(action);
        }

        public void RemoveUpdateListener(UnityAction action)
        {
            onUpdate.RemoveListener(action);
        }

        public new virtual System.Type GetType()
        {
            return null;
        }

        public virtual object ObjectValue()
        {
            return null;
        }
    }

    public static class DragonStateEvents
    {
        private static Dictionary<string, object> eventTypes = new Dictionary<string, object>();

        public static UnityEvent<T> GetUpdateEvent<T>(string key)
        {
            if (eventTypes.TryGetValue(key, out object value))
            {
                return (UnityEvent<T>)value;
            }
            else
            {
                var newEvent = new UnityEvent<T>();
                eventTypes.Add(key, newEvent);
                return newEvent;
            }
        }
    }

    public abstract class DragonStateBase<T> : DragonStateBase
    {
        public T Default;

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
            DragonStateEvents.GetUpdateEvent<T>(StateName).Invoke(value);
            onUpdate.Invoke();
        }

        [System.Obsolete("Use AddValueListener instead.")]
        public virtual void AddListener(UnityAction<T> action)
        {
            AddValueListener(action);
        }

        [System.Obsolete("Use RemoveValueListener instead.")]
        public virtual void RemoveListener(UnityAction<T> action)
        {
            RemoveValueListener(action);
        }

        public virtual void AddValueListener(UnityAction<T> action)
        {
            DragonStateEvents.GetUpdateEvent<T>(StateName).AddListener(action);
        }

        public virtual void RemoveValueListener(UnityAction<T> action)
        {

            DragonStateEvents.GetUpdateEvent<T>(StateName).RemoveListener(action);
        }

        public override Type GetType()
        {
            return typeof(T);
        }

        protected class DragonStateEvent<U> : UnityEvent<U> { }
    }
}