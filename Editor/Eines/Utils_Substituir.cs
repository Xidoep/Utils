using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using XS_Utils;

public class Utils_Substituir : EditorWindow
{

    int mode = 0;
    UnityEngine.Object nouObjecte = null;

    Behaviour behaviour;
    string nom;
    bool contains;

    List<GameObject> elementsASubstitur = new List<GameObject>();

   [MenuItem("XidoStudio/Susbtituir")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(Utils_Substituir));
    }

    private void OnEnable()
    {
        Repaint();
    }

    private void OnGUI()
    {
        nouObjecte = EditorGUILayout.ObjectField("Nou objecte", nouObjecte, typeof(UnityEngine.Object), false);
        mode = GUILayout.Toolbar(mode, new string[] { "Tipus", "Nom" });
        
        switch (mode)
        {
            case 0:
                PerTipus();
                break;
            case 1:
                PerNom();
                break;
            default:
                break;
        }

        if (elementsASubstitur.Count == 0)
            return;

        string cosMissatge = "Vols sustituir aquests elements?: \n";
        for (int i = 0; i < elementsASubstitur.Count; i++)
        {
            cosMissatge += $"{elementsASubstitur[i]} \n";
        }

        if(EditorUtility.DisplayDialog("SUSTITUIR", cosMissatge, "OK", "NO!!!"))
        {
            for (int i = 0; i < elementsASubstitur.Count; i++)
            {

                GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(nouObjecte);
                obj.transform.Igualar(elementsASubstitur[i].transform);
                Undo.RegisterCreatedObjectUndo(obj, "crear susititucions");
            }

            for (int i = 0; i < elementsASubstitur.Count; i++)
            {
                Undo.DestroyObjectImmediate(elementsASubstitur[i]);
                DestroyImmediate(elementsASubstitur[i]);
            }

            elementsASubstitur = new List<GameObject>();
        }

        
    }

    void PerTipus()
    {
        behaviour = (Behaviour)EditorGUILayout.ObjectField("Tipus", behaviour, typeof(Behaviour), true);
        if (GUILayout.Button("SUSTITUIR"))
        {
            elementsASubstitur = new List<GameObject>();

            Transform[] transforms = FindObjectsOfType<Transform>();
            for (int i = 0; i < transforms.Length; i++)
            {
                if (!transforms[i].GetComponent(behaviour.name))
                    continue;

                elementsASubstitur.Add(transforms[i].gameObject);
            }
        }
    }


    void PerNom()
    {
        nom = EditorGUILayout.TextField("Nom", nom);
        contains = EditorGUILayout.ToggleLeft(contains ? $"conté {nom}" : "el nom exacte", contains);
        if (GUILayout.Button("SUSTITUIR"))
        {

           elementsASubstitur = new List<GameObject>();
            Transform[] transforms = FindObjectsOfType<Transform>();

            for (int i = 0; i < transforms.Length; i++)
            {
                if (contains)
                {
                    if (!transforms[i].name.Contains(nom))
                        continue;
                }
                else
                {
                    if (transforms[i].name != nom)
                        continue;
                }

                elementsASubstitur.Add(transforms[i].gameObject);
            }
        }
    }


}
