using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantCollection : MonoBehaviour {

    public Camera cam;
    public float speedBoost = 0.5f;
    public Animator anim;
    public Animation Clip;
    public static bool pressed;
    public Vector3 originalScale = new Vector3(.2f,.2f,.2f);
    public Vector3 MaxScale = new Vector3(1, 1, 1);
    public float Speedtimer;
    
    vp_FPController controller;

    // Use this for initialization
    void Start ()
    {
        controller = gameObject.GetComponent<vp_FPController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        { // if left button pressed...
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 2))
            {
                if (hit.collider.gameObject.tag == "Plant_WM")
                {
                    if (hit.collider.gameObject.transform.localScale == MaxScale)
                    {
                        pressed = false;
                        //where all the shit happens
                        //speed boost * .5 for 10 sec
                        hit.collider.gameObject.transform.localScale = originalScale;
                        //PlantObject.max = false;
                    }
                    
                }
                if (hit.collider.gameObject.tag == "Plant_Button")
                {
                    //pressed = true;
                    //print("hit Button");
                    //anim.enabled = true;
                    //Invoke("AnimEnable", 1);
                    //animate button
                }
                if (hit.collider.gameObject.tag == "Plant_BB")
                {                                                                                        
                    if (hit.collider.gameObject.transform.localScale == MaxScale)
                    {
                        pressed = false;
                        //where all the shit happens
                        if (controller.StateEnabled("Default"))
                        {
                            controller.SetState("MegaSpeed");
                        }
                        hit.collider.gameObject.transform.localScale = originalScale;
                        //PlantObject.max = false;
                    }
                }
            }
        }
        if (controller.StateEnabled("MegaSpeed"))
        {
            if (Speedtimer > 0)
            {
                Speedtimer -= Time.deltaTime;
            }
            else if (Speedtimer <= 0)
            {
                controller.SetState("MegaSpeed", false);
                controller.SetState("Defaul", true);
            }
           
        }
    }

    void AnimEnable()
    {
        anim.enabled = false;
        
    }
}
