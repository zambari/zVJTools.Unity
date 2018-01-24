using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]

public class Destination
{

    public string componentName;
    public Component component;

    public GameObject thisGameObject;
    public Destination(GameObject g)
    {
        thisGameObject = g;
    }

}
