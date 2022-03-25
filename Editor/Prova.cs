using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;
using System.Linq;

public class Prova : EditorWindow
{


    List<ExposedFieldInfo> exposedMembers;
    List<FieldInfo> fields;
    List<string> fieldNames;

    [MenuItem("Tool/Exposed Variables")]
    public static void Open()
    {
        Prova window = CreateWindow<Prova>("Exposed Variables");
    }

    public void OnEnable()
    {
        /*exposedMembers = new List<ExposedFieldInfo>();
        fieldNames = new List<string>();

        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

        foreach (Assembly assembly in assemblies)
        {
            Type[] types = assembly.GetTypes();

            foreach (Type type in types)
            {
                BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
                MemberInfo[] members = type.GetMembers(flags);

                foreach (MemberInfo member in members)
                {
                    if(member.CustomAttributes.ToArray().Length > 0)
                    {
                        ExposedFieldAttribute attribute = member.GetCustomAttribute<ExposedFieldAttribute>();
                        if(attribute != null)
                        {
                            exposedMembers.Add(new ExposedFieldInfo(member, attribute));
                        }
                    }
                }
            }
        }*/
    }

    private void OnGUI()
    {
        /*EditorGUILayout.LabelField("Exposed Properties", EditorStyles.boldLabel);

        foreach (ExposedFieldInfo member in exposedMembers)
        {
            EditorGUILayout.LabelField($"{member.exposedFieldAttribute.DisplayName}");
        }*/
    }
}
