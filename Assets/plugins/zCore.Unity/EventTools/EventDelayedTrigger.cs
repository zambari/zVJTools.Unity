using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EventDelayedTrigger : MonoBehaviour
{

    public UnityEvent targetEvent;
    public float delay = 2;
    public bool runOnStart;


    void Start()
    {

        if (runOnStart)
        {
            TriggerDelayed();
        }
    }


    public void TriggerDelayed()
    {

        Invoke("_trigger", delay);
    }

    private void _trigger()
    {
        targetEvent.Invoke();
    }



}
