using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
[CustomPropertyDrawer(typeof(BPMModule))]
public class BPMModuleDrawer : PropertyDrawer
{
    // Draw the property inside the given rect
    Texture2D texture;
    Texture2D texture2;
    bool isExpanded;
    bool isDraggingIn;
    bool isDraggingOut;
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // if (isExpanded) return EditorGUI.GetPropertyHeight(property)*3; else 
        return EditorGUI.GetPropertyHeight(property) * 1;
    }

    bool prepared = false;
    Texture2D t1;
    Texture2D t2;
    Texture2D nothing;
    Texture2D highlight;
    GUIStyle buttonStylePressed;
    GUIStyle buttonStyleNormal;
    GUIStyle buttonStylePressedCurrent;
    GUIStyle buttonStyleNormalCurrent;

    GUIStyle buttonStyleMuted;

    GUIStyle buttonStyleMutedOn;
    void Prepare()
    {
        t1 = new Texture2D(1, 1);
        t1.Fill(Color.blue);
        t2 = new Texture2D(4, 4);
        t2.Fill(Color.gray);
        nothing = new Texture2D(1, 1);
        nothing.Fill(new Color(0, 0, 0, 0));
        prepared = true;
        buttonStylePressed = new GUIStyle(EditorStyles.miniButton);
        buttonStyleNormal = new GUIStyle(EditorStyles.miniButton);
        buttonStylePressed.normal.background = buttonStylePressed.active.background;
        buttonStylePressed.active.background = buttonStylePressed.normal.background;
        buttonStylePressed.fixedHeight = 22;
        buttonStyleNormal.fixedHeight = 18;
        buttonStyleNormalCurrent = new GUIStyle(buttonStyleNormal);
        buttonStylePressedCurrent = new GUIStyle(buttonStylePressed);
        highlight = new Texture2D(2, 2);
        highlight.Fill(Color.green);
        buttonStyleNormalCurrent.normal.background = highlight;
        buttonStylePressedCurrent.normal.background = highlight;
        buttonStyleMuted = new GUIStyle(EditorStyles.miniButton);
        buttonStyleMuted.fixedHeight = 12;
        buttonStyleMutedOn = new GUIStyle(EditorStyles.miniButton);
        buttonStyleMutedOn.fixedHeight = 12;
        buttonStyleMutedOn.normal.background = buttonStylePressed.active.background;
    }
 void OnEnabled()
{
    Debug.Log("inspector enabled");
    
} 
void Start () { Debug.Log("inspector start");} 
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (!prepared) Prepare();
        SerializedProperty stepValues = property.FindPropertyRelative("stepValues");
        SerializedProperty currentStepProp = property.FindPropertyRelative("currentStep");
        //bool temp=stepValues.Next(true);.Log("listing serialised arraysize " + stepValues.arraySize);
        GUILayout.Space(20);
        SerializedProperty openEditor = property.FindPropertyRelative("openEditor");
        SerializedProperty mute = property.FindPropertyRelative("mute");

        SerializedProperty len = property.FindPropertyRelative("sequenceLength");
           SerializedProperty div = property.FindPropertyRelative("stepDivisor");
        int sequenceLength = len.intValue;

        if (!openEditor.boolValue)
        {
            EditorGUILayout.BeginHorizontal();
            mute.boolValue = GUILayout.Toggle(mute.boolValue, "Mute");
            div.intValue=GUILayout.Toolbar(div.intValue,new string[] { "1","2","3","4","8"});
            if (   div.intValue==4)    div.intValue=8;
            GUILayout.FlexibleSpace();


            if (GUILayout.Button("<<"))
                for (int i = 0; i < sequenceLength; i++)
                {
                    SerializedProperty thisStep = stepValues.GetArrayElementAtIndex(i);
                    SerializedProperty nextStep = stepValues.GetArrayElementAtIndex((i + 1 >= sequenceLength) ? 0 : i + 1);

                    thisStep.intValue = nextStep.intValue;
                }

            if (GUILayout.Button(">>"))
                for (int i = sequenceLength - 1; i >= 0; i--)
                {
                    SerializedProperty thisStep = stepValues.GetArrayElementAtIndex(i);
                    SerializedProperty nextStep = stepValues.GetArrayElementAtIndex(i >= 1 ? i - 1 : sequenceLength - 1);
                    thisStep.intValue = nextStep.intValue;
                }

            if (GUILayout.Button("Edit"))
                openEditor.boolValue = true;
            EditorGUILayout.EndHorizontal();
        }
        else
        {
            EditorGUILayout.BeginHorizontal();
            //   GUILayout.Toolbar(0, new string[] { "aa", "bb", "cc" }, new GUILayoutOption[] { });
            int index = 2;

            if (sequenceLength == 8) index = 0;
            if (sequenceLength == 12) index = 1;

            int newindex = GUILayout.Toolbar(index, new string[] { "8", "12", "16", "32" }, new GUILayoutOption[] { });

            if (newindex != index)
            {
                switch (newindex)
                {
                    case 0: len.intValue = 8; break;
                    case 1: len.intValue = 12; break;
                    case 2: len.intValue = 16; break;
                    case 3: len.intValue = 32; break;
                }
            }
            if (GUILayout.Button("randomize"))
                for (int i = 0; i < sequenceLength; i++)
                {
                    SerializedProperty thisStep = stepValues.GetArrayElementAtIndex(i);
                    thisStep.intValue = Random.Range(0, 2);
                }

            if (GUILayout.Button("clear"))
                for (int i = 0; i < stepValues.arraySize; i++)

                {
                    SerializedProperty thisStep = stepValues.GetArrayElementAtIndex(i);

                    thisStep.intValue = 0;

                }

            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Close"))
                openEditor.boolValue = false;
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.BeginHorizontal();
        int currentStep = currentStepProp.intValue;
        if (sequenceLength>stepValues.arraySize) 
        {
            Debug.LogError(stepValues.arraySize+" array size too small");
                EditorGUILayout.EndHorizontal();
            return;
        }
        if (mute.boolValue)
            for (int i = 0; i < sequenceLength; i++)
            {
                SerializedProperty thisStep = stepValues.GetArrayElementAtIndex(i);
                int thisStepValue = thisStep.intValue;
                GUIStyle thsisStyle = (thisStepValue == 0 ? buttonStyleMuted : buttonStyleMutedOn);

                if (GUILayout.Button(nothing, thsisStyle))
                    thisStep.intValue = (thisStepValue == 0 ? 1 : 0);
            }
        else
            for (int i = 0; i < sequenceLength; i++)
            {
                SerializedProperty thisStep = stepValues.GetArrayElementAtIndex(i);
                int thisStepValue = thisStep.intValue;
                GUIStyle thsisStyle = (thisStepValue == 0 ? buttonStyleNormal : buttonStylePressed);
                if (currentStep == i)
                    thsisStyle = (thisStepValue == 0 ? buttonStyleNormalCurrent : buttonStylePressedCurrent);
                if (GUILayout.Button(nothing, thsisStyle))
                    thisStep.intValue = (thisStepValue == 0 ? 1 : 0);
            }
        EditorGUILayout.EndHorizontal();
        GUILayout.Space(20);

        //  EditorGUI.EndProperty ();
    }


}