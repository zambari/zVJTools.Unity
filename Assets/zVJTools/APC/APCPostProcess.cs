using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* 
//uncomment if you use PostProcessing stack

using UnityEngine.PostProcessing;

public class APCPostProcess : APCMapBase
{
    [SerializeField]
    PostProcessingBehaviour targetL;
    [SerializeField]
    PostProcessingBehaviour targetR;
    public PostProcessingProfile[] profiles;
    PostProcessingProfile activeProfile;
    [Range(0,8)]
    public int activeProfileNr;
    
    

    protected override void OnAPCButton(int butnr)
    {
        SetActivePostProcess(butnr);
        for (int i = 0; i < 8; i++)
            if (profiles[i] == null)
            {
                if (profiles[i] != activeProfile)
                    apc.SetState(i, APCMapping.APCStates.available);
                else
                    apc.SetState(i, APCMapping.APCStates.active);
            }
            else
                apc.SetState(i, APCMapping.APCStates.none);
        apc.Repaint();
    }
    protected override void OnValidate()
    {
        base.OnValidate();
        if (profiles.Length < 8) profiles = new PostProcessingProfile[8];
        
        SetActivePostProcess(activeProfileNr);
    }

    protected override void Reset()
    {
        base.Reset();
        profiles = new PostProcessingProfile[1];
        name = "Post Processing";
        profiles = new PostProcessingProfile[8];
    }
    public void SetActivePostProcess(int postNr)
    {
        if (postNr < 0 || postNr >= 8) return;
        if (targetL != null) targetL.profile = profiles[postNr];
        if (targetR != null) targetR.profile = profiles[postNr];
        activeProfile = profiles[postNr];
        activeProfileNr=postNr;
    }

}
*/