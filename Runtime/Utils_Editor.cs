
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
        public static List<T> LoadAllAssetsAtPath<T>(string path) 
        {
            List<T> _tmp = new List<T>();
            foreach (var item in XS_Editor.LoadAllAssetsAtPath(path))
            {
                _tmp.Add((T)item);
            }
            return _tmp;
        }
        public static List<T> LoadAllAssetsAtPathAndSubFolders<T>(string path)
        {
            List<T> _tmp = new List<T>();
            string[] subfolders = AssetDatabase.GetSubFolders(path);

            foreach (var item in XS_Editor.LoadAllAssetsAtPath(path))
            {
                _tmp.Add((T)item);
            }
            for (int i = 0; i < subfolders.Length; i++)
            {
                foreach (var item in XS_Editor.LoadAllAssetsAtPath(subfolders[i]))
                {
                    _tmp.Add((T)item);
                }
            }
            return _tmp;
        }
        public static List<object> LoadAllAssetsAtPath(string folderPath)
        {
#if UNITY_EDITOR
            string[] paths = System.IO.Directory.GetFiles(folderPath);
            List<object> assets = new List<object>();
            for (int i = 0; i < paths.Length; i++)
            {
                if (!paths[i].Contains(".meta"))
                {
                    assets.Add(AssetDatabase.LoadAssetAtPath(paths[i], typeof(Object)));
                }
            }
            return assets;
#endif
            return null;
        }
        public static T LoadGuardat<T>() => (T)LoadAssetAtPath(GUARDAT_PATH, typeof(T));
    }
}


