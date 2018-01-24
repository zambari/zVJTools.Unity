using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#pragma warning disable 414
#pragma warning disable 219
//zambari 2017
public class NameHelper
{
    MonoBehaviour mono;
    static char seperator = '\uFEFF'; // unicode whitespace
    const string extraSpace = "  ";
    string baseName;
    bool isMangledCached;
    bool useSecondTag = false;
    string _tag = "";
    string _tagPost = "";
    public bool isMangled
    {
        get
        {
            //     if (!useCached)
            return (mono.name.Contains(seperator.ToString()));
            //     else
            //       return isMangledCached;
        }
    }
 
    public string tag
    {
        get { return _tag; }
        set
        {
            if (useDecorators)
                _tag = decoA + value + decoB;
            else
                _tag = value;
            setName();
        }
    }
    public string tagPost
    {
        get { return _tagPost; }
        set
        {
            if (value == null || value.Length == 0) useSecondTag = false; else useSecondTag = true;
            _tagPost = value;
            setName();
        }
    }
    public bool useDecorators;
    string decoA = "[";
    string decoB = "]";
    string decoratedTag;

/*    public string decorate(string source)
    {
        if (!useDecorators) return source;
        return decoA + source + decoB;
    }

    void setDecorators(string a, string b)
    {
        useDecorators = true;
        decoratedTag = a + _tag + b;

    }*/
    void setName()
    {
        if (useSecondTag)
            mono.name = tag + extraSpace + seperator + unMangled + seperator + extraSpace + _tagPost;
        else
            mono.name = tag + extraSpace + seperator + unMangled;
    }
    public void SetTag(string t)
    {
        _tag = t;
        setName();
    }
    public void SetTagPost(string t)
    {
        useSecondTag = true;
        _tagPost = t;
        setName();
    }
    public void RemoveTag()
    {
        mono.name = unMangled;
    }
    public string removeTag
    {
        get { RemoveTag(); return null; }
        set { RemoveTag(); }
    }
    public static string GetWithoutTags(string s)
    {
        string[] split = s.Split(seperator);
        if (split.Length < 2) return s;
        return split[1];
    }
    //  bool useCached;
    public string unMangled
    {
        get
        {
            //   if (!useCached)
            //   {
            string[] split = mono.name.Split(seperator);
            if (split.Length < 2) return mono.name;
            return split[1];
            //   }
            //    else
            //         return baseName;
        }
    }
    public NameHelper(MonoBehaviour source)
    {
        mono = source;
        baseName = mono.name;
    }

}
