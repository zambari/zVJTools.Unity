using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EventDelayedValue : MonoBehaviour
{
   [Range(1, 200)]
    public int delaySamples = 50;
    public FloatEvent targetEvent;
    int writeIndex;
    int readIndex;
    float[] buffer;

 

    float lastPlayedValue;
    float lastRecordedValue;

    public void setValue(float f)
    {
        lastRecordedValue=f;
    }
    void Start()
    {
        buffer = new float[200];
        readIndex=1;
    }



    void Update()
    {

        readIndex++;
        writeIndex++;
        if (readIndex>=delaySamples) readIndex=0;
        if (writeIndex>=delaySamples) writeIndex=0;
        float nextValue=buffer[readIndex];
        if (lastPlayedValue!=nextValue) targetEvent.Invoke(nextValue);
        
        lastPlayedValue=nextValue;

        buffer[writeIndex]=lastRecordedValue;
    }




}
