using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AFewDragons
{
    public class DragonStateUtility : MonoBehaviour
    {

        /// <summary>
        /// Gets the subtypes of a base class.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<Type> GetSubtypes<T>() where T : class
        {
            var subtypes = new List<Type>();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assembly.FullName.StartsWith("Mono.Cecil")) continue;
                if (assembly.FullName.StartsWith("UnityScript")) continue;
                if (assembly.FullName.StartsWith("Boo.Lan")) continue;
                if (assembly.FullName.StartsWith("System")) continue;
                if (assembly.FullName.StartsWith("I18N")) continue;
                if (assembly.FullName.StartsWith("UnityEngine")) continue;
                if (assembly.FullName.StartsWith("UnityEditor")) continue;
                if (assembly.FullName.StartsWith("mscorlib")) continue;
                try
                {
                    foreach (Type type in assembly.GetTypes())
                    {
                        if (!type.IsClass) continue;
                        if (type.IsAbstract) continue;
                        if (!type.IsSubclassOf(typeof(T))) continue;
                        subtypes.Add(type);
                    }
                }
                catch (System.Reflection.ReflectionTypeLoadException)
                {
                }
            }
            return subtypes;
        }

        /// <summary>
        /// Create a ScriptableObject of a specified type, calling Initialize() if present.
        /// </summary>
        /// <param name="type">The ScriptableObject type.</param>
        /// <returns>The new ScriptableObject.</returns>
        public static ScriptableObject CreateScriptableObject(Type type)
        {
            var scriptableObject = ScriptableObject.CreateInstance(type);
            InitializeScriptableObject(scriptableObject);
            return scriptableObject;
        }

        /// <summary>
        /// Initialize a ScriptableObject
        /// </summary>
        /// <param name="scriptableObject">The ScriptableObject</param>
        public static void InitializeScriptableObject(ScriptableObject scriptableObject)
        {
            if (scriptableObject == null) return;
            var methodInfo = scriptableObject.GetType().GetMethod("Initialize");
            if (methodInfo != null) methodInfo.Invoke(scriptableObject, null);
        }
    }


}
