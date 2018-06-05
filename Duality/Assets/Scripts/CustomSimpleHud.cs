using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomSimpleHud : MonoBehaviour {

    vp_SimpleHUD HUD;
    vp_FPPlayerEventHandler playerHealth;
    public Slider health;
    private float healthValue;

    // Use this for initialization
    void Start ()
    {
        HUD = gameObject.GetComponent<vp_SimpleHUD>();
        playerHealth = gameObject.GetComponent<vp_FPPlayerEventHandler>();
        healthValue = playerHealth.Health.Get();
    }
	
	// Update is called once per frame
	void Update ()
    {
        healthValue = playerHealth.Health.Get();
        health.value = healthValue;

    }
}
