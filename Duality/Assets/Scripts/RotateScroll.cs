using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScroll : MonoBehaviour {

    public float distance = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        gameObject.transform.Rotate(0,Input.GetAxis("Mouse ScrollWheel") * distance,  0, Space.Self);	
	}
}
