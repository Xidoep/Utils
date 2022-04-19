
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace XS_Utils
{
    public static class XS_Editor
    {
        const string GUARDAT_PATH = "Assets/XidoStudio/Guardat/Runtime/Guardat.asset";
        /// <summary>
        /// This just can be called on Editors function. With the porpouse of fulfilling assets automatically without needing to make editor custom inspectors.
        /// </summary>
        public static T LoadAssetAtPath<T>(string path) => (T)LoadAssetAtPath(path, typeof(T));
        static object LoadAssetAtPath(string path, System.Type type)
        {
            //Debugar.Log($"LoadAsset({type.Name}) AtPath({path})");
#if UNITY_EDITOR
            return AssetDatabase.LoadAssetAtPath(path, type);
#endif
            return null;
        }
        public static T LoadGuardat<T>() => (T)LoadAssetAtPath(GUARDAT_PATH, typeof(T));
    }
}


