using System.Collections.Generic;
using System;
using UnityEngine.Events;

namespace AFewDragons
{
    [Serializable]
    public class GlobalState
    {
        public string Version = "1";
        public Dictionary<string, object> State = new Dictionary<string, object>();
    }

    public static class GlobalStateManager
    {
        private static UnityEvent<string, object> StateEvent = new GlobalStateEvent<string, object>();

        private class GlobalStateEvent<T, C> : UnityEvent<T, C>
        {

        }

        static GlobalStateManager()
        {
            state = new GlobalState();
        }

        private static GlobalState state;

        public static void SetState(GlobalState state)
        {
            GlobalStateManager.state = state;
        }

        public static GlobalState GetState()
        {
            return state;
        }

        public static T Get<T>(string key, T defaultState)
        {
            if (string.IsNullOrWhiteSpace(key)) return defaultState;

            if (!state.State.ContainsKey(key))
            {
                return defaultState;
            }

            return (T)state.State[key];
        }

        public static void Set<T>(string key, T newState)
        {
            if (string.IsNullOrWhiteSpace(key)) return;
            state.State[key] = newState;
            StateEvent.Invoke(key, newState);
        }

        public static void Remove(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) return;
            state.State.Remove(key);
        }

        public static void AddListener(UnityAction<string, object> action)
        {
            StateEvent.AddListener(action);
        }

        public static void RemoveListener(UnityAction<string, object> action)
        {
            StateEvent.RemoveListener(action);
        }
    }
}
