using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant_Button : MonoBehaviour {

    private List<GameObject> fruit;
    private Transform parentTransform;
    public Transform player;
    private Animator anim;
    public GameObject button;
    public float DistanceFromPlayer = 3.0f;
    public bool pressed = false;

	// Use this for initialization
	void Start ()
    {
        
        //fruit = GameObject.FindGameObjectsWithTag("Fruit");
        parentTransform = this.transform;
        //player = GameObject.FindGameObjectWithTag("MainCamera").transform;
        anim = gameObject.GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        foreach (Transform child in parentTransform)
        {
            if (child.CompareTag("Plant_Button"))
            {
                button = child.gameObject;
            }
        }
    }

    void OnMouseDown()
    {
        //pressed = true;
        //anim.enabled = true;
        //Invoke("DisableAnim", 1);
        if ((button.transform.position - player.position).magnitude <= DistanceFromPlayer)
        {
            pressed = true;
            anim.enabled = true;
            Invoke("DisableAnim",1);
        }
    }

    void DisableAnim()
    {
        anim.enabled = false;
    }
}
