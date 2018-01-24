//v0.1
using UnityEngine;

public class MoveRel : MonoRect
{
    [Header("MoveRel will set positon relative to parent")]
    [Header("1 = same as parent")]
    public bool previewnPositionX = true;
    public bool previewnPositionY = false;
    public bool previewSizeX = false;
    public bool previewSizeY = false;


    [Range(0, 1)]
    public float previewSettingValue;

    public bool setAnchors;
    [Header("Use public set methods in runtime")]
    [ReadOnly]
    public int recievedEventCount;
    void Reset()
    {
        parentRect.pivot=Vector2.zero;
        //rect.anchorMin=Vector2.zero;
        rect.pivot=Vector2.zero;
     //   rect.anchorMax=Vector2.zero;
    }

    void Start()
    {
        previewnPositionX = false;
        previewnPositionY = false;
        previewSizeX = false;
        previewSizeY = false;

    }
    protected  void OnValidate()
    {
        if (!isActiveAndEnabled) return;
        if (setAnchors)
            rect.setAnchorsX(0, 0);


        if (transform.parent == null) Debug.LogError("Please only use Move Rel on objects that have a Parent");
        if (rect == null) Debug.LogError("Please only use Move Rel on objects that have a Rect Transfrom ");
        if (parentRect == null) Debug.LogError("Please only use Move Rel on objects which parent have a Rect Transfrom ");
        if (previewSizeY)
        {
            previewnPositionX = false;
            previewnPositionY = false;
            previewSizeX = false;
            setRelativeSizeY(previewSettingValue);

        }
        else
           if (previewSizeX)
        {
            previewnPositionX = false;
            previewnPositionY = false;
            previewSizeY = false;
            setRelativeSizeX(previewSettingValue);
        }
        else
           if (previewnPositionX)
        {

            previewnPositionY = false;
            previewSizeX = false;
            previewSizeY = false;
            setRelativePosX(previewSettingValue);
        }
        else
         if (previewnPositionY)
        {
            previewnPositionX = false;
            previewSizeX = false;
            previewSizeY = false;
            setRelativePosY(previewSettingValue);
        }

        recievedEventCount = 0;

    }


    public void setRelativePosX(float f)
    {
        recievedEventCount++;
        previewSettingValue = f;
        rect.setRelativeLocalX(parentRect, f);
    }
    public void setSizeX(float f)
    {
        recievedEventCount++;
        previewSettingValue = f;
        rect.setSizeX(f);
    }
    public void setRelativeSizeX(float f)
    {
        recievedEventCount++;
        previewSettingValue = f;
        rect.setRelativeSizeX(parentRect, f);
    }

    public void setRelativePosY(float f)
    {
        recievedEventCount++;
        previewSettingValue = f;
        rect.setRelativeLocalY(parentRect, f);
    }
    public void setSizeY(float f)
    {
        recievedEventCount++;
        previewSettingValue = f;
        rect.setSizeY(f);
    }
    public void setRelativeSizeY(float f)
    {
        recievedEventCount++;
        previewSettingValue = f;

        rect.setRelativeSizeY(parentRect, f);
    }
}
