using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
[CustomPropertyDrawer(typeof(Range))]
public class RangeDrawer : PropertyDrawer
{
    // Draw the property inside the given rect
    Texture2D texture;
    Texture2D texture2;
    bool isExpanded;
    bool isDraggingIn;
    bool isDraggingOut;
  /*  public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // if (isExpanded) return EditorGUI.GetPropertyHeight(property)*3; else 
        return EditorGUI.GetPropertyHeight(property) * 1;
    }
*/
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

        SerializedProperty floatInProperty = property.FindPropertyRelative("_in");
        SerializedProperty floatOutProperty = property.FindPropertyRelative("_out");
        float inPoint = floatInProperty.floatValue;
        float outPoint = floatOutProperty.floatValue;


        Rect narrowStrip = new Rect(position.x + 90, position.y, position.width - 111, 15);
        float wid = narrowStrip.width;
        Rect narrowStripRange = new Rect(narrowStrip.x + inPoint * wid, narrowStrip.y, (outPoint - inPoint) * wid, narrowStrip.height);
        Rect handleRightRange = new Rect(narrowStripRange.x + narrowStripRange.width, narrowStripRange.y, 10, narrowStripRange.height);
        Rect handleLeftRange = new Rect(narrowStripRange.x - 10, narrowStripRange.y, 10, narrowStripRange.height);
        Event e = Event.current;
        float drag = 0;
        if (e.type == EventType.MouseDrag)
            drag = e.delta.x / Screen.width;
        else
        if (e.type == EventType.MouseUp)
        {
            isDraggingIn = false;
            isDraggingOut = false;
        }
        else
        if (e.type == EventType.MouseDown)
        {

            if (narrowStripRange.Contains(e.mousePosition) && (inPoint + drag >= 0 && outPoint + drag <= 1))
            {
                isDraggingIn = true;
                isDraggingOut = true;
            }

            if (handleLeftRange.Contains(e.mousePosition) && (inPoint + drag >= 0 && inPoint + drag < outPoint))
                isDraggingIn = true;

            if (handleRightRange.Contains(e.mousePosition) && (outPoint + drag <= 1 && outPoint + drag > inPoint))
                isDraggingOut = true;

        }

        Color barColor = Color.green * 0.6f;
        Color barColorDragged = Color.green * 0.8f;
        Color handleColor = Color.green * .3f;
        Color handleColorHovered = Color.green;

        EditorGUI.DrawRect(narrowStrip, Color.grey);


        EditorGUI.DrawRect(narrowStripRange, ((isDraggingIn || isDraggingOut) ? barColorDragged : barColor));
        EditorGUI.DrawRect(handleLeftRange, (isDraggingIn ? handleColorHovered : handleColor));
        EditorGUI.DrawRect(handleRightRange, (isDraggingOut ? handleColorHovered : handleColor));

        if (narrowStripRange.width > 70)
        {
            Rect valueLabelLeft = new Rect(handleLeftRange.x + handleLeftRange.width + 2, handleLeftRange.y + 2, 50, handleLeftRange.height);
            Rect valueLabelRight = new Rect(handleRightRange.x - 27, handleRightRange.y + 2, 50, handleRightRange.height);

            GUIStyle st = new GUIStyle();
            st.fontSize = 9;

            string inS = (Mathf.Round(inPoint * 100) / 100).ToString();
            string outS = (Mathf.Round(outPoint * 100) / 100).ToString();

            st.normal.textColor = (isDraggingIn ? Color.white : Color.white * 0.75f);

            GUI.Label(valueLabelLeft, inS, st);

            st.normal.textColor = (isDraggingOut ? Color.white : Color.white * 0.75f);
            GUI.Label(valueLabelRight, outS, st);
        }
        if (drag != 0)
        {

            if (isDraggingIn && inPoint + drag >= 0 && inPoint + drag < outPoint)
                if (!isDraggingOut || outPoint + drag < 1)
                    floatInProperty.floatValue = inPoint + drag;

            if (isDraggingOut && outPoint + drag <= 1 && outPoint + drag > inPoint)
                if (!isDraggingIn || inPoint + drag >= 0)
                    floatOutProperty.floatValue = outPoint + drag;
        }

        isExpanded = EditorGUI.Foldout(position, isExpanded, label);
        /*
        if (isExpanded) {





          narrowStrip=new Rect(narrowStrip.x,narrowStrip.y+20,narrowStrip.width,narrowStrip.height);
         // EditorGUI.Slider(narrowStrip, 0, 0, 1);
            //floatInProperty.floatValue=EditorGUI.Slider(narrowStrip, inPoint,0,1);
         // EditorGUI.PropertyField (narrowStrip, floatInProperty, GUIContent.none);
          narrowStrip=new Rect(narrowStrip.x,narrowStrip.y+20,narrowStrip.width,narrowStrip.height);
          //floatOutProperty.floatValue=EditorGUI.Slider(narrowStrip, outPoint,0,1);
         // EditorGUI.PropertyField (narrowStrip, floatOutProperty, GUIContent.none);

        }*/
        //EditorGUI.PropertyField (nameRect, property.FindPropertyRelative ("name"), GUIContent.none);

        //  EditorGUI.EndProperty ();
    }


}