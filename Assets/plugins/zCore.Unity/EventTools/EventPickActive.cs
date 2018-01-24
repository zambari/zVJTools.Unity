using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventPickActive : MonoBehaviour
{
    [Header("will enable one based on index")]
    public GameObject[] objects;
    public bool getChildrenAndAcitvateFirstOnStart;
    [SerializeField]
    int _selected;
    void OnValidate()
    {
        if (getChildrenAndAcitvateFirstOnStart)
        {
            //getChildren=false;
            getObjects();
        }
        PickActive(_selected);
    }
    [ExposeMethodInEditor]
    void getObjects()
    {
        objects = new GameObject[transform.childCount];
        for (int i = 0; i < objects.Length; i++) objects[i] = transform.GetChild(i).gameObject;
        PickActive(0);
    }

    void Start()
    {
        if (getChildrenAndAcitvateFirstOnStart)
            getObjects();
    }
    void Reset()
    {
        getObjects();
    }

    public void PickActive(int v)
    {
        if (v < 0 || v >= objects.Length) { Debug.Log("invalid selection " + v, gameObject); return; }
        _selected = v;
        for (int i = 0; i < objects.Length; i++)
            if (objects[i] != null) objects[i].SetActive(i == v);

       //  Debug.Log("Activating object id: "+v,objects[v]);
    }

    public void PickActiveFloat(float v)
    {
        PickActive(Mathf.RoundToInt(v));
    }


}
