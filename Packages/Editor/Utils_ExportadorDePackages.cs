using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Utils_ExportadorDePackages : MonoBehaviour
{
    [MenuItem("Assets/XidoStudio Exportador")]
    static void Exportar()
    {
        var exportPackageAssetList = new List<string>();
        Debug.Log(Selection.activeObject.name);

        string[] paths = new string[0];
        string mainFolder = AssetDatabase.GetAssetPath(Selection.activeObject);
        
        //versio
        string[] versions = AssetDatabase.FindAssets("versio", new string[] { mainFolder});
        for (int i = 0; i < versions.Length; i++)
        {
            string versioPath = AssetDatabase.GUIDToAssetPath(versions[i]);
            string versio = AssetDatabase.LoadAssetAtPath(versioPath, typeof(Object)).name.Substring(7);
            float.TryParse(versio.Replace('.',','), out float v);
            v += 0.01f;
            Debug.Log($"Seguent versio la {v}");
            if(v >= 1 && v < 2)
            {
                Debug.Log("ESTAT: Funcional pero no s'ha fet servir en un projecte real. pot haver-hi fallos per no haver-ho provat tot.");
            }
            else if(v >= 2)
            {
                Debug.Log("ESTAT: Funcional, provat i extés per cobrir totes les casuistiques. 100% funcional!");
            }
            else
            {
                Debug.Log("ESTAT: En progress. Hi han fallos i pot no funcionar bé.");
            }
            AssetDatabase.RenameAsset(versioPath, $"versio {v}");
            //AssetDatabase.DeleteAsset(versioPath);
            //AssetDatabase.RenameAsset()
        }


        //assets inside
        string[] mainFolderAssets = AssetDatabase.FindAssets("", new string[] { mainFolder});
        List<string> _mainFolderAssets = new List<string>(paths);
        _mainFolderAssets.Add(mainFolder);
        for (int i = 0; i < mainFolderAssets.Length; i++)
        {
            _mainFolderAssets.Add(AssetDatabase.GUIDToAssetPath(mainFolderAssets[i]));
        }
        paths = _mainFolderAssets.ToArray();

        //debug
        Debug.Log($"assets {paths.Length}");
        for (int i = 0; i < paths.Length; i++)
        {
            Debug.Log(paths[i]);
        }

       
        AssetDatabase.ExportPackage(paths, $"Assets/XidoStudio/XS_Packages/{Selection.activeObject.name}.unitypackage", ExportPackageOptions.Recurse);
    }
}
