using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusDetection : MonoBehaviour
{

    //private Transform[] spawnPoints;

    // Use this for initialization
    void Start()
    {
        //spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");  
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "SpawnPoint")
        {
            col.gameObject.SetActive(true);
        }
        else
        {
            col.gameObject.SetActive(false);
        }

    }
}
