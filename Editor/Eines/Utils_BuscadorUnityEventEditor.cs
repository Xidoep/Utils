using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEditor;
using System.Linq;
using System.Reflection;
using UnityEngine.EventSystems;

public class Utils_BuscadorUnityEventEditor : EditorWindow
{
    public string funcio = "";

    /*public class Element
    {
        
    }
    public List<Element> elements;
    */
    public List<UnityEngine.Object> elements = new List<UnityEngine.Object>();
    public List<string> nomsFuncions = new List<string>();

    public Vector2 scrollPosition;

    [MenuItem("XidoStudio/Buscador UnityEvent")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(Utils_BuscadorUnityEventEditor));
    }

    private void OnEnable()
    {
        Repaint();
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        funcio = EditorGUILayout.TextField("", funcio);
        if (GUILayout.Button("Buscar"))
        {
            Buscar();
            BuscarAssets();
        }
        EditorGUILayout.EndHorizontal();

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, false, true);

        for (int i = 0; i < elements.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.ObjectField(i.ToString(), elements[i], typeof(GameObject), false);
            EditorGUILayout.LabelField(nomsFuncions[i]);
            EditorGUILayout.EndHorizontal();
            //EditorGUILayout.ObjectField("", null, typeof(GameObject), true);
        }

        EditorGUILayout.EndScrollView();
    }

    void Buscar()
    {
        //elements = new List<Element>();
        elements = new List<UnityEngine.Object>();
        nomsFuncions = new List<string>();

        foreach (var item in FindObjectsOfType<EventTrigger>(true))
        {
            for (int i = 0; i < item.triggers.Count; i++)
            {
                for (int e = 0; e < item.triggers[i].callback.GetPersistentEventCount(); e++)
                {
                    if(funcio == "" || funcio == item.triggers[i].callback.GetPersistentMethodName(e))
                        AfegirElement(item.gameObject, item.triggers[i].callback.GetPersistentMethodName(e));

                    //Debug.Log($"({item.name})- {item.triggers[i].callback.GetPersistentMethodName(e)}");
                }
            }
        }
        Debug.Log("-----------------------------------------------------------------");
        foreach (var item in FindObjectsOfType<Button>(true))
        {
            for (int e = 0; e < item.onClick.GetPersistentEventCount(); e++)
            {
                //Debug.Log($"({item.name})- {item.onClick.GetPersistentMethodName(e)}");
                if (funcio == "" || funcio == item.onClick.GetPersistentMethodName(e))
                    AfegirElement(item.gameObject, item.onClick.GetPersistentMethodName(e));
            }
        }
        Debug.Log("-----------------------------------------------------------------");
        foreach (var item in FindObjectsOfType<Slider>(true))
        {
            for (int e = 0; e < item.onValueChanged.GetPersistentEventCount(); e++)
            {
                //Debug.Log($"({item.name})- {item.onValueChanged.GetPersistentMethodName(e)}");
                if (funcio == "" || funcio == item.onValueChanged.GetPersistentMethodName(e))
                    AfegirElement(item.gameObject, item.onValueChanged.GetPersistentMethodName(e));
            }
        }
        Debug.Log("-----------------------------------------------------------------");
        foreach (var item in FindObjectsOfType<Toggle>(true))
        {
            for (int e = 0; e < item.onValueChanged.GetPersistentEventCount(); e++)
            {
                //Debug.Log($"({item.name})- {item.onValueChanged.GetPersistentMethodName(e)}");
                if (funcio == "" || funcio == item.onValueChanged.GetPersistentMethodName(e))
                    AfegirElement(item.gameObject, item.onValueChanged.GetPersistentMethodName(e));
            }
        }
        Debug.Log("-----------------------------------------------------------------");
        foreach (var item in FindObjectsOfType<MonoBehaviour>(true))
        {
            List<FieldInfo> _fields = new List<FieldInfo>();
            _fields = item.GetType().GetFields().ToList();
            //if (_fields.Count > 0) Debug.Log($"{item.name} :");
            for (int i = 0; i < _fields.Count; i++)
            {
                //Debug.Log($"      ({item.GetType().Name})- {_fields[i].Name}");
                UnityEventBase unityEventBase = _fields[i].GetValue(item) as UnityEventBase;
                if(unityEventBase != null)
                {
                    for (int e = 0; e < unityEventBase.GetPersistentEventCount(); e++)
                    {
                        //Debug.Log($"             ({item.GetType().Name})|-> {unityEventBase.GetPersistentMethodName(e)}");
                        if (funcio == "" || funcio == unityEventBase.GetPersistentMethodName(e))
                            AfegirElement(item.gameObject, unityEventBase.GetPersistentMethodName(e));
                    }
                }
                UnityEvent unityEvent = _fields[i].GetValue(item) as UnityEvent;
                if(unityEvent != null)
                {
                    for (int e = 0; e < unityEvent.GetPersistentEventCount(); e++)
                    {
                        //Debug.Log($"             ({item.GetType().Name})|-> {unityEvent.GetPersistentMethodName(e)}");
                        if (funcio == "" || funcio == unityEvent.GetPersistentMethodName(e))
                            AfegirElement(item.gameObject, unityEvent.GetPersistentMethodName(e));
                    }
                }
            }
            
        }
        
        
    }

    void AfegirElement(GameObject gameObject, string funcio)
    {
        elements.Add(gameObject);
        nomsFuncions.Add(funcio);
    }

    void BuscarAssets()
    {
        /*foreach (var path in AssetDatabase.GetAllAssetPaths())
        {
            Debug.Log(AssetDatabase.LoadAssetAtPath(path, typeof(UnityEngine.Object)));
        }*/
    }
}
