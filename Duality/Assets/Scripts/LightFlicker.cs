using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour {
	
	public float flicker = 0.25f;
	
	float intensity;
	
	// Use this for initialization
	void Start () {
        intensity = gameObject.GetComponent<Light>().intensity;
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.GetComponent<Light>().intensity = intensity * Random.Range (1 - flicker, 1 + flicker);
	}
}
