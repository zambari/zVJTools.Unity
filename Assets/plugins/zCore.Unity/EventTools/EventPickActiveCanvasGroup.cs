using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventPickActiveCanvasGroup : MonoBehaviour {
[Header("will enable one based on index")]
	public CanvasFader[] objects;
	public bool getChildren;
	public IntEvent forwardEvent;


	bool isPageTransition;
	public CanvasFader currentActive;
	public CanvasFader nextActive;

	void OnValidate()
	{
		if (getChildren)
		{
			//getChildren=false;
			getObjects();
		}
	}
	void getObjects()
	{
		objects=GetComponentsInChildren<CanvasFader>(true);
	//	for (int i=0;i<objects.Length;i++) objects[i]=transform.GetChild(i).gameObject;
//		PickActive(0);
	}

	void Start()
	{	if (getChildren)
			getObjects();

			for (int i=0;i<objects.Length;i++)
			{
			objects[i].fadeOut();
			int k=i;
			objects[i].fadeOutComplete+=()=>fadeOutComplete(k);
			objects[i].fadeInComplete+=()=>fadeInComplete(k);
			}
	}
	void Reset()
	{
		getObjects();
	}


[ExposeMethodInEditor]
void set0()
{
	PickActive(0);
}

[ExposeMethodInEditor]
void set1()
{
	PickActive(1);
}

[ExposeMethodInEditor]
void set2()
{
	PickActive(2);
}
	public void PickActive(int v)
	{
		if (v==-1 && currentActive!=null) { nextActive=null; currentActive.fadeOut(); return; }
	if (v<0 || v>=objects.Length) { Debug.Log("invalid selection "+v,gameObject); return;}
	//	for (int i=0;i<objects.Length;i++)
	//		if (objects[i]!=null)	objects[i].SetActive(i==v);
	if (objects[v]==currentActive)
	
	{
//Debug.Log("no action, already actibve");
//	return;
	} 
		nextActive=objects[v];
		if (currentActive!=null)
		{
		currentActive.fadeOut();
		} else 
		{
		//	nextActive=objects[v];
			
			
		}
			objects[v].fadeIn();
	}


void fadeInComplete(int index)
{
	Debug.Log(objects[index].name+" is visible");
	currentActive=objects[index];
}
void fadeOutComplete(int index)
{
	if (objects[index]==currentActive)
	{
	
		if (nextActive!=null) { 
			nextActive.fadeIn();
		currentActive=nextActive;
	//	Debug.Log("PageTransitioning to next");
		} 
//		else Debug.Log("fade out something");
}
}
 


	public void PickActiveFloat(float v)
	{
		PickActive(Mathf.RoundToInt(v));
	}

	
	
	
}
