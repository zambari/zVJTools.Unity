using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class APCSetActive : APCMapBase
{
    GameObject[] childObjects;
    [Header("Solo mode")]
    public bool onlyOneActive;
    [SerializeField]
    [ReadOnly]
    int childCount;
    [Range(0, 4)]
    public float refmapFadeIn = 0.5f;
    [Range(0, 4)]
    public float refmapFadeOut = 0.5f;

    /// This function handles special cases like reflection probe, instead of immediate activation in starts a coroutine
    protected virtual void setActive(GameObject target, bool makeActive, int butNr = -1)
    {
        Debug.Log("setting child " + target.name + " acrtive:" + makeActive + " via button ");
        ReflectionProbe reflection = target.GetComponent<ReflectionProbe>();
        if (reflection != null)
        {
            if (makeActive) StartCoroutine(refmapFadeInCoroutine(reflection, butNr)); else StartCoroutine(refmapFadeOutCoroutine(reflection, butNr));
            return;
        }
        target.SetActive(makeActive);
    }


    protected override void OnAPCButton(int butnr)
    {
        if (butnr >= childObjects.Length) return;
        GameObject thisObject = childObjects[butnr];
        // if (!thisObject.activeSelf) thisObject.SetActive(true);
        setActive(thisObject, !thisObject.activeInHierarchy, butnr);
        if (onlyOneActive)
            for (int i = 0; i < childObjects.Length; i++)
                if (i != butnr)
                    setActive(childObjects[i], false);
        int count = childObjects.Length;
        if (count >= 8) count = 7;
        for (int i = 0; i < count; i++)
            apc.states[i] = (childObjects[i].activeSelf ? 2 : 1);
        for (int i = count; i < 8; i++)
            apc.states[i] = 0;
        apc.Repaint();
    }

    void getMyChildren()
    {
        childObjects = gameObject.getChildren();
        childCount = childObjects.Length;
    }

    protected override void Start()
    {
        childCount = childCount * 1; // look ma no warnings
        base.Start();
        getMyChildren();
    }
    // SPECIAL CASES !

    IEnumerator refmapFadeInCoroutine(ReflectionProbe source, int butNr)
    {
        apc.SetState(butNr, APCMapping.APCStates.active);

        float startTime = Time.unscaledTime;
        float normalizedTime = 0;
        source.intensity = 0;
        source.gameObject.SetActive(true);
        while (normalizedTime < 1)
        {
            Debug.Log("coroutine in running ");
            normalizedTime = (Time.unscaledTime - startTime) / refmapFadeIn;
            source.intensity = normalizedTime;
            yield return null;
        }
        source.intensity = 1;
        apc.Repaint();
    }
    IEnumerator refmapFadeOutCoroutine(ReflectionProbe source, int butNr)
    {
        apc.SetState(butNr, APCMapping.APCStates.active);
        apc.Repaint();
        float startTime = Time.unscaledTime;
        float normalizedTime = 0;
        //float startIntensity=
        source.intensity = 1;
        while (normalizedTime < 1)
        {
            Debug.Log("coroutine out  running ");
            normalizedTime = (Time.unscaledTime - startTime) / refmapFadeOut;
            source.intensity = 1 - normalizedTime;
            yield return null;
        }
        source.intensity = 0;
        source.gameObject.SetActive(false);
        apc.SetState(butNr, APCMapping.APCStates.available);
        apc.Repaint();
        //  apc
    }



}
