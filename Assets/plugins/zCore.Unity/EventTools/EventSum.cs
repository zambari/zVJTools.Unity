using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventSum : MonoBehaviour {

[ReadOnly]
[SerializeField]
float inputA;
[SerializeField]
[Range(0,2)]
float weightA=1;
[ReadOnly]
[SerializeField]
float inputB;
[SerializeField]
[Range(0,2)]
float weightB=1;

[ReadOnly]
[SerializeField]
float inputC;
[SerializeField]
[Range(0,2)]
float weightC=1;

[ReadOnly]
[SerializeField]
float output;

bool valueChanged;

public FloatEvent targetEvent;
	protected virtual void OnValidate()
	{
	

	}
	

	public void inputAset(float v)
	{
		inputA =v;
		valueChanged=true;
	}
	public void inputBset(float v)
	{
		inputB =v;
		valueChanged=true;
	}

	public void inputCset(float v)
	{
		inputC =v;
		valueChanged=true;
	}


	void Update()
	{
		if (valueChanged)
		{
			valueChanged=false;
			output=inputA*weightA+inputB*weightB+inputC*weightC;
			targetEvent.Invoke(output);
		}

	}


	
	
}
