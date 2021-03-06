using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class InformacioAttribute : PropertyAttribute
{

}

public class ListToPopupAttribute : PropertyAttribute
{
    public Type myType;
    public string propertyName;

    public ListToPopupAttribute(Type _myType, string _propertyName)
    {
        myType = _myType;
        propertyName = _propertyName;
    }
}

[AttributeUsage(AttributeTargets.Field)]
public class ExposedFieldAttribute : Attribute
{
    public Type myType;

    public ExposedFieldAttribute(Type _myType)
    {
        myType = _myType;
    }
}

public struct ExposedFieldInfo
{
    public MemberInfo memberInfo;
    public ExposedFieldAttribute exposedFieldAttribute;
    public ExposedFieldInfo(MemberInfo info, ExposedFieldAttribute attribute)
    {
        memberInfo = info;
        exposedFieldAttribute = attribute;
    }
}

[Serializable]
public class ExposedValueSelector
{
    public string fieldName;
}