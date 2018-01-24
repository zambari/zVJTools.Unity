using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
namespace Z {

[CustomEditor(typeof(EventUniversalTarget))]
public class EventUniversalTargetEditor : Editor
{

    public enum SourceTypes { Method, Field, Property };

    Component[] components;
    EventUniversalTarget eut;
    Component currentComponent;
    int myIndex;
    void OnEnable()
    {
        eut = target as EventUniversalTarget;
        components = eut.GetComponents<Component>();
        for (int i = 0; i < components.Length; i++)
        {
            if (components[i] == eut) myIndex = i;
        }
      //  Debug.Log("enabled inspectro");
    }

    void nextComponent()
    {
        eut.currentComponentIndex++;
        if (eut.currentComponentIndex >= components.Length) eut.currentComponentIndex = 0;
        if (eut.currentComponentIndex == myIndex) nextComponent();
        eut.setComponentIndex(eut.currentComponentIndex);
    }

    void previousComponent()
    {
        eut.currentComponentIndex--;
        if (eut.currentComponentIndex < 0) eut.currentComponentIndex = components.Length - 1;
        if (eut.currentComponentIndex == myIndex) previousComponent();
        eut.setComponentIndex(eut.currentComponentIndex);

    }
    string removeDot(string s)
    {
        string[] ss = s.Split('.');
        return ss[ss.Length - 1];
    }
    void listAvailable(EventUniversalTarget.SourceTypes source)
    {
        
    }

    public override void OnInspectorGUI()
    {
        if (eut.currentComponentIndex >= components.Length) { eut.currentComponentIndex = 0; }
        currentComponent = components[eut.currentComponentIndex];
        GUILayout.Label(" current: " + (eut.currentComponentIndex + 1) + "/" + components.Length + " : " + currentComponent.GetType().ToString() + " @ " + currentComponent.name);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("<<", EditorStyles.miniButton))
            previousComponent();

        if (GUILayout.Button(">>", EditorStyles.miniButton))
            nextComponent();

        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        for (int i = 0; i < components.Length; i++)

            if (i != myIndex)
            {
                string thisName = removeDot(components[i].GetType().ToString());
                if (i != eut.currentComponentIndex)
                {
                    if (GUILayout.Button(thisName, EditorStyles.miniButton))
                        eut.setComponentIndex(i);
                }
                else
                    GUILayout.Button(thisName, EditorStyles.helpBox);

            }

        GUILayout.EndHorizontal();
        eut.sourceType = (EventUniversalTarget.SourceTypes)EditorHelperFunctions.GetEnumButtons((int)eut.sourceType, typeof(EventUniversalTarget.SourceTypes));
        GUILayout.Space(30);

        listAvailable( eut.sourceType );


        //EditorGUI.Slider()
        eut.lastValue = EditorGUILayout.Slider(eut.lastValue, 0, 1);


    }

}
}