// v.0.4 2017.07.31. startrd numbering those
// v0.41 texture clear
// v0.42 byte[] conversions added
// v0.43 2017.08.29 
// v0.44 random string 
// v0.45 FromJson/ToJson alternative naming 0.45.1 adds AssetDatabase.Refresh to make file visible in editor
// v0.46 perform on gameobejctselection
// v0.47 fromjson comment escaping

using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif


/// oeverrides zRectExtensions
public static class zExtensions
{

    public static GameObject[] GetChildrenGameObjects(this GameObject go)
    {
        return GetChildrenGameObjects(go.transform);
    }
#if UNITY_EDITOR
public static void PerformAction(this GameObject[] selection, Action<GameObject> actionToPerform)
{
    if (actionToPerform==null) return;
    for (int i=0;i<selection.Length;i++)
    {
        actionToPerform(selection[i]);
    }
}
#endif
    public static GameObject[] GetChildrenGameObjects(this Transform transform)
    {
        GameObject[] children = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i).gameObject;
        }
        return children;
    }
    public static Color baseColor = new Color(1f / 6, 1f / 2, 1f / 2, 1f / 2); //?
    public static char[] ToCharArray(this byte[] b, int len = 0) // 2017.08.18
    {
        if (len == 0) len = b.Length;
        if (len == -1) return new char[1];
        Debug.Log(len);
        char[] c = new char[len];
        for (int i = 0; i < len; i++)
            c[i] = (char)b[i];
        return c;
    }

    public static void DestroySmart(this Component c)
    {

        if (Application.isPlaying)
        {
            MonoBehaviour.Destroy(c);
        }
        else
        {
#if UNITY_EDITOR
            EditorApplication.delayCall += () => MonoBehaviour.DestroyImmediate(c);
#endif
        }


    }
    public static byte[] ToByteArray(this string s)// 2017.08.18
    {
        byte[] byteArray = new byte[s.Length];
        for (int i = 0; i < s.Length; i++)
            byteArray[i] = (byte)s[i];
        return byteArray;
    }
    public static string ArrayToString(this byte[] b) // 2017.08.18
    {
        string s = "";
        for (int i = 0; i < b.Length; i++)
        {
            if (b[0] == 0) return s;
            s += (char)b[i];
        }
        return s;
    }
    public static bool executeIfTrue(this bool condition, Action ac)
    {
        if (condition) ac.Invoke();
        return false;
    }
    public static void CollapseComponent(this MonoBehaviour mono, Component c, bool expanded = false)
    {
#if UNITY_EDITOR
        if (c != null)
            UnityEditorInternal.InternalEditorUtility.SetIsInspectorExpanded(c, false);
#endif
    }
    public static void CollapseComponent(this MonoBehaviour mono, bool expanded = false)
    {
        Component c = mono;
#if UNITY_EDITOR
        if (c != null)
            UnityEditorInternal.InternalEditorUtility.SetIsInspectorExpanded(c, false);
#endif
    }


    public static void CollapseComponent(this Component c, bool expanded = false)
    {
#if UNITY_EDITOR
        if (c != null)
            UnityEditorInternal.InternalEditorUtility.SetIsInspectorExpanded(c, false);
#endif
    }
    /// <summary>
    /// Fades a colour

    /// </summary>

    public static void setAlpha(this Color c, float a)
    {
        c.alpha(a);
    }
    public static Color alpha(this Color c, float a)
    {
        Color m = new Color(c.r, c.g, c.b, a);
        return m;

    }

    public static float randomizeRespectingNormalisation(this float f, float howMuch)
    {
        float n = f + UnityEngine.Random.value * howMuch - howMuch / 2;
        if (n < 0) n = 0;
        if (n > 1) n = 1;


        return n;

    }
    public static Color randomize(this Color c, float howMuchHue = 0.15f, float howMuchSat = 0.3f, float howMuchL = 0.2f)
    {
        float H, S, L;
        Color.RGBToHSV(c, out H, out S, out L);
        H = H.randomizeRespectingNormalisation(howMuchHue);
        S = S.randomizeRespectingNormalisation(howMuchSat);
        L = L.randomizeRespectingNormalisation(howMuchL);
        Color newCol = Color.HSVToRGB(H, S, L);
        newCol.a = c.a;
        return newCol;
    }

    public static Vector3 RandomizeXY(this Vector3 r, float range = 300, bool allowNegatives = true)

    {

        float x = r.x + UnityEngine.Random.value * range;
        float y = r.y + UnityEngine.Random.value * range;
        if (!allowNegatives)
        {
            if (x < 0) x = x * -1;
            if (y < 0) y = y * -1;
        }
        return new Vector3(x, y, r.z);

    }
    public static Vector2 Randomize(this Vector2 r, float range = 300, bool allowNegatives = true)

    {

        float x = r.x + UnityEngine.Random.value * range;
        float y = r.y + UnityEngine.Random.value * range;
        if (!allowNegatives)
        {
            if (x < 0) x = x * -1;
            if (y < 0) y = y * -1;
        }
        return new Vector2(x, y);

    }


    /// <summary>
    /// Copied from https://gist.github.com/maxattack/4c7b4de00f5c1b95a33b
    /// </summary>
    public static Quaternion SmoothDamp(Quaternion rot, Quaternion target, ref Quaternion deriv, float time)
    {
        // account for double-cover
        var Dot = Quaternion.Dot(rot, target);
        var Multi = Dot > 0f ? 1f : -1f;
        target.x *= Multi;
        target.y *= Multi;
        target.z *= Multi;
        target.w *= Multi;
        // smooth damp (nlerp approx)
        var Result = new Vector4(
            Mathf.SmoothDamp(rot.x, target.x, ref deriv.x, time),
            Mathf.SmoothDamp(rot.y, target.y, ref deriv.y, time),
            Mathf.SmoothDamp(rot.z, target.z, ref deriv.z, time),
            Mathf.SmoothDamp(rot.w, target.w, ref deriv.w, time)
        ).normalized;
        // compute deriv
        var dtInv = 1f / Time.deltaTime;
        deriv.x = (Result.x - rot.x) * dtInv;
        deriv.y = (Result.y - rot.y) * dtInv;
        deriv.z = (Result.z - rot.z) * dtInv;
        deriv.w = (Result.w - rot.w) * dtInv;
        return new Quaternion(Result.x, Result.y, Result.z, Result.w);
    }

    /// <summary>
    /// prints a list of keyframes, in a formsuitable for copy and pasting back to the code to recreate
    /// add a name for it to be present in the output
    /// </summary>
    public static void dumpKeys(this AnimationCurve a, string name = null)
    {
        a.listKeyFramesAsCode(name);
    }
    public static void listKeyFramesAsCode(this AnimationCurve a, string name = null)
    {
        int i = 0;
        string s = "Listing AnimationCurve keyframes\nClick to see full output (multiline) " + (name == null ? " (you can add name too !)" : "") + "  \n\n";

        s += "\n// Begin AnimationCurve dump (copy from here)\n";
        foreach (Keyframe k in a.keys)
        {
            s += name + ".AddKey(new Keyframe(" + k.time + "f," + k.value + "f," + k.inTangent + "f," + k.outTangent + "f));\n";
            i++;
        }
        Debug.Log(s + "// end keyframe dump (created using zExtensions)\n\n");
    }
    public static bool checkFloat(this float f)
    {
        if (Single.IsNaN(f))
        {
            Debug.Log("invalid float (NAN), dividing by zero? !");
            return false;
        }
        return true;
    }

/// <summary>
///  Saves this object as Json file
/// This version adds streamingAssetsPath
/// </summary>

    public static void ToJson(this object obj, string path) // different naming conventino
    {
        obj.saveJson(Application.streamingAssetsPath + "/" +path);
    }

/// <summary>
///  Saves this object as Json file
/// </summary>
/// 
    public static void saveJson(this object obj, string path)
    {
        string dataAsJson = JsonUtility.ToJson(obj,true);
        File.WriteAllText(path, dataAsJson);
        if (File.Exists(path))
        {
            Debug.Log("saved : " + path);
            #if UNITY_EDITOR
            AssetDatabase.Refresh();
            #endif
        }
        else
            Debug.Log("saving failed, file not created : " + path);
    }

/// <summary>
/// Loads an object from json. usage: newObject= newObject.FromJson &lt;typeOfNewObject&gt;(path)
/// this version adds streamingAssetsPath to path string
/// </summary>
    public static T FromJson<T>(this T obj, string path) // different naming conventino
    {
        return obj.loadJson<T>(Application.streamingAssetsPath + "/" +path);
    }

/// <summary>
/// Loads an object from json. usage: newObject= newObject.FromJson&lt;typeOfNewObject&gt;(path)
/// </summary>
    public static T loadJson<T>(this T obj, string path)
    {
        string dataAsJson = File.ReadAllText(path);
        if (dataAsJson == null || dataAsJson.Length < 2)
            Debug.Log("loading file:" + path + " failed");
        else
            obj = JsonUtility.FromJson<T>(dataAsJson);
        return obj;
    }


    public static void AddLayoutElementFlexible(this GameObject g, bool flexible = true)
    {
        LayoutElement le = g.GetComponent<LayoutElement>();
        if (le == null) le = g.AddComponent<LayoutElement>();
        le.flexibleHeight = 1; le.flexibleWidth = 1;
#if UNITY_EDITOR
        UnityEditorInternal.InternalEditorUtility.SetIsInspectorExpanded(le, false);
#endif
    }
    public static string ToShortString(this float f)
    {
        return (f.ToString("F"));
        //return (Mathf.Round(f * 100) / 100).ToString();
    }

    [Obsolete("Use ToShortString instead")]
    public static string ToStringShort(this float f)
    {
        return (Mathf.Round(f * 100) / 100).ToString();

    }

    public static bool shiftPressed()

    {
        return (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));
    }
    // LAYOUT HELPER



    public static T AddOrGetComponent<T>(this GameObject gameObject) where T : Component
    {
        T t = gameObject.GetComponent<T>();
        if (t == null) t = gameObject.AddComponent<T>();
        return t;
    }
    public static string RandomString(int length)
    {
        const string pool = "abcdefghijklmnopqrstuvwxyz0123456789";
        var builder = new StringBuilder();

        for (var i = 0; i < length; i++)
        {
            var c = pool[UnityEngine.Random.Range(0, pool.Length - 1)];
            builder.Append(c);
        }

        return builder.ToString();
    }

    [System.Obsolete("use isActiveAndEnabled - i didn't know it existed")]
    public static bool disabled(this MonoBehaviour source)

    {
        return (!source.enabled || !source.gameObject.activeInHierarchy);

    }
    public static Transform[] GetChildren(this Transform transform)
    {
        Transform[] children = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i);
        }
        return children;
    }

    public static LayoutElement[] getActiveElements(this HorizontalLayoutGroup layout)
    {
        List<LayoutElement> elements = new List<LayoutElement>();
        if (layout == null) return elements.ToArray();
        for (int i = 0; i < layout.transform.childCount; i++)
        {
            GameObject thisChild = layout.transform.GetChild(i).gameObject;
            LayoutElement le = thisChild.GetComponent<LayoutElement>();
            if (le != null)
            {
                if (!le.ignoreLayout) elements.Add(le);
            }
        }
        return elements.ToArray();
    }
    public static string asByteSize(this float byteCount)
    {

        if (byteCount < 10000) return Mathf.Round(byteCount / 1024) + "kb ";
        else
            return (byteCount / (1024 * 1024)).ToShortString() + "MB ";

    }
    public static Color Random(this Color c)
    {
        /*  if (c==null) { c =baseColor; }
          float r=c.r;
          float g=c.g;
          float b=c.b;
          r=r/2+r*UnityEngine.Random.value;
          g=g/2+g*UnityEngine.Random.value;
          g=g/2+b*UnityEngine.Random.value;

          c=new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value, 0.3f);
          return c;*/
        //        Color n=UnityEngine.Random.ColorHSV(0.4f,0.8f,0.3f,0.6f);
        c.a = UnityEngine.Random.value * 0.4f + 0.2f;
        return UnityEngine.Random.ColorHSV(0.4f, 0.8f, 0.3f, 0.6f);
    }
    /// <summary>
    /// slowish but faster to type,   returns rect.GetComponent<Image>();
    /// </summary>

    public static Image image(this RectTransform rect, float transparency = 1)
    {
        Image thisImage = rect.GetComponent<Image>();
        if (thisImage == null)
        {
            thisImage = rect.gameObject.AddComponent<Image>();
            thisImage.color = thisImage.color.Random();
        }
        return thisImage;
    }
    public static RectTransform rect(this GameObject go)
    {
        RectTransform r = go.GetComponent<RectTransform>();
        if (r == null) r = go.AddComponent<RectTransform>();
        return r;
    }
    public static void clear(this Texture2D texture) //, bool apply=true
    {
        texture.clear(Color.black);
    }

    public static void clear(this Texture2D texture, Color fillColor) //, bool apply=true
    {
        Fill(texture, fillColor);
    }
    public static void Fill(this Texture2D texture, Color fillColor) //, bool apply=true
    {
        Color32[] black = new Color32[texture.width * texture.height];
        for (int i = 0; i < black.Length; i++)
            black[i] = fillColor;

        texture.SetPixels32(black);
        texture.Apply();

    }
    public static void Multiply(this Texture2D texture, Color fillColor) //, bool apply=true
    {
        Color32[] colors = texture.GetPixels32();
        for (int i = 0; i < colors.Length; i++)
            colors[i] = colors[i] * fillColor;

        texture.SetPixels32(colors);
        texture.Apply();

    }

    public static void Add(this Texture2D texture, Color fillColor) //, bool apply=true
    {
        Color32[] colors = texture.GetPixels32();
        for (int i = 0; i < colors.Length; i++)
            colors[i] = colors[i] + fillColor;
        texture.SetPixels32(colors);
        texture.Apply();

    }
    /* 
        #if UNITY_EDITOR
    public static void SetTextureImporterFormat( this Texture2D texture, bool isReadable)
    {
        if ( null == texture ) return;

        string assetPath = AssetDatabase.GetAssetPath( texture );
        var tImporter = AssetImporter.GetAtPath( assetPath ) as TextureImporter;
        if ( tImporter != null )
        {
            tImporter.textureType = TextureImporterType.Default;

            tImporter.isReadable = isReadable;

            AssetDatabase.ImportAsset( assetPath );
            AssetDatabase.Refresh();
        }
    }
        #endif*/
}



public static class RectExtensions
{

    public static void removeChildren(this Transform transform, int childIndex = 0)
    {
        int k = 0;
        for (int i = transform.childCount - 1; i >= childIndex; i--)
        {

            GameObject go = transform.GetChild(i).gameObject;
            MonoBehaviour.Destroy(go);
            k++;
        }
        Debug.Log("destroyed " + k + " children of " + transform.name, transform.gameObject);
    }
    public static RectTransform AddChild(this RectTransform parentRect)
    {
        GameObject go = new GameObject();
        RectTransform rect = go.getRect();


        go.transform.SetParent(parentRect);

        rect.anchorMin = new Vector2(0, 0);
        rect.anchorMax = new Vector2(1, 1);
        rect.sizeDelta = new Vector2(10, 10);
        rect.offsetMin = new Vector2(5, 5);
        rect.offsetMax = new Vector2(-5, -5);
        rect.localPosition = Vector2.zero;
        //Debug.Log(" added child to ",parentRect.gameObject);
        //	Debug.Log("new object is",rect.gameObject);

        return rect;
    }


    public static RectTransform AddChild(this GameObject parent)
    {
        RectTransform parentRect = parent.GetComponent<RectTransform>();
        return parentRect.AddChild();
    }
    public static void LayoutParameters(this VerticalLayoutGroup vl)
    {
        vl.spacing = 2;
        vl.padding = new RectOffset(3, 3, 3, 3);
        vl.childControlWidth = true;
        vl.childControlHeight = true;
        vl.childForceExpandHeight = false;
        vl.childForceExpandWidth = false;
    }
    public static void setRelativeSizeX(this RectTransform rect, RectTransform parentRect, float v)
    {
        float sizeX = parentRect.rect.width;
        if (v.checkFloat())
            rect.sizeDelta = new Vector2(sizeX * v, rect.sizeDelta.y);
        else Debug.Log("source is" + rect.name + " parent " + parentRect.name, rect.gameObject);
    }
    public static void setRelativeSizeY(this RectTransform rect, RectTransform parentRect, float v)
    {
        float sizeY = parentRect.rect.height;
        if (v.checkFloat())
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, sizeY * v);
        else Debug.Log("source is" + rect.name + " parent " + parentRect.name, rect.gameObject);
    }
    public static void setRelativeLocalY(this RectTransform rect, RectTransform parentRect, float v)
    {
        float sizeY = parentRect.rect.height;
        if (v.checkFloat())
            rect.localPosition = new Vector2(rect.localPosition.x, sizeY * v);
        else Debug.Log("source is" + rect.name + " parent " + parentRect.name, rect.gameObject);
    }
    public static void setSizeXY(this RectTransform rect, float x, float y)
    {

        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, x);
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, y);

    }
    public static void setSizeX(this RectTransform rect, float v)
    {

        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, v);

    }
    public static void setSizeY(this RectTransform rect, float v)
    {

        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, v);

    }
    public static float getWidth(this RectTransform rect)
    {

        return rect.rect.width;

    }

    public static float getHeight(this RectTransform rect)
    {

        return rect.rect.height;

    }
    public static void setLocalX(this RectTransform rect, float v)
    {
        if (float.IsNaN(v)) return;
        if (rect == null) return;
        rect.localPosition = new Vector2(v, rect.localPosition.y);

    }
    public static void setLocalY(this RectTransform rect, float v)
    {
        rect.localPosition = new Vector2(rect.localPosition.y, v);

    }
    public static void setRelativeLocalX(this RectTransform rect, RectTransform parentRect, float v)
    {
        float sizeX = parentRect.rect.width;
        if (v.checkFloat())
            rect.localPosition = new Vector2(sizeX * v, rect.localPosition.y);
        else Debug.Log("source is" + rect.name + " parent " + parentRect.name, rect.gameObject);

    }

    public static void setRelativeStartX(this RectTransform rect, RectTransform parentRect, float v)
    {

        float sizeX = parentRect.rect.width;
        if (v.checkFloat())
            rect.offsetMin = new Vector2(sizeX * v, rect.offsetMin.y);
        else Debug.Log("source is" + rect.name + " parent " + parentRect.name, rect.gameObject);
    }
    public static void setRelativeEndX(this RectTransform rect, RectTransform parentRect, float v)
    {

        float sizeX = parentRect.rect.width;
        if (v.checkFloat())
            rect.offsetMax = new Vector2(-sizeX * v, rect.offsetMax.y);
        else Debug.Log("source is" + rect.name + " parent " + parentRect.name, rect.gameObject);
    }
    public static void setRelativeStartX(this RectTransform rect, float v)
    {
        if (rect.transform.parent == null)
        {
            Debug.Log("no parent", rect);
            return;
        }
        RectTransform parentRect = rect.transform.parent.GetComponent<RectTransform>();
        if (parentRect == null)
        {
            Debug.Log("no parent RectTransform Component", rect);
            return;
        }
        rect.setRelativeStartX(parentRect, v);

    }
    public static void setRelativeEndX(this RectTransform rect, float v)
    {
        if (rect.transform.parent == null)
        {
            Debug.Log("no parent", rect);
            return;
        }
        RectTransform parentRect = rect.transform.parent.GetComponent<RectTransform>();
        if (parentRect == null)
        {
            Debug.Log("no parent RectTransform Component", rect);
            return;
        }
        rect.setRelativeEndX(parentRect, v);
    }
    public static void setRelativeEndY(this RectTransform rect, float v)
    {
        if (rect.transform.parent == null)
        {
            Debug.Log("no parent", rect);
            return;
        }
        RectTransform parentRect = rect.transform.parent.GetComponent<RectTransform>();
        if (parentRect == null)
        {
            Debug.Log("no parent RectTransform Component", rect);
            return;
        }
        float sizeY = parentRect.rect.height;
        rect.offsetMin = new Vector2(rect.offsetMin.x, sizeY * v);
    }
    public static void setRelativeStartY(this RectTransform rect, float v)
    {
        if (rect.transform.parent == null)
        {
            Debug.Log("no parent", rect);
            return;
        }
        RectTransform parentRect = rect.transform.parent.GetComponent<RectTransform>();
        if (parentRect == null)
        {
            Debug.Log("no parent RectTransform Component", rect);
            return;
        }
        float sizeY = parentRect.rect.height;
        rect.offsetMax = new Vector2(rect.offsetMax.x, -sizeY * v);

    }
    public static void setAnchorLeft(this RectTransform rect, float v)
    {
        rect.anchorMin = new Vector2(v, rect.anchorMin.y);
    }
    public static void setAnchorRight(this RectTransform rect, float v)
    {
        rect.anchorMax = new Vector2(1 - v, rect.anchorMax.y);
    }
    public static void setAnchorTop(this RectTransform rect, float v)
    {
        rect.anchorMax = new Vector2(rect.anchorMax.x, v);
    }
    public static void setAnchorBottom(this RectTransform rect, float v)
    {
        rect.anchorMin = new Vector2(rect.anchorMin.x, v);
    }
    public static void setAnchorsY(this RectTransform rect, float min, float max)
    {
        rect.anchorMin = new Vector2(min, rect.anchorMin.y);
        rect.anchorMax = new Vector2(max, rect.anchorMax.y);
    }
    public static void setAnchorsX(this RectTransform rect, float min, float max)
    {
        rect.anchorMin = new Vector2(rect.anchorMin.x, min);
        rect.anchorMax = new Vector2(rect.anchorMax.y, max);
    }
    public static void setAnchorX(this RectTransform rect, float v)
    {
        rect.anchorMin = new Vector2(v, rect.anchorMin.y);
        rect.anchorMax = new Vector2(v, rect.anchorMax.y);
    }
    public static void setPivotX(this RectTransform rect, float v)
    {
        float deltaPivot = rect.pivot.x - v;
        Vector2 temp = rect.localPosition;
        rect.pivot = new Vector2(v, rect.pivot.y);
        rect.localPosition = temp - new Vector2(deltaPivot * rect.rect.width * rect.localScale.x, 0);
    }
    public static void setPivotY(this RectTransform rect, float v)
    {
        float deltaPivot = rect.pivot.y - v;
        Vector2 temp = rect.localPosition;
        rect.pivot = new Vector2(rect.pivot.x, v);
        rect.localPosition = temp - new Vector2(0, deltaPivot * rect.rect.height * rect.localScale.y);
    }
    public static void setPivot(this RectTransform rect, float x, float y)
    {
        float deltaPivotx = rect.pivot.x - x;
        float deltaPivoty = rect.pivot.y - y;
        Vector2 temp = rect.localPosition;
        rect.pivot = new Vector2(x, y);
        rect.localPosition = temp - new Vector2(deltaPivotx * rect.rect.width * rect.localScale.x, deltaPivoty * rect.rect.height * rect.localScale.y);
    }

    public static void setTopLeftAnchor(this RectTransform rect, Vector2 newAnchor)
    {
        Vector2 temp = rect.sizeDelta;
        rect.anchorMin = new Vector2(newAnchor.x, rect.anchorMin.y);
        rect.anchorMax = new Vector2(rect.anchorMax.x, newAnchor.y);
        rect.sizeDelta = temp;

    }
    public static void setBottomRightAnchor(this RectTransform rect, Vector2 newAnchor)
    {
        rect.anchorMin = new Vector2(rect.anchorMin.x, newAnchor.y);
        rect.anchorMax = new Vector2(newAnchor.x, rect.anchorMax.y);

    }

    public static RectTransform getRect(this GameObject go)
    {
        RectTransform rect = go.GetComponent<RectTransform>();
        if (rect == null) rect = go.AddComponent<RectTransform>();
        return rect;

    }

    /// <summary>
    /// Creates a child recttransorm
    /// </summary>

    public static RectTransform getChild(this RectTransform thisRect)
    {

        RectTransform rect = thisRect.GetComponent<RectTransform>();
        if (rect == null) rect = thisRect.gameObject.AddComponent<RectTransform>();
        return rect;

    }
    /// <summary>
    /// Gets children, if True gets all children of children as well
    /// </summary>

    public static GameObject[] getChildren(this GameObject thisGo, bool deep = false)
    {
        if (!deep)
        {
            Transform t = thisGo.transform;
            GameObject[] c = new GameObject[t.childCount];
            for (int i = 0; i < c.Length; i++)
            {
                c[i] = t.GetChild(i).gameObject;
            }
            return c;
        }
        else
        {
            Transform[] transforms = thisGo.GetComponentsInChildren<Transform>(true);
            GameObject[] c = new GameObject[transforms.Length - 1];
            for (int i = 0; i < c.Length; i++)
            {
                c[i] = transforms[i + 1].gameObject;
            }
            return c;
        }
    }

    /// <summary>
    /// gets children for an array, useful for editor selections 
    /// </summary>

    public static GameObject[] getChildrenArray(this GameObject[] thisGoArray, bool deep = false)
    {
        List<GameObject> children = new List<GameObject>();
        for (int i = 0; i < thisGoArray.Length; i++)
            children.AddRange((thisGoArray[i]).getChildren(deep));


        return children.ToArray();
    }


    public static GameObject[] getAllChildrenCalled(this GameObject[] thisGoArray, string name)
    {
        GameObject[] children = thisGoArray.getChildrenArray(true);
        List<GameObject> namedObjects = new List<GameObject>();
        for (int i = 0; i < children.Length; i++)
        {
            if (children[i].name.Equals(name)) namedObjects.Add(children[i]);
        }
        return namedObjects.ToArray();


    }
}
