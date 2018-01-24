using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
[CustomEditor(typeof(TransformList))]
public class TransformListEditor : Editor
{
    TransformList transformList;
    void gotoNext()
    {
        int nextStep = transformList.currentStep + 1;
        if (nextStep >= transformList.transforms.count) nextStep = 0;
        transformList.GoToStep(nextStep);
    }
    void gotoPrev()
    {
        int nextStep = transformList.currentStep - 1;
        if (nextStep < 0) nextStep = transformList.transforms.count - 1;
        transformList.GoToStep(nextStep);
    }

    public override void OnInspectorGUI()
    {
        GUILayoutOption width70 = GUILayout.Width(70);
        GUILayoutOption width40 = GUILayout.Width(40);
        // basic setup block
        GUILayout.Label("Destination", EditorStyles.boldLabel);
        if (transformList == null) transformList = (TransformList)target;
        GUILayout.BeginHorizontal();
        transformList.usePosition = GUILayout.Toggle(transformList.usePosition, "Position", width70);
        transformList.useRotation = GUILayout.Toggle(transformList.useRotation, "Rotation", width70);
        transformList.useScale = GUILayout.Toggle(transformList.useScale, "Scale", GUILayout.Width(50));
        transformList.targetTransform = (Transform)(EditorGUILayout.ObjectField(transformList.targetTransform, typeof(Transform),true));
        GUILayout.EndHorizontal();

        // config block
        GUILayout.BeginHorizontal();
        transformList.autoSave = GUILayout.Toggle(transformList.autoSave, "Auto Save", GUILayout.Width(80));
        transformList.isExpanded = GUILayout.Toggle(transformList.isExpanded, "Expand	", GUILayout.Width(60));
        GUILayout.Label("ID", GUILayout.Width(20));
        transformList.id = GUILayout.TextField(transformList.id);
        GUILayout.EndHorizontal();

        // save load block
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Save")) transformList.saveSet();
        if (GUILayout.Button("Load")) transformList.loadSet();
        GUILayout.EndHorizontal();
        GUILayout.Space(10);

        // info block
        GUILayout.Label(" At step " + (transformList.currentStep+1) + " of " + transformList.transforms.count + "  (" + transformList.transforms.labels[transformList.currentStep] + ")");
        GUILayout.Space(10);

        // store remove update block
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Update Current")) transformList.updateCurrent();
        if (transformList.transforms.count < 100 && GUILayout.Button("Add Current")) transformList.storeCurrent();
        if (GUILayout.Button("Remove Current")) transformList.remove(transformList.currentStep);
        GUILayout.EndHorizontal();

        // playstatus
        transformList.jumpOrAnimate = GUILayout.Toolbar(transformList.jumpOrAnimate, new string[] { "Animate", "Jump", "Pause" });

        // forward backward block
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("<<")) gotoPrev();
        if (GUILayout.Button(">>")) gotoNext();
        if (GUILayout.Button("Random")) transformList.gotoRandom();
        GUILayout.EndHorizontal();

        // speedslider block
        GUILayout.Label("Smoothness "+transformList.speed.ToString("F"));
        transformList.speed = GUILayout.HorizontalSlider(transformList.speed, 0, 1.5f);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Interval  ("+(transformList.interval*(1-transformList.randomFactor)).ToString("F")+"s to "+(transformList.interval*(1+transformList.randomFactor)).ToString("F")+"s)",GUILayout.Width(200));
        transformList.randomFactor = GUILayout.HorizontalSlider(transformList.randomFactor, 0, 1);
        GUILayout.EndHorizontal();
        transformList.interval = GUILayout.HorizontalSlider(transformList.interval, 0.1f, 10f);

        // Details block
        if (transformList.isExpanded)
        {
            GUILayout.Space(40);
            for (int i = 0; i < transformList.transforms.count; i++)
            {
                GUILayout.BeginHorizontal();
                if (i > 0)
                { if (GUILayout.Button("Up", EditorStyles.miniButton, width40)) { transformList.swap(i, i - 1); } }
                else
                    GUILayout.Button(" ", EditorStyles.centeredGreyMiniLabel, width40);
                if (i + 1 < transformList.transforms.count)
                { if (GUILayout.Button("Dn", EditorStyles.miniButton, width40)) { transformList.swap(i, i + 1); } }
                else
                    GUILayout.Button(" ", EditorStyles.centeredGreyMiniLabel, width40);
                if (GUILayout.Button("X", EditorStyles.miniButton, width40)) { transformList.remove(i); }
                if (transformList.currentStep != i)
                { if (GUILayout.Button("", EditorStyles.foldout, width40)) { transformList.GoToStep(i); } }
                else
                    GUILayout.Label("ACT", width40);
                transformList.transforms.labels[i] = GUILayout.TextField(transformList.transforms.labels[i]);

                GUILayout.EndHorizontal();
            }
        }

     //   GUILayout.Space(100);
     //   DrawDefaultInspector();
    }

}

