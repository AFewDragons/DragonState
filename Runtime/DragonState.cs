using System.Collections.Generic;
using System;
using UnityEngine.Events;

namespace AFewDragons
{
    [Serializable]
    public class DragonState
    {
        public string Version = "1";
        public Dictionary<string, object> State = new Dictionary<string, object>();
    }

    public static class DragonStateManager
    {
        private static UnityEvent<string, object> StateEvent = new GlobalStateEvent<string, object>();

        private class GlobalStateEvent<T, C> : UnityEvent<T, C>
        {

        }

        static DragonStateManager()
        {
            state = new DragonState();
        }

        private static DragonState state;

        public static void SetState(DragonState state)
        {
            DragonStateManager.state = state;
        }

        public static DragonState GetState()
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
