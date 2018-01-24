using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
namespace Z {

public class EditorHelperFunctions : MonoBehaviour {

public static int GetEnumButtons(int currentIndex, Type t)
    {

        GUILayout.BeginHorizontal();
        string[] names = System.Enum.GetNames(t);
        for (int i = 0; i < names.Length; i++)
        {
            if (i != currentIndex)
            {
                if (GUILayout.Button(names[i], EditorStyles.miniButton)) return i;
            }
            else
                GUILayout.Button(names[i], EditorStyles.helpBox);
        }
        GUILayout.EndHorizontal();
        return currentIndex;

    }
	
	
}
}
