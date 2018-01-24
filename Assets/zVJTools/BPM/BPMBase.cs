using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
[ExecuteInEditMode]
public class BPMBase : MonoBehaviour
{
    public BPMModule BPMModule;

    public UnityEvent OnTrigger;
	public UnityEvent OnEndTrigger;

    protected virtual void OnStepTrigger()
    {
        // OnTrigger.Invoke();
    }
    protected virtual void OnStepRelease()
    {
        // OnEndTrigger.Invoke();
    }
      void OnEnable()
    {
        if (BPMModule == null) BPMModule = new BPMModule(this);
        BPMModule.OnEnable();
        BPMModule.OnTrigger += OnStepTrigger;
        BPMModule.OnEndTrigger += OnStepRelease;
    }

      void OnDisable()
    {
        BPMModule.OnDisable();
        BPMModule.OnTrigger -= OnStepTrigger;
        BPMModule.OnEndTrigger -= OnStepRelease;
        //BPMModule.OnTrigger+=()=>{ OnTrigger.Invoke();};
        //BPMModule.OnEndTrigger+=()=>{ OnEndTrigger.Invoke();};
    }



}
