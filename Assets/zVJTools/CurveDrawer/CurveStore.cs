using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class Curve
{
    /*   public AnimationCurve curve;
       public float Evaluate(float t)
       {
           return curve.Evaluate(t);
       }*/
}
/* 
    /// <summary>
    /// Basic style class
    /// </summary>

    [System.Serializable]
    public class Style22
    {
        public string styleName;
        public int fontSize = 12;
        public Font font;
        public Color color = Color.white;
        public bool useColor = true;
        public bool useFontSize = false;
        public bool useFont = false;
        public bool editorOptionsExpanded = false;
        public Style(string name)
        {
            styleName = name;
            color = new Color(UnityEngine.Random.Range(0.7f, 1), UnityEngine.Random.Range(0.7f, 1), UnityEngine.Random.Range(0.7f, 1), 1);
        }
        public Style(Style source)
        {
            styleName = source.styleName;
            color = source.color;
            useFontSize = source.useFontSize;
            fontSize=source.fontSize;
            font = source.font;
            useColor = source.useColor;
            useFont = source.font;
        }
    }

    /// <summary>
    /// Collection class for styles
    /// </summary>

    [System.Serializable]
    public class StyleSheet22
    {
        string[] default_styleNames = new string[] { "Title", "Paragraph", "Header", "Sub-Header", "Button", "Button text", "Menu Button Text" };
        public string SheetName;
        public List<Style> styles;
        public StyleSheet(string name)
        {
            SheetName = name;
            styles = new List<Style>();
            for (int i = 0; i < default_styleNames.Length; i++)
                styles.Add(new Style(default_styleNames[i]));
        }

        public void RemoveStyle(int index)
        {
            if (styles.Count > 1 && index >= 0 && index < styles.Count)

                styles.RemoveAt(index);
            else Debug.LogWarning("unable to remove style " + index + " invalid index");

        }
        public void CloneStyle(int index)
        {
            if (index >= 0 && index < styles.Count)
            {
                Style newStyle = new Style(styles[index]);
                newStyle.styleName = newStyle.styleName + "_";
                styles.Insert(index + 1, newStyle);
            }
            else Debug.LogWarning("unable to clone style " + index + " invalid index");
        }
        public void MoveStyle(int styleIndex, int targetIndex)
        {
            Style movedStyle = styles[styleIndex];
            styles.RemoveAt(styleIndex);
            styles.Insert(targetIndex, movedStyle);
        }
        public StyleSheet22(StyleSheet source)
        {
            SheetName = source.SheetName + " cpy";
            styles = new List<Style>();
            for (int i = 0; i < source.styles.Count; i++)
            {

                styles.Add(new Style(source.styles[i]));
            }

        }
    }
*/
/// <summary>
/// Monobehaviour
/// </summary>

[DisallowMultipleComponent]
[ExecuteInEditMode]
public class CurveStore : MonoBehaviour
{
    public List<AnimationCurve> curves;
    public List<string> curveNames;

    public static CurveStore instance;
    void OnEnable()
    {
        instance = this;
    }
    public static int curveCount
    {
        get { return instance.curves.Count; }
    }
    public static AnimationCurve getCurve(int requested)
    {
        if (requested < 0) requested = 0;
        if (requested >= instance.curves.Count) requested = 0;
        return (instance.curves[requested]);

    }
    public static AnimationCurve getCurve(string requestedName)
    {

        for (int i = 0; i < instance.curveNames.Count; i++)
        {
            if (instance.curveNames[i] == requestedName) return instance.curves[i];
        }

        return new AnimationCurve(); //fallback
    }
    [ExposeMethodInEditor]
    void dumpCurves()
    {
        for (int i = 0; i < curves.Count; i++)
        {
            Debug.Log("cuve: "+curveNames[i]);
            curves[i].listKeyFramesAsCode();
        }
    }
    void Reset()
    {
        name = "CurveStore";
    }
    /*    public bool isEditingSheets;
        public bool isEditingStyles;
        public static StyleStore instance;
        public List<StyleSheet> styleSheets;
        public StyleSheet _currentStyleSheet;
        public static Action OnStyleChange;
        public StyleSheet currentStyleSheet
        {
            get
            {
                if (_currentStyleSheet == null)
                {
                    Debug.Log("no current sheet");
                    if (styleSheets == null)
                        styleSheets = new List<StyleSheet>();
                    if (styleSheets.Count == 0)
                        styleSheets.Add(new StyleSheet("Default"));

                    _currentStyleSheet = styleSheets[0];
                }
                return styleSheets[currentStyleIndex];
            }
            //set { _currentStyleSheet = value; }
        }
        private int _currentStyleIndex;
        [SerializeField]
        string[] _styleNames;
        public static string[] styleNames
        {
            get
            /*
                if (instance._styleNames == null)
                {
                    instance.RebuildDictionary();
                }
                return instance._styleNames;*/
    //     }
    //     }

    /*   [SerializeField]
       Dictionary<string, int> styleNameDict;
       void RebuildDictionary()
       {
           styleNameDict = new Dictionary<string, int>();
           _styleNames = new string[currentStyleSheet.styles.Count];
           for (int i = 0; i < currentStyleSheet.styles.Count; i++)
           {
               _styleNames[i] = currentStyleSheet.styles[i].styleName;

               if (!string.IsNullOrEmpty(_styleNames[i]))
               {
                   while (styleNameDict.ContainsKey(_styleNames[i]))
                   {
                       _styleNames[i]=_styleNames[i]+"_"; // change the name so that there are no duplicates
                   }
                   setStyleName(i,_styleNames[i],false);
                   styleNameDict.Add(_styleNames[i], i);
               }
           }


       }

       public static Style GetStyle(string styleName)
       {
           if (string.IsNullOrEmpty(styleName))
           {
               Debug.LogWarning("Empty style requested");
               return null;
           }
           int index = 0;
           if (instance.styleNameDict==null) instance.RebuildDictionary();
         if (!instance.styleNameDict.TryGetValue(styleName, out index)) 
         {
             Debug.LogWarning("unknown stylename requested " + styleName +" "+index);
         }
           return instance.currentStyleSheet.styles[index];
       }

       public void setStyle(int styleNr)
       {
               currentStyleIndex=styleNr;
       }
       public int currentStyleIndex
       {
           get { return _currentStyleIndex; }
           set
           {
               if (value == _currentStyleIndex) return;
               if (value >= styleSheets.Count || value<0)
               {
                   Debug.LogWarning("Invalid style requested (" + value + "), ignoring");
                   return;
               }
               _currentStyleIndex = value;
               //   currentStyleSheet = styleSheets[_currentStyleIndex];

               if (OnStyleChange != null) OnStyleChange.Invoke();
           }
       }
       public string getStyleName(int styleIndex)
       {
           if (styleIndex < 0 || styleIndex > currentStyleSheet.styles.Count)
           {
               Debug.Log("invalid style name requested " + styleIndex, gameObject);
               return "invalid";
           }
           return styleNames[styleIndex];
       }
       public void setStyleName(int styleIndex, string newName,bool rebuild=true)
       {
       //    Debug.Log("Setting name " + styleIndex + " to " + newName);
           if (styleIndex < 0 || styleIndex > currentStyleSheet.styles.Count)
               Debug.Log("invalid style name set requested " + styleIndex, gameObject);
           //  if (!currentStyleSheet.styles[styleIndex].Equals(newName))
           for (int i = 0; i < styleSheets.Count; i++)
               styleSheets[i].styles[styleIndex].styleName = newName;
            if (rebuild)  RebuildDictionary();
       }

       private void OnEnable()
       {
           if (instance != null && instance != this)
               DestroyImmediate(gameObject);

           instance = this;
           if (styleSheets == null || styleSheets.Count == 0)
           {
               styleSheets = new List<StyleSheet>();
               styleSheets.Add(new StyleSheet("Default"));
           }
       }

       void Reset()
       {
           styleSheets = new List<StyleSheet>();
           styleSheets.Add(new StyleSheet("Default"));
           currentStyleIndex = 0;
           RebuildDictionary();
           // currentStyleSheet = styleSheets[0];
       }
       public void DuplicateStyle(int styleIndex)
       {
           for (int i = 0; i < styleSheets.Count; i++)
           {
               Style thisStyle = new Style(styleSheets[i].styles[styleIndex]);
               styleSheets[i].styles.Insert(styleIndex + 1, thisStyle);
           }
           RebuildDictionary();
       }
       public void MoveStyle(int styleIndex, int targetIndex)
       {
           Debug.Log("moving from " + styleIndex + " to " + targetIndex);
           for (int i = 0; i < styleSheets.Count; i++)
           {
               Style thisStyle = styleSheets[i].styles[styleIndex];

               styleSheets[i].styles.RemoveAt(styleIndex);
               styleSheets[i].styles.Insert(targetIndex, thisStyle);
           }
           RebuildDictionary();
       }
       public void DuplicateSheet(int sheetIndex)
       {
           StyleSheet newSheet = new StyleSheet(styleSheets[sheetIndex]);
           styleSheets.Insert(sheetIndex + 1, newSheet);
       }
       public void MoveSheet(int SheetIndex, int targetIndex)
       {
           StyleSheet newSheet = styleSheets[SheetIndex];
           styleSheets.RemoveAt(SheetIndex);
           styleSheets.Insert(targetIndex, newSheet);
       }
       public void RemoveStyle(int styleIndex)
       {
           for (int i = 0; i < styleSheets.Count; i++)
               styleSheets[i].styles.RemoveAt(styleIndex);
           RebuildDictionary();
       }
       void OnDisable()
       {
           instance = null;
       }

   }*/
}
