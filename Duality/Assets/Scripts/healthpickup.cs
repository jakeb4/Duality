using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthpickup : MonoBehaviour {

    //reference to player health script
    vp_FPPlayerDamageHandler health;

    //amount of health increase
    public float healthamount = 2;
    //private int curShipHealth = 100;

    void Start ()
    {
        //get health script from player gameObject
        health = gameObject.GetComponent<vp_FPPlayerDamageHandler>();

        StartCoroutine(addHealth());
    }

    void OnTriggerEnter(Collider col)
    {
        //check iof player collides with health
        if (col.gameObject.tag == "health")
        {
            //add health and disable object
            col.gameObject.SetActive(false);
            health.CurrentHealth += healthamount;
        }
    }

    IEnumerator addHealth()
    {
        while (true)
        { // loops forever...
            if (health.CurrentHealth < 10)
            { // if health < 100...
                health.CurrentHealth += .1f; // increase health and wait the specified time
                yield return new WaitForSeconds(1);
            }
            else
            { // if health >= 100, just yield 
                yield return null;
            }
        }
    }
}
