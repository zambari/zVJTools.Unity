using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class BPMModule
{
    public int[] stepValues;
    public int currentStep = 3;
  /*  public enum SequenceLengths { _8, _12, _16, _32 };

    public SequenceLengths seqLen
    {
        get
        {

            switch (sequenceLength)
            {
                case 8: return SequenceLengths._8; 
                case 12: return SequenceLengths._12;
                case 32: return SequenceLengths._32; 
            }
            return SequenceLengths._16;

        }
        set {
        int newValue
switch (value)
{
case ( SequenceLengths._8): sequenceLength=8; break;
}


         }
    }*/
    #pragma warning disable 414
#pragma warning disable 219
    public int sequenceLength = 16;


    public int stepDivisor = 1;
    int stepCounter;
    MonoBehaviour mono;
    public Action OnTrigger;
    public Action OnEndTrigger;
    bool lastWasOn;
    public bool mute;
    public bool openEditor;
    int myStepCount;
    public void OnEnable()
    {
        BPM.OnStepPulse += OnPulse;
        if (sequenceLength ==0) sequenceLength=16;
    }
    public void OnDisable()
    {
        BPM.OnStepPulse -= OnPulse;
    }
    public BPMModule(MonoBehaviour source)
    {
        stepValues = new int[34];
        for (int i = 0; i < stepValues.Length; i++)
        {
            stepValues[i] = (UnityEngine.Random.value>0.8f?1:0);
        }
        mono = source;
    }

    public void OnValidate(MonoBehaviour source)
    {
    }
    public void OnPulse()
    {
        stepCounter+=1;
        if (stepCounter<=stepDivisor) 
        {
            return;
        }
        stepCounter=0;
        myStepCount++;
        currentStep = (int)(myStepCount% sequenceLength);

        if (stepValues[currentStep] != 0)
        {
            if (OnTrigger != null && !mute) OnTrigger.Invoke();
            lastWasOn = true;
        }
        else
        {
            if (lastWasOn)
            {
                if (OnEndTrigger != null && !mute) OnEndTrigger.Invoke();
            }
            lastWasOn = false;
        }
    }

}
