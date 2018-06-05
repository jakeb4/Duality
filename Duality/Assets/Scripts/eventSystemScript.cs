using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class eventSystemScript : MonoBehaviour {

    EventSystem es;
    public GameObject Settings;
    public GameObject Resolution;
    public GameObject Campaign;
    private bool ResoisSelected;
    private bool CamIsSelected;

    // Use this for initialization
    void Start ()
    {
        ResoisSelected = false;
        CamIsSelected = false;
        es = gameObject.GetComponent<EventSystem>();
        es.firstSelectedGameObject = Campaign;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(Settings.activeInHierarchy == true)
        {
            if (ResoisSelected == false)
            {
                if (es.currentSelectedGameObject != Resolution)
                {
                    if (es.currentSelectedGameObject != null)
                    {
                        es.SetSelectedGameObject(Resolution);
                        ResoisSelected = true;
                        CamIsSelected = false;
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
            if (CamIsSelected == false)
            {
                if (es.currentSelectedGameObject != Campaign)
                {
                    if (es.currentSelectedGameObject != null)
                    {
                        es.SetSelectedGameObject(Campaign);
                        CamIsSelected = true;
                    }
                    else
                    {
                        Campaign = es.currentSelectedGameObject;
                        //isSelected = true;
                    }
                }
            }
            
        }
	}
}
