using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteract : MonoBehaviour {

    //public GameObject LockLight;
    //public Material LockLightMat;
    //public static bool locked;
    public bool isLocked;

    //Close to door check
    private bool nearDoor;
    //[SerializeField]
    private Animator anim;

    private GameObject door;
    Interact interact;
    public bool keyCardA;
    public bool keyCardB;
    public bool opened;
    public bool AlwaysLocked;

    DamageHandler_Enemy enemy;

    // Use this for initialization
    void Start ()
    {
        opened = false;
        //locked = true;
        isLocked = true;
        //LockLight = gameObject.transform.GetChild(3).gameObject;
        //anim = door.GetComponent<Animator>();
        door = gameObject.transform.GetChild(0).gameObject;
        anim = GetComponent<Animator>();
        
        //LockLightMat = LockLight.GetComponent<Renderer>().material;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (interact != null)
        {
            if (keyCardA == true && !AlwaysLocked)
            {
                //door need card A
                if (interact.KeyCardACollected == true)
                {
                    isLocked = false;
                }
                else if (interact.KeyCardACollected == false)
                {
                    isLocked = true;
                }
            }
            else if (keyCardB == true && !AlwaysLocked)
            {
                if (interact.KeyCardBCollected == true)
                {
                    isLocked = false;
                }
                else
                {
                    isLocked = true;
                }
                //door need card B
            }
            else if(keyCardA == false && keyCardB == false && !AlwaysLocked)
            {
                isLocked = false;
            }

            else if(AlwaysLocked)
            {
                isLocked = true;
            }
        }
        //LockLightMat = LockLight.GetComponent<Renderer>().material;
        //if (LockLightMat != null)
        //{
            //print(LockLightMat.name);
            //if (LockLightMat.name == "GreenLight (Instance)")
            //{
            //    //unlocked
            //    isLocked = false;
            //    locked = false;
            //}
            //else if (LockLightMat.name == "RedLight (Instance)")
            //{
            //    isLocked = true;
            //    locked = true;
            //    //locked
            //}
            //else if (LockLightMat.name == "AlwaysOpen (Instance)")
            //{
            //    isLocked = false;
            //    locked = false;
            //}
            //else if (LockLightMat.name == "AlwaysClose (Instance)")
            //{
            //    isLocked = true;
            //    locked = true;
            //}
            //else
            //{
            //    locked = false;
            //}
        //}
	}

    //Close to door check
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "PlayerSpawner")
        {
            nearDoor = true;
            interact = col.gameObject.GetComponent<Interact>();
        }
        //if(col.gameObject.tag != "PlayerSpawner" && col.gameObject.tag != "Enemy")
        //{
        //    anim.Play("Close");
        //}
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "PlayerSpawner")
        {
            if (interact.DoorOpen == true)
            {
                nearDoor = false;
                //anim.SetBool("isNear", false);
                GameObject sound = Instantiate(Resources.Load("DoorClosingSound"), transform.position, transform.rotation) as GameObject;
                AudioSource doorclose = sound.GetComponent<AudioSource>();
                doorclose.Play();
                Destroy(sound, 3.0f);
                anim.Play("Close");
                interact.DoorOpen = false;
            }
            
        }
        //if (col.gameObject.tag == "Enemy")
        //{
        //    enemy = col.gameObject.GetComponent<DamageHandler_Enemy>();
        //    if (enemy.DoorOpened == true)
        //    {
        //        //nearDoor = false;
        //        //anim.SetBool("isNear", false);
        //        GameObject sound = Instantiate(Resources.Load("DoorClosingSound"), transform.position, transform.rotation) as GameObject;
        //        Destroy(sound, 3.0f);
        //        anim.Play("Close");
        //        interact.DoorOpen = false;
        //    }

        //}
    }

    public void Open()
    {
        anim.Play("Open");
        opened = true;
    }
    public void Close()
    {
        anim.Play("Close");
        opened = false;
    }
}
