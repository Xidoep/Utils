using System;
using System.Collections;
using System.Collections.Generic;
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

