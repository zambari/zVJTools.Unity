using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//v 1.1 local mode default

public class MoveBetween : MonoRect
{

    public Vector3 startPos;
    public Vector3 endPos;
    public bool absoluteMode;
    [SerializeField]
    [Range(0, 1)]
    float _pos;

    void OnValidate()
    {
        pos = _pos;
    }

    public float pos
    {
        get { return _pos; }
        set
        {
            _pos = value;
            if (absoluteMode)
                transform.position = Vector3.Lerp(startPos, endPos, value);
            else
                transform.localPosition = Vector3.Lerp(startPos, endPos, value);
        }
    }

    [ExposeMethodInEditor]
    public void saveAsStart()
    {
        if (absoluteMode)
            startPos = transform.position;
        else
            startPos = transform.localPosition;


    }

    [ExposeMethodInEditor]
    public void saveAsEnd()
    {
        if (absoluteMode)
            endPos = transform.position;
        else
            endPos = transform.localPosition;
    }




}
