using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSetLocalScale : MonoBehaviour
{

    public float minScale = 0.5f;
    public float maxScale = 1.5f;
    public void SetLocalScale(float f)
    {
        float k = minScale + (maxScale - minScale) * f;
        transform.localScale = new Vector3(k, k, k);
    }
}
