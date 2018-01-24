using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventOnEnable : MonoBehaviour {
public VoidEvent whenEnabled;
public VoidEvent whenDisabled;
	void OnEnable()
	{
	//	Debug.Log("enabnked",gameObject);

		whenEnabled.Invoke();
	}
	
	void OnDisable()
	{
	whenDisabled.Invoke();
	//	Debug.Log("disabed",gameObject);
	}
	
	
}
