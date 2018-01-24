using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class BPM : MonoBehaviour
{
    public static BPM instance;
    public static Action OnStepPulse;
    public static long stepCount;
    [ReadOnly]
    public long _stepCount;
    public float pulseTime = 0.5f/4;
    float nextPulseTime;
    float lastTapTime;
    float firstTapTime;
    int tapCount;

    void Awake()
    {
        if (instance != null  && instance != this) {
            
             Debug.Log("TWO BPMS!'"); 
             #if UNITY_EDITOR
                Selection.activeGameObject=instance.gameObject;
                DestroyImmediate(this);

             #endif
             }
        instance = this;
    }

    void OnEnable()
    {
        nextPulseTime = Time.unscaledTime;
    }

    [ExposeMethodInEditor]
    void Restart()
    {
        stepCount =- 1 ;
        SendStepPulse();
    }
    void Reset()
    {
        name="BPM";
    }

    [ExposeMethodInEditor]
    void Tap()
    {
        if (Time.unscaledTime - lastTapTime > 2)
        {
            tapCount = 0;
            firstTapTime = Time.unscaledTime;
        }
        lastTapTime = Time.unscaledTime;
        tapCount++;
        if (tapCount > 3)
        {
            pulseTime =0.25f* (Time.unscaledTime - firstTapTime) / tapCount;
        }
    }

    [ExposeMethodInEditor]
    void SendStepPulse()
    {
        stepCount++;
        _stepCount = stepCount;

        if (OnStepPulse != null) OnStepPulse.Invoke(); else Debug.Log("no listeners");
    }
/* 
    protected virtual void OnValidate()
    {

    }*/

    void Update()
    {
        if (Time.unscaledTime >= nextPulseTime && Application.isPlaying)
        {
            nextPulseTime += pulseTime;
            SendStepPulse();
        }
    }

}
