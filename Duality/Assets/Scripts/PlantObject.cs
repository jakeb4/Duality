using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantObject : MonoBehaviour {

    public float maxSize;
    public float growFactor;
    public float waitTime;
    public float CoolDown;
    public Vector3 MaxSizes = new Vector3(1,1,1);
    public static bool max = false;
    Plant_Button plantButton;

    void Start()
    {
        CoolDown = 0f;
        plantButton = gameObject.GetComponentInParent<Plant_Button>();
    }
    void Update()
    {
        
            if (CoolDown == 0f)
            {
                StartCoroutine(Scale());
            }
            else if(CoolDown != 0f)
                CoolDown -= Time.deltaTime;
        
    }

    IEnumerator Scale()
    {
        if (plantButton != null)
        {
            if (plantButton.pressed == true)
            {
                yield return new WaitForSeconds(waitTime);

                float timer = 0;

                while (plantButton.pressed == true) // this could also be a condition indicating "alive or dead"
                {
                    // we scale all axis, so they will have the same value, 
                    // so we can work with a float instead of comparing vectors
                    while (maxSize > transform.localScale.x)
                    {
                        timer += Time.deltaTime;
                        transform.localScale += MaxSizes * growFactor;

                        yield return null;
                    }
                    if (transform.localScale != MaxSizes)
                    {
                        transform.localScale = MaxSizes;
                        plantButton.pressed = false;
                        yield break;
                    }
                  

                    yield break;
                    // reset the timer

                }
                plantButton.pressed = false;

            }
        }
        
            
    }
}

