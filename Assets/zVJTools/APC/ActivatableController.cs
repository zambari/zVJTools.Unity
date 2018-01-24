using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ActivatableController : MonoBehaviour
{
    public bool solo;
    protected Activatable currentlyActive;
    protected Activatable[] activatables;
    public virtual void ActiveNotification(Activatable newActive)
    {
        if (currentlyActive != null)
        {
            if (solo)
                currentlyActive.gameObject.SetActive(false);
        }
        currentlyActive = newActive;
    }



    protected virtual void getActivatables()
    {
        List<Activatable> activatatableList = new List<Activatable>();
        for (int i = 0; i < transform.childCount; i++)
        {
            Activatable thisActivatable = transform.GetChild(i).GetComponent<Activatable>();
            if (thisActivatable != null) activatatableList.Add(thisActivatable);

        }
        activatables = activatatableList.ToArray();
    }




    void OnEnable()
    {
        getActivatables();
    }

    void Start()
    {
        getActivatables();
    }


}
