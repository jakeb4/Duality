using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseEventSystemScript : MonoBehaviour {

    EventSystem es;
    public GameObject Settings;
    public GameObject Resolution;
    public GameObject Pause;
    private bool ResoisSelected;
    private bool ResumeIsSelected;

    // Use this for initialization
    void Start()
    {
        ResoisSelected = false;
        ResumeIsSelected = false;
        es = gameObject.GetComponent<EventSystem>();
        es.firstSelectedGameObject = Pause;
    }

    // Update is called once per frame
    void Update()
    {
        if (Settings.activeInHierarchy == true)
        {
            if (ResoisSelected == false)
            {
                if (es.currentSelectedGameObject != Resolution)
                {
                    if (es.currentSelectedGameObject != null)
                    {
                        es.SetSelectedGameObject(Resolution);
                        ResoisSelected = true;
                        ResumeIsSelected = false;
                    }
                    else
                    {
                        Resolution = es.currentSelectedGameObject;
                        //isSelected = true;
                    }
                }
            }
        }
        else
        {
            //es.firstSelectedGameObject = Campaign;
            ResoisSelected = false;
            if (ResumeIsSelected == false)
            {
                if (es.currentSelectedGameObject != Pause)
                {
                    if (es.currentSelectedGameObject != null)
                    {
                        es.SetSelectedGameObject(Pause);
                        ResumeIsSelected = true;
                    }
                    else
                    {
                        Pause = es.currentSelectedGameObject;
                        //isSelected = true;
                    }
                }
            }

        }
    }
}
