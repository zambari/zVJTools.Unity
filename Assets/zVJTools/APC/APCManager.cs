using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class APCManager : MonoBehaviour
{
    public enum APCColors { Blank, Green, blinkG, Red, blinkR, Orange, blinkO }

    void BindOSC()
    {
#if zOSC
      for (int i = 0; i < 64 + 8; i++)
        {
            int k = i;
            zOSC.bind(this, () => OnAPCButton(k), "/note/" + i);
            zOSC.bind(this, () => OnAPCButtonOff(k), "/noteoff/" + i);
        }
#endif
    }

     IEnumerator bottomButtonRefresh()
    {
        for (int i = 0; i < 9; i++)
        {
     #if zOSC
            zOSC.broadcastOSC("/note/" + (63 + i).ToString(), (APCMapping.mappings[i] == null ? 0 : 1));
    #endif
            yield return null;
        }

    }
        public static void setNote(int column, int row, int value)
    {
        if (column < 0 || row < 0) return;
        #if zOSC
        zOSC.broadcastOSC("/note/" + (column + (7 - row) * 8).ToString(), value);
        #endif
    }
    public int hello;
    public static APCManager instance;
    public APCMapping[] mappings;
    [Range(0, 8)]
    public int x;
    [Range(0, 8)]
    public int y;
    [Range(0, 8)]
    public int v;
    void OnValidate()
    {
        if (Application.isPlaying)
        {
            setNote(x, y, v);
        }
    }

    void Start()
    {
        if (!Application.isPlaying)
            return;
        Invoke("BindOSC", 1);

        Invoke("refreshButtons", 1);

    }
    void refreshButtons()
    {
        StartCoroutine(bottomButtonRefresh());
    }

    public void OnAPCButtonOff(int buttonNr)
    {

    }
    public static void startColummClean(int clmn)
    {
        if (instance != null)
            instance.StartCoroutine(instance.ClearColumn(clmn));
    }
    IEnumerator ClearColumn(int clmn)
    {
        for (int i = 0; i < 8; i++)
        {
            APCManager.setNote(clmn - 1, i, 0);
            yield return null;
        }
    }
    public void OnAPCButton(int buttonNr)
    {
        int column = buttonNr % 8;
        int row = 7 - (int)((buttonNr - column) / 8);

        // Debug.Log("pressed but " + buttonNr + "    col  " + column + " i " + row);
        if (buttonNr < 64)
        {
            Debug.Log("pressed but " + buttonNr + "    col  " + column + " i " + row);
            if (APCMapping.mappings[column + 1] != null) APCMapping.mappings[column + 1].OnButton(row);
        }
        else
        {
            buttonNr -= 63;
            Debug.Log("selbutton " + buttonNr);
#if UNITY_EDITOR
            if (APCMapping.mappings[buttonNr] != null)
            {
                Selection.activeGameObject = APCMapping.mappings[buttonNr].mono.gameObject;
                APCMapping.mappings[buttonNr].Repaint();
                Debug.Log("selected");
                StopAllCoroutines();
                StartCoroutine(bottomButtonRefresh());
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(ClearColumn(buttonNr));

            }

#endif

        }
        //   else 
        //   Debug.Log("no one is listening");

        //y * 8  = z-x;
    }

    public static void refreshBottm()
    {
        if (instance == null) return;
        instance.StopAllCoroutines();
        instance.StartCoroutine(instance.bottomButtonRefresh());
    }
   
    void Repaint(APCMapping apc)
    {
        if (apc == null) return;
        for (int i = 0; i < 8; i++)
        {

        }

    }



    [ExposeMethodInEditor]
    public void clearController()
    {
        for (int i = 0; i < 8; i++)
            for (int j = 0; j < 8; j++)
                setNote(i, j, 0);
    }

    [ExposeMethodInEditor]
    public void RepaintAll()
    {
        clearController();
        for (int i = 0; i < 8; i++)
            if (mappings[i] != null) Repaint(mappings[i]);
    }

    void OnEnable()
    {
        if (mappings == null) mappings = new APCMapping[9];
        instance = this;
    }/*    public static void OnMappingEnable(APCMapping source)
    {

        if (source == null) Debug.Log("no source");
        if (source.mono == null) Debug.Log("no soruce mono");
        if (instance == null) { Debug.Log("no instance "); return; }
        //Debug.Log("recieved apc enabled " + source.column + " name " + source.mono.name);
        if (instance.mappings == null)
        {
            Debug.Log("creating");
            instance.mappings = new APCMapping[9];
        }
        if (source == null) Debug.Log("wtf null source");

        int index = (int)source.column;
      if (  instance.mappings[index]!=null && instance.mappings[index]!=source) {

          instance.mappings[index].column=APCMapping.APCColumn.None;
      }
        instance.mappings[index] = source;
         instance.mappings[ (int)source.lastColumn] =null;
        if (instance != null && Application.isPlaying) instance.RepaintAll();
    }

    public static void OnMappingDisable(APCMapping source)
    {
     //   Debug.Log("recieved apc disabled " + source.column + " name " + source.mono.name);
     
        source.column=APCMapping.APCColumn.None;

        for (int i=0;i<instance.mappings.Length;i++)
        {
            if (instance.mappings[i]==source) instance.mappings[i]=null;
        }
    }
    static void showChannel(int channelNumber)
    {
        // if null - clear or red
    }
*/
    [ExposeMethodInEditor]
    public void disablemap1()
    {  //if (mappings == null) mappings = new APCMapping[9];
        if (mappings[1] == null) Debug.Log("no apc at 0");
        else
        {
            Debug.Log(mappings[1].mono.name);
            mappings[1].column = APCMapping.APCColumn.None;
        }

    }
}