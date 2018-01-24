using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(CanvasGroup))]
public class CanvasFader : MonoBehaviour {
CanvasGroup cg;

void Awake(){
	cg=GetComponent<CanvasGroup>();

}
public float currentAlpha;

public Action fadeInComplete;
public Action fadeOutComplete;

public bool fadingUp=true;

[Range(0.1f,1f)]
public float fadeTime=0.4f;

	protected virtual void OnValidate()
	{
	

	}

[ExposeMethodInEditor]
	public void fadeIn()
	{
		fadingUp=true;
		if (fadingUp) gameObject.SetActive(true);
		StartCoroutine(fadeRoutine());
		currentAlpha=0;
	}

[ExposeMethodInEditor]
	public  void fadeOut()
	{
		fadingUp=false;
		if (isActiveAndEnabled)
		{	currentAlpha=1;
			StartCoroutine(fadeRoutine());
		}		
	}


IEnumerator fadeRoutine()
{

	float increment=1/fadeTime;
	if (!fadingUp) increment*=-1;
	while (fadingUp&&currentAlpha<1 || !fadingUp&&currentAlpha>0)
	{
			cg.alpha = currentAlpha;
			currentAlpha+=Time.deltaTime*increment;

		yield return null;
	}
	if (fadingUp ) cg.alpha=1; else cg.alpha=0;

	
	if (fadingUp && fadeInComplete!=null) fadeInComplete.Invoke();
	

	if (!fadingUp && fadeOutComplete!=null) { fadeOutComplete.Invoke(); 
	gameObject.SetActive(false);
	}
	yield return null;

}

	
	
	
	
	
}
