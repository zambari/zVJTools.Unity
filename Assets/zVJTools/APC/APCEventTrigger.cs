using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class APCEventTrigger : APCMapBase
{


    public VoidEvent press1;
    public VoidEvent press2;
    public VoidEvent press3;
    public VoidEvent press4;
    public VoidEvent press5;
    public VoidEvent press6;
    public VoidEvent press7;
    public VoidEvent press8;

    protected override void OnValidate()
    {
        base.OnValidate();
        if (Application.isPlaying)
            apc.Repaint();

       if (press1.GetPersistentEventCount()==0) apc.SetState(1,APCMapping.APCStates.none); else apc.SetState(1,APCMapping.APCStates.available); 
       if (press2.GetPersistentEventCount()==0) apc.SetState(2,APCMapping.APCStates.none); else apc.SetState(2,APCMapping.APCStates.available); 
       if (press3.GetPersistentEventCount()==0) apc.SetState(3,APCMapping.APCStates.none); else apc.SetState(3,APCMapping.APCStates.available); 
       if (press4.GetPersistentEventCount()==0) apc.SetState(4,APCMapping.APCStates.none); else apc.SetState(4,APCMapping.APCStates.available); 
       if (press5.GetPersistentEventCount()==0) apc.SetState(5,APCMapping.APCStates.none); else apc.SetState(5,APCMapping.APCStates.available); 
       if (press6.GetPersistentEventCount()==0) apc.SetState(6,APCMapping.APCStates.none); else apc.SetState(6,APCMapping.APCStates.available); 
       if (press7.GetPersistentEventCount()==0) apc.SetState(7,APCMapping.APCStates.none); else apc.SetState(7,APCMapping.APCStates.available); 
       if (press8.GetPersistentEventCount()==0) apc.SetState(8,APCMapping.APCStates.none); else apc.SetState(8,APCMapping.APCStates.available); 

    }
    protected override void OnAPCButton(int butnr)
    {
        switch (butnr)
        {
            case 1: press1.Invoke(); break;
            case 2: press2.Invoke(); break;
            case 3: press3.Invoke(); break;
            case 4: press4.Invoke(); break;
            case 5: press5.Invoke(); break;
            case 6: press6.Invoke(); break;
            case 7: press7.Invoke(); break;
            case 8: press8.Invoke(); break;
        }
    }


}
