using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureChanger : MonoBehaviour {

    public Material RegMaterial;
    public Material DimMaterial;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (SwitchDimensions.Switched == false)
        {
            gameObject.GetComponent<Renderer>().material = RegMaterial;
        }
        if (SwitchDimensions.Switched == true)
        {
            gameObject.GetComponent<Renderer>().material = DimMaterial;
        }
    }
}
