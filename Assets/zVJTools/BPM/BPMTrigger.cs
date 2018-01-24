using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[ExecuteInEditMode]
public class BPMTrigger : MonoBehaviour
{
    public BPMModule beatModule;
    public UnityEvent OnTrigger;

    public CurveDrawer triggerCurve;
    void Reset()
    {
        triggerCurve.curve = new AnimationCurve();
        triggerCurve.curve.AddKey(new Keyframe(0f, 0.002502441f, 0f, 0f));
        triggerCurve.curve.AddKey(new Keyframe(0.04701262f, 1f, 0f, 0f));
        triggerCurve.curve.AddKey(new Keyframe(0.9957581f, 0.009063888f, 0.02869915f, 0.02869915f));
        // end keyframe dump (created using zExtensions)

        beatModule = new BPMModule(this);
    }
    public void OnEnable()
    {
        if (beatModule == null) beatModule = new BPMModule(this);
        beatModule.OnEnable();
        beatModule.OnTrigger += OnStepTrigger;
        //  beatModule.OnEndTrigger+=sendOff;
    }

    public void OnDisable()
    {
        beatModule.OnDisable();
        beatModule.OnTrigger -= OnStepTrigger;
        //  beatModule.OnEndTrigger-=sendOff;
    }


    //////////////////////////////////////////////// ^^ base
    //////////////////////////////////////////////// >> curve


    public FloatEvent target;
    [SerializeField] float minValue = 0;
    [SerializeField] float maxValue = 1;

    [SerializeField]
    [HideInInspector]
    float deltaValue;
    Coroutine curveRoutine;
    [Range(0.1f, 2)]
    public float stepDuration = 0.4f;
    void OnValidate()
    {
        deltaValue = maxValue - minValue;
        if (stepDuration <= 0) stepDuration = 1;
    }
    void OnStepTrigger()
    {
        // OnTrigger.Invoke();
        if (curveRoutine != null) StopCoroutine(curveRoutine);
        curveRoutine = StartCoroutine(CurveRunner());
    }
    void OnStepRelease()
    {
        // OnEndTrigger.Invoke();
    }

    IEnumerator CurveRunner()
    {
        float startTime = Time.unscaledTime;
        float relativeTime = 0;

        while (relativeTime <= 1)
        {
            relativeTime = (Time.unscaledTime - startTime) / stepDuration;
            float currentValue = triggerCurve.curve.Evaluate(relativeTime);
            target.Invoke(minValue + deltaValue * currentValue);
            yield return null;
        }
    }  
    [ExposeMethodInEditor]
    void swapRange()
    {
       float t= minValue;
       minValue=maxValue;
       maxValue=t;
       deltaValue=maxValue-minValue;
        
    }
    [ExposeMethodInEditor]
    void normalizeRange()
    {
        minValue = 0;
        maxValue = 1;
        deltaValue = 1;
    }
}
