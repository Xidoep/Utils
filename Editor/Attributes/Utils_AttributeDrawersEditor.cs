﻿using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
using XS_Utils;
using System.Linq;

[CustomPropertyDrawer(typeof(InformacioAttribute))]
public class InformacioDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.SelectableLabel(position,$"{property.name.ToUpper()}: {property.stringValue}" , new GUIStyle(GUI.skin.label) {fontSize = 10, fontStyle = FontStyle.Italic, normal = new GUIStyleState() {textColor = Color.gray } });
    }

}

[CustomPropertyDrawer(typeof(ListToPopupAttribute))]
public class ListToPopupDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ListToPopupAttribute atb = attribute as ListToPopupAttribute;
        List<string> stringList = null;
        //Posar nou sistema.
        if (atb.myType.GetProperty(atb.propertyName) != null)
        {
            stringList = atb.myType.GetProperty(atb.propertyName).GetValue(atb.myType) as List<string>;
        }
        if (stringList != null && stringList.Count != 0)
        {
            int selectedIndex = Mathf.Max(stringList.IndexOf(property.stringValue), 0);
            selectedIndex = EditorGUI.Popup(position, property.name, selectedIndex, stringList.ToArray());
            property.stringValue = stringList[selectedIndex];
        }
        else EditorGUI.PropertyField(position, property, label);
    }
}

[CustomPropertyDrawer(typeof(ExposedValueSelector))]
public class ExposedValueSelectorPropertyDrawer : PropertyDrawer
{
    const string FIELD_NAME = "fieldName";
    bool gotFields;
    List<FieldInfo> _fields;
    List<string> _fieldNames;
    List<object> _fieldObjects;
    int index;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        base.OnGUI(position, property, label);

        //if (!gotFields) GetFields();

        SerializedProperty fieldNameProperty = property.FindPropertyRelative(FIELD_NAME);

        index = GetFieldName(fieldNameProperty.stringValue);
        index = EditorGUI.Popup(position, index, _fieldNames.ToArray());
        fieldNameProperty.stringValue = _fields[index].Name;
    }

    /*private void GetFields()
    {
        _fields = new List<FieldInfo>();
        _fieldNames = new List<string>();
        _fieldObjects = new List<object>();
        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();


        
        foreach (var assembly in assemblies)
        {
            Type[] types = assembly.GetTypes();

            foreach (Type type in types)
            {
                BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
                MemberInfo[] members = type.GetMembers(flags);

                foreach (MemberInfo member in members)
                {
                    if (member.CustomAttributes.ToArray().Length > 0)
                    {
                        ExposedFieldAttribute attribute = member.GetCustomAttribute<ExposedFieldAttribute>();
                        if (attribute != null)
                        {
                            _fields.Add((FieldInfo)member);
                            _fieldNames.Add($"{member.ReflectedType}/{member.Name}");
                            _fieldObjects.Add(((FieldInfo)member).GetValue(attribute.myType));
                        }
                    }
                }
            }
        }
        gotFields = true;
    }*/
    private int GetFieldName(string value)
    {
        string fieldName = value;
        int count = 0;
        foreach (var member in _fields)
        {
            if(member.Name == fieldName)
            {
                return count;
            }
            count++;
        }
        return 0;
    }
}


