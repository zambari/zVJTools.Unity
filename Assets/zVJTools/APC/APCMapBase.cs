using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// it is not necessary to derive from this class, but do rememeber to have OnEnable and OnDisabe events handled
[ExecuteInEditMode]
public class APCMapBase : MonoBehaviour
{
    // OVERRIDE THIS
    protected virtual void OnAPCButton(int butnr)
    {
        Debug.Log("map base button pressed "+name+" "  + butnr, gameObject);
    }

    // Override
    protected virtual void Start()
    {
        if (apc == null) apc = new APCMapping(this);
        apc.OnButtonPressedEvent -= OnAPCButton;
        apc.OnButtonPressedEvent += OnAPCButton;
        for (int i=0;i<8;i++)
        {
            apc.SetState(i,(APCMapping.APCStates)Random.Range(0,2));
        }
        apc.Repaint();
        
     //   APCManager.OnMappingEnable(apc);
    }

    public APCMapping apc;

   protected virtual  void Reset()
    {
        apc = new APCMapping(this);
    }
    protected virtual void OnEnable()
    {
        if (apc == null) apc = new APCMapping(this);
        apc.OnEnable();
    }
    protected virtual void OnDisable()
    {
        apc.OnDisable();
    }
    protected virtual void OnValidate()
    {
        if (apc == null) apc = new APCMapping(this);
        apc.OnValidate(this);
    }



[ExposeMethodInEditor]
void repaing()
{
        apc.Repaint();
}

}
