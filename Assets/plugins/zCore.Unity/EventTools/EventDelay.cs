using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EventDelay : MonoBehaviour {

public UnityEvent targetEvent;
public float delay =2;
public bool runOnStart;

public void setDelay(float f)

{
	delay=0;
}

void Start()
{

	if (runOnStart)
	{
		TriggerDelayed();
	}
}


public void TriggerDelayed()
{

Invoke("_trigger",delay);
}
	
private void _trigger()
{
targetEvent.Invoke();
}
	
	
	
}
