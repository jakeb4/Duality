using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class gunUI : MonoBehaviour
{

    [SerializeField]
    private GameObject pistol;
    [SerializeField]
    private GameObject shotgun;

    public GameObject pistolIcon;
    public GameObject shotgunIcon;

    private Vector3 pistolLoc;
    private Vector3 shotgunLoc;
    private Vector3 ShrinkedpistolSc;
    private Vector3 LargershotgunSc;
    private Vector3 N_pistolSc;
    private Vector3 N_shotgunSc;

    vp_PlayerInventory inventory = null;
    vp_ItemInstance pistolObject = null;
    vp_ItemInstance shotgunObject = null;
    string pistolName = "Pistol01";
    string shotgunName = "Shotgun01";
    vp_ItemIdentifier ident;
    bool moved = false;

    private float Alpha;

    // Use this for initialization
    void Start()
    {
        inventory = gameObject.GetComponent<vp_PlayerInventory>();
        pistolLoc = pistolIcon.transform.position;
        shotgunLoc = shotgunIcon.transform.position;
        LargershotgunSc = shotgunIcon.transform.localScale / 2;
        ShrinkedpistolSc = pistolIcon.transform.localScale / 2;
        N_shotgunSc = shotgunIcon.transform.localScale;
        N_pistolSc = pistolIcon.transform.localScale;
        shotgunIcon.SetActive(false);
        pistolIcon.SetActive(false);
        //shotgunIcon.GetComponent<Image>().color = new Color(0, 0, 0, .5f);
    }

    // Update is called once per frame
    void Update()
    {
        pistol = GameObject.Find("1PistolTransform");
        pistolObject = inventory.GetItem(pistolName) as vp_UnitBankInstance;
        shotgunObject = inventory.GetItem(shotgunName) as vp_UnitBankInstance;
        if (pistolObject != null)
        {
            if (pistol == null)
            {
                pistol = GameObject.FindGameObjectWithTag("pistol");
            }
            print(pistolObject.Type.name);
            pistolIcon.SetActive(true);

        }
        if (shotgunObject != null)
        {
            shotgunIcon.SetActive(true);
            if (shotgun == null)
            {  
                shotgun = GameObject.FindGameObjectWithTag("shotgun");
            }
            
            print(shotgunObject.Type.name);
            
        }
        UpdateUI();


    }

    void UpdateUI()
    {
        if (pistol != null)
        {
            if (pistolObject != null)
            {
                pistolIcon.SetActive(true);
                if (pistol.activeInHierarchy)
                {
                    if (moved == false)
                    {
                        pistolIcon.transform.position = pistolLoc;
                        shotgunIcon.transform.position = shotgunLoc;
                        moved = true;
                    }
                    pistolIcon.SetActive(true);

                    //pistolIcon.GetComponent<Image>().color = new Color(0, 0, 0, 1);
                    if (shotgun != null)
                    {
                        //shotgunIcon.GetComponent<Image>().color = new Color(0, 0, 0, .5f);
                        shotgunIcon.SetActive(true);
                    }
                    else
                        print("shotgun is null");

                }
            }
            else
            {
                //print("its null!");
                pistolIcon.SetActive(false);
            }
        }



        if (shotgun != null)
        {
            if (shotgunObject != null)
            {
                if (shotgun.activeInHierarchy)
                {
                    shotgunIcon.SetActive(true);
                    //shotgunIcon.GetComponent<Image>().color = new Color(0, 0, 0, 1);
                    
                    if (moved == true)
                    {
                        pistolIcon.transform.position = new Vector3(shotgunLoc.x - 105.2f, shotgunLoc.y, shotgunLoc.z);
                        shotgunIcon.transform.position = new Vector3(pistolLoc.x + 141.2f, pistolLoc.y, pistolLoc.z);
                        moved = false;
                    }
                    else if(moved == false && pistolObject == null)
                    {
                        pistolIcon.transform.position = new Vector3(shotgunLoc.x - 105.2f, shotgunLoc.y, shotgunLoc.z);
                        shotgunIcon.transform.position = new Vector3(pistolLoc.x + 141.2f, pistolLoc.y, pistolLoc.z);
                        moved = false;
                    }
                    if (pistolObject != null)
                    {
                        pistolIcon.SetActive(true);
                        //pistolIcon.GetComponent<Image>().color = new Color(0, 0, 0, .5f);
                    }
                }
                else
                {
                    //shotgunIcon.GetComponent<Image>().color = new Color(0, 0, 0, .5f);
                }
            }
            else
            {
                shotgunIcon.SetActive(false);
                
            }
        }
        //else
        //{
        //    shotgunIcon.SetActive(false);
        //}
    }
}
