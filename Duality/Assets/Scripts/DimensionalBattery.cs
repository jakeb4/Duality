using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionalBattery : MonoBehaviour
{

    private GameObject Battery;
    public static bool charged;

    // Use this for initialization
    void Start()
    {
        charged = false;
        Battery = gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //if player has battery.
        //use it on this gameobject.
        //charge this gameobject.

        if (Battery == null)
        {
            Battery = gameObject.transform.GetChild(0).gameObject;
        }
        if (Battery.activeInHierarchy == true)
        {
            charged = true;
        }
        else if (Battery.activeInHierarchy == false)
        {
            charged = false;
        }
        else
            charged = true;
    }
}
