using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

        /// <summary>
        /// Add asset to another asset through asset database. Does prefab checking.
        /// </summary>
        /// <param name="baseObj"></param>
        /// <param name="obj"></param>
        public static void AddToAsset(ScriptableObject baseObj, UnityEngine.Object obj)
        {
            var so = obj as ScriptableObject;
            baseObj.hideFlags = HideFlags.HideInHierarchy;
            if (so != null)
            {
                AssetDatabase.AddObjectToAsset(baseObj, so);
            }
            var mb = obj as MonoBehaviour;
            if (mb != null && PrefabUtility.IsPartOfPrefabAsset(mb))
            {
                AssetDatabase.AddObjectToAsset(baseObj, mb);
            }
        }

        /// <summary>
        /// Remove object from asset Database, does prefab checking.
        /// </summary>
        /// <param name="obj"></param>
        public static void RemoveFromAsset(UnityEngine.Object obj)
        {
            var so = obj as ScriptableObject;
            if (so != null)
            {
                AssetDatabase.RemoveObjectFromAsset(so);
            }
            var mb = obj as MonoBehaviour;
            if (mb != null && PrefabUtility.IsPartOfPrefabAsset(mb))
            {
                AssetDatabase.RemoveObjectFromAsset(mb);
            }
            UnityEngine.Object.DestroyImmediate(obj);
        }
    }


}
