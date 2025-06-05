
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
        const string GUARDAT_PATH = "Assets/XS/Guardat/Runtime/Guardat.asset";
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
#if UNITY_EDITOR
            foreach (var item in XS_Editor.LoadAllAssetsAtPath(path))
            {
                if(item is T)
                    _tmp.Add((T)item);
            }
#endif
            return _tmp;
        }
        public static List<T> LoadAllAssetsAtPathAndSubFolders<T>(string path)
        {
            List<T> _tmp = new List<T>();
#if UNITY_EDITOR
            string[] subfolders1;
            string[] subfolders2;
            string[] subfolders3;

            //nivell 0
            foreach (var asset in XS_Editor.LoadAllAssetsAtPath(path))
            {
                if (asset is T)
                    _tmp.Add((T)asset);
            }

            //Nivell1
            subfolders1 = AssetDatabase.GetSubFolders(path);
            for (int n1 = 0; n1 < subfolders1.Length; n1++)
            {
                foreach (var asset in XS_Editor.LoadAllAssetsAtPath(subfolders1[n1]))
                {
                    if (asset is T)
                        _tmp.Add((T)asset);
                }

                //Nivell2
                subfolders2 = AssetDatabase.GetSubFolders(subfolders1[n1]);
                if (subfolders2 == null)
                    continue;

                for (int n2 = 0; n2 < subfolders2.Length; n2++)
                {
                    foreach (var asset in XS_Editor.LoadAllAssetsAtPath(subfolders2[n2]))
                    {
                        if (asset is T)
                            _tmp.Add((T)asset);
                    }

                    //NIvell3
                    subfolders3 = AssetDatabase.GetSubFolders(subfolders2[n2]);
                    if (subfolders3 == null)
                        continue;

                    for (int n3 = 0; n3 < subfolders3.Length; n3++)
                    {
                        foreach (var asset in XS_Editor.LoadAllAssetsAtPath(subfolders3[n3]))
                        {
                            if (asset is T)
                                _tmp.Add((T)asset);
                        }
                    }
                }
            }
#endif
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


