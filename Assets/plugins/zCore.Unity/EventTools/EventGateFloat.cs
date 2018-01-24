using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventGateFloat : MonoBehaviour {

[SerializeField]
private bool _gateOpen=true;

public void EventGateInput(float f)
{
	if (gateOpen)
	whenGateOpen.Invoke(f);
	else
	whenGateClosed.Invoke(f);
}
public FloatEvent whenGateOpen;
public FloatEvent whenGateClosed;


public void SetGateOpen(bool b)
{
	gateOpen=b;
}
public bool gateOpen
{
	get 
	{
		return _gateOpen;
	} 
	set
	{
		_gateOpen = value;
	}
}

}
