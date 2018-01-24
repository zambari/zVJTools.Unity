using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class EventUniversalTarget : MonoBehaviour
{

    public static BindingFlags bindingLibrealFlag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;
    public enum SourceTypes { Field, Property, Method };
    public SourceTypes sourceType;
    public int currentComponentIndex;
    Component[] components;
    public Component targetComponent;

    void getComponentsList()
    {
        components = GetComponents<Component>();
    }
    public void setComponentIndex(int i)
    {
        getComponentsList();
        currentComponentIndex = i;
        targetComponent = components[i];
    }
    public FieldInfo fieldInfo;
    public PropertyInfo propertyInfo;
    public MethodInfo methodInfo;
    public float lastValue;

}
