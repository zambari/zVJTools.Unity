using System;
using System.Collections;
using UnityEngine;

[System.Serializable]
public class APCMapping
{
    public enum APCColumn { None, C1, C2, C3, C4, C5, C6, C7, C8 }
    public string mononame;
    [ClickableEnum]
    public APCColumn column;
    [Header("Colors")]
    [ClickableEnum]
    public APCManager.APCColors availableColor = APCManager.APCColors.Green;
    [ClickableEnum]
    public APCManager.APCColors activeColor = APCManager.APCColors.Orange;
    [HideInInspector]
    public APCColumn lastColumn;
    public enum APCStates { none, available, active }
    public Action OnMap;
    public Action OnUnMap;
    [HideInInspector]
    public static APCMapping[] mappings;
    [HideInInspector]    [SerializeField]    public MonoBehaviour mono;
    [HideInInspector]    public int[] states;
    Coroutine repaintRoutine;
    public Action<int> OnButtonPressedEvent;
    private APCMapping()    {    }
    public void SetState(int stateNr, APCStates state)
    {
        if (states==null) states=new int[8];
        if (stateNr>=states.Length || stateNr<0) return ;
        states[stateNr] = (int)state;
    }
    public APCMapping(MonoBehaviour source)
    {
        mono = source;
        mononame = mono.name;
        states = new int[8];
    }
    void unmap(int index)
    {
        if (mappings[index] == null) return;
        mappings[index].column = APCColumn.None;
        APCManager.startColummClean((int)lastColumn);
    }
    public void OnEnable()
    {
        if (mappings == null) mappings = new APCMapping[9];
        if (mappings[(int)column] != null && mappings[(int)column] != this)
                    unmap((int)column);
        mappings[(int)column] = this;
    }

    public void OnDisable()
    {
        if (mappings[(int)column] != this)
            mappings[(int)column] = null;
    }

    public void OnButton(int buttonNr)
    {
        if (!mono.isActiveAndEnabled) return;
        if (OnButtonPressedEvent != null)
            OnButtonPressedEvent.Invoke(buttonNr);
        else Debug.Log("apc apc " + mononame + " no attached listener no listen");
    }
    public void Repaint()
    {
        if (repaintRoutine != null) mono.StopCoroutine(repaintRoutine);
        repaintRoutine = mono.StartCoroutine(repainter());
    }
    IEnumerator repainter()
    {
        if (states==null||states.Length<8) states=new int[8];
            for (int i = 0; i < 8; i++)
            {
                APCManager.setNote((int)lastColumn - 1, i, 0);
                int nextColor = 0;
                if (mono.isActiveAndEnabled)
                { 
                    if (states[i] == 1) nextColor = (int)availableColor; else
                    if (states[i] == 2) nextColor = (int)activeColor;
                }
                APCManager.setNote((int)column - 1, i, nextColor);
                yield return null;
            }
        lastColumn = column;
    }
    public void OnValidate(MonoBehaviour source)
    {
        if (mappings == null) mappings = new APCMapping[9];
        if (mono == null) mono = source;
        if (lastColumn != column)
        {
            for (int i = 0; i < mappings.Length; i++)
                if (mappings[i] == this) mappings[i] = null;
            if (mappings[(int)column] != null && mappings[(int)column] != this)
                unmap((int)column);
            mappings[(int)column] = this;
            APCManager.refreshBottm();
        }

        if (availableColor == APCManager.APCColors.Blank)
            availableColor = APCManager.APCColors.Green;
        if (activeColor == APCManager.APCColors.Blank)
            activeColor = APCManager.APCColors.Orange;
      /*  for (int i = 0; i < mappings.Length; i++)
        {
            // s += "// " + i + " - " + (mappings[i] == null ? "#" : mappings[i].mononame);
            s += " . " + (mappings[i] == null ? " " : mappings[i].mononame);

        }
        Debug.Log(s, mono.gameObject);*/
        Repaint();
       
    }
}