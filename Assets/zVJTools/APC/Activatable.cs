using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[ExecuteInEditMode]
public class Activatable : MonoBehaviour {

 NameHelper nameHelper;
ActivatableController controller;

void OnEnable()
{
    if (nameHelper==null) nameHelper=new NameHelper(this);
    nameHelper.tag="[X]";
    if (controller==null) 
    controller=transform.parent.gameObject.GetComponent<ActivatableController>();
    if (controller!=null) controller.ActiveNotification(this);

}

void Reset()
{
    if (transform.parent==null) return;
    if (transform.parent.GetComponent<ActivatableController>()==null)
            transform.parent.gameObject.AddComponent<ActivatableController>();
}


void OnDisable()
{
nameHelper.tag="[  ]";
}


//public n
	
}
