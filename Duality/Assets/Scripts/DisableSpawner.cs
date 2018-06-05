using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableSpawner : MonoBehaviour {


    public GameObject Spawner1;
    public GameObject Spawner2;



    // Use this for initialization
    void Start ()
    {
        Spawner1.gameObject.SetActive(true);
        Spawner2.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerExit(Collider col)
    {
        if(col.gameObject.tag == "DisableSpawner")
        {
            Spawner1.gameObject.SetActive(false);
            Spawner2.gameObject.SetActive(true);
        }
    }
}
