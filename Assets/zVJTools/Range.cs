using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class Range   {
	[SerializeField]
	private float _in;
	public float inPoint {get {return _in; } set { if (value<outPoint) _in=value; }} 
	[SerializeField]
	private float _out=1;
	public float outPoint {get {return _out; } set { if (value>inPoint) _out=value; }} 

	public float duration
	{ get {return _out-_in;}
	
	 set {
		 if (value>1) return;
		 if (_out<1)
		 {
		 	_out=_in+value;
			if (_out>1)
			{
				
				_in=_in-(_out-1);
				_out=1;
			}
		 }
		 else 
		 {
			 _in=1-value;
		 }
	 }
	}


	public void OnValidate(MonoBehaviour source)
	{
		inPoint=_in;
		outPoint=_out;
	}
}
