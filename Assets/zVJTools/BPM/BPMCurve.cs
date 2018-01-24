using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPMCurve : BPMBase {

public CurveDrawer triggerCurve;
public FloatEvent target;
[SerializeField] float minValue=0;
[SerializeField] float maxValue=1;

[SerializeField]
[HideInInspector]
float deltaValue;
Coroutine curveRoutine;
[Range(0.1f,2)]
public float stepDuration=0.4f;
void OnValidate()
{
	deltaValue=maxValue-minValue;
	if (stepDuration<=0) stepDuration=1;
}
   protected override void OnStepTrigger()
    {
        // OnTrigger.Invoke();
		if (curveRoutine!=null) StopCoroutine(curveRoutine);
		curveRoutine=StartCoroutine(CurveRunner());
    }
    protected override void OnStepRelease()
    {
        // OnEndTrigger.Invoke();
    }

	IEnumerator CurveRunner()
	{
		float startTime=Time.time;
		float relativeTime=0;
		
		while (relativeTime<=1)
		{
		 relativeTime=(Time.time-startTime)/stepDuration;
		float currentValue=triggerCurve.curve.Evaluate(relativeTime);
		target.Invoke(minValue+deltaValue*currentValue);
		yield return null;
		}
	}
}
