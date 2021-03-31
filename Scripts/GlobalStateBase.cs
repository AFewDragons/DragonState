using UnityEngine;
using UnityEngine.Events;

namespace AFewDragons
{
    public abstract class GlobalStateBase : ScriptableObject { }

    public abstract class GlobalStateBase<T> : GlobalStateBase
    {
        public string StateName;
        public bool UseDefault = true;

        public T Default;
        protected GlobalStateEvent<T> updateEvent = new GlobalStateEvent<T>();

        public virtual T Get()
        {
            return GlobalStateManager.Get(StateName, UseDefault ? Default : default(T));
        }

        public virtual void Set(T value)
        {
            GlobalStateManager.Set(StateName, value);
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

        protected class GlobalStateEvent<T> : UnityEvent<T> { }
    }
}