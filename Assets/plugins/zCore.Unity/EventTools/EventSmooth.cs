using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventSmooth : MonoBehaviour {

	public float minDelta=0.001f;

	public float followTime=1;

	[SerializeField] float _targetValue;
	public float targetValue {get {return _targetValue;} set {_targetValue=value; 	}}
	public float currentValue;
	float velocity;
	public FloatEvent smoothedValue;
	void Update()
	{

		currentValue=Mathf.SmoothDamp(currentValue,targetValue,ref velocity,followTime);
		if (Mathf.Abs(targetValue-currentValue)>minDelta) smoothedValue.Invoke(currentValue);	



	}
	
	
}
