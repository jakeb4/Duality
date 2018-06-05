using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Interact : MonoBehaviour
{
    //reference to UI popup text
    private vp_FPPlayerEventHandler m_Player;

    //reference to button animator
    [SerializeField]
    private Animator buttonAnim;

    //reference to Door animator
    private Animator DoorAnim;

    //reference to Door Parent Object
    private GameObject DoorParent;
    private GameObject childofDoor;

    //reference to Door Object
    private GameObject Door;

    //reference to DoorInteract script component
    DoorInteract doorScript;

    //check if player is near battery
    private bool nearBattery;

    //check if battery is collected by player
    public bool batteryCollected;

    //Reference to BatteryIcon UI Element
    public GameObject batteryIcon;
    private GameObject Battery;
    //private bool NearButton;

    //Reference to AudioTape
    private AudioSource audioSource;
    private float Audiolength;
    private bool isPlaying = false;
    private bool inAudioField = false;
    private GameObject audioField;

    //private GameObject alienTech;

    //reference to KeyCards
    public GameObject KeyCardA;
    public GameObject KeyCardB;
    bool nearKeyCardA = false;
    bool nearKeyCardB = false;
    public bool KeyCardACollected;
    public bool KeyCardBCollected;

    public NavMeshSurface surface;
    private bool navmeshCheck;

    public bool DoorOpen;

    public GameObject pickUpSound;
    private GameObject pickupSoundClone;
    private AudioSource pickupSoundSource;
    private GameObject sound;
    private bool soundPlayed;
    private bool pressed;
    private bool inFrontOfObject = false;

    void Start()
    {
        pressed = false;
        soundPlayed = false;
        pickupSoundSource = pickUpSound.GetComponent<AudioSource>();
        navmeshCheck = false;
        //Hide Ui element for battery bc the player hasn't collected it.
        if (batteryIcon != null)
        {
            batteryIcon.SetActive(false);
        }

        if(KeyCardA != null)
        {
            KeyCardA.SetActive(false);
        }

        if (KeyCardB != null)
        {
            KeyCardB.SetActive(false);
        }

        KeyCardACollected = false;
        KeyCardBCollected = false;

        //get the player event handler component script to access UI popup text.
        m_Player = this.GetComponent<vp_FPPlayerEventHandler>();

    }

    void Update()
    {

        //checks if player is infront of object
        if (inFrontOfObject)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1.25f))
            {
                // collect battery
                if (hit.collider.gameObject.tag == "Battery")
                {
                    if (batteryCollected == false)
                    {
                        m_Player.HUDText.Send("Press E to Pick Up");

                        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown("joystick button 2"))
                        {
                            //collect battery
                            batteryCollected = true;
                            batteryIcon.SetActive(true);
                            hit.collider.gameObject.SetActive(false);
                            //pickupSoundClone = Instantiate(pickUpSound, transform.position, Quaternion.identity);
                            pickupSoundSource.Play();
                        }
                    }
                    else if (batteryCollected == true)
                    {
                        m_Player.HUDText.Send("You already have one!");
                    }
                }

                if (hit.collider.gameObject.tag == "AudioTape")
                {
                    m_Player.HUDText.Send("Press E to Listen");
                    if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown("joystick button 2"))
                    {
                        //play Audio
                        if (isPlaying == false)
                        {
                            audioSource = hit.collider.gameObject.GetComponent<AudioSource>();
                            Audiolength = audioSource.clip.length;
                            audioSource.enabled = true;
                            Invoke("DiableAudio", Audiolength);
                            isPlaying = true;
                        }

                    }
                }

                if (hit.collider.gameObject.tag == "KeyCardA")
                {
                    m_Player.HUDText.Send("Press E to PickUp");
                    if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown("joystick button 2"))
                    {
                        //pick it up
                        KeyCardACollected = true;
                        KeyCardA.SetActive(true);
                        hit.collider.gameObject.SetActive(false);
                        pickupSoundSource.Play();
                    }
                }
                if (hit.collider.gameObject.tag == "KeyCardB")
                {
                    m_Player.HUDText.Send("Press E to PickUp");
                    if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown("joystick button 2"))
                    {
                        //pick it up]
                        KeyCardBCollected = true;
                        KeyCardB.SetActive(true);
                        hit.collider.gameObject.SetActive(false);
                        pickupSoundSource.Play();
                    }
                }

                //if light is green it is unlocked
                if (hit.collider.gameObject.tag == "DoorCollider")
                {

                    //buttonAnim = hit.collider.gameObject.GetComponent<Animator>();
                    DoorParent = hit.collider.gameObject.transform.parent.gameObject;
                    print(DoorParent);
                    childofDoor = DoorParent.transform.GetChild(0).gameObject;
                    //buttonAnim = childofDoor.transform.GetComponentInChildren<Animator>();
                    doorScript = DoorParent.GetComponent<DoorInteract>();

                    if (doorScript.isLocked == false)
                    {
                        Door = DoorParent.transform./*GetChild(0)*/gameObject;
                        DoorAnim = Door.GetComponent<Animator>();
                        //vp_ItemPickup targetItem = hit.transform.gameObject.GetComponent<vp_ItemPickup>();

                        //Show pick up message here, "Press E to pick up item".
                        if (DoorAnim.enabled != true)
                        {
                            m_Player.HUDText.Send("Press E to Open Door");
                        }
                        m_Player.HUDText.Send("Press E to Open Door");

                        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown("joystick button 2"))
                        {
                            //animate button
                            //open door
                            //buttonAnim.enabled = true;
                            pressed = true;
                            if (DoorAnim.enabled == false)
                            {
                                DoorAnim.enabled = true;
                            }

                            if (!soundPlayed)
                            {
                                soundPlayed = true;
                                sound = Instantiate(Resources.Load("DoorOpeningSound"), transform.position, transform.rotation) as GameObject;
                                AudioSource doorOpen = sound.GetComponent<AudioSource>();
                                doorOpen.Play();
                            }
                            Destroy(sound, 3.0f);
                            DoorOpen = true;
                            DoorAnim.Play("Open");
                            Invoke("DisableAnim", 1f);

                            //targetItem.OnKeyPress_Pickup(this.transform.parent.GetComponent<Collider>());
                            //debug.log(hit.transform.gameObject.name);
                        }
                    }
                    //else if light is red then door is locked
                    else if (doorScript.isLocked == true)
                    {
                        //anim = hit.collider.gameObject.GetComponent<Animator>();
                        //Show pick up message here, "Press F to pick up item".
                        if (pressed == false)
                        {
                            m_Player.HUDText.Send("Press E to Open Door");
                        }

                        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown("joystick button 2"))
                        {
                            pressed = true;
                            Invoke("DisableAnim", 1);
                            m_Player.HUDText.Send("Door is Locked.");
                        }
                    }
                }
            }
        }
    }
    //Disable the button animator function
    void DisableAnim()
    {
        pressed = false;
        //surface.BuildNavMesh();
        if (navmeshCheck == false)
        {
                navmeshCheck = true;
        }
    }

    //Check if near battery using Trigger Collision.
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Battery")
        {
            nearBattery = true;
        }
        if (col.gameObject.tag == "AudioField")
        {

            if (isPlaying == false)
            {
                audioSource = col.gameObject.GetComponent<AudioSource>();
                Audiolength = audioSource.clip.length;
                audioSource.enabled = true;
                isPlaying = true;
                audioField = col.gameObject;
                Invoke("DiableAudio", Audiolength);
            }
        }
        if(col.gameObject.tag == "Battery" || col.gameObject.tag == "DoorCollider" || col.gameObject.tag == "KeyCardB" || col.gameObject.tag == "KeyCardA" || col.gameObject.tag == "AudioTape" || col.gameObject.tag == "Door")
        {
            //in front of object
            inFrontOfObject = true;
        }
    }

    //if(col.gameObject.tag == "KeyCardA")
    //{
    //    nearKeyCardA = true;
    //}
    //if (col.gameObject.tag == "KeyCardB")
    //{
    //    nearKeyCardB = true;
    //}

    //if (col.gameObject.tag == "AlienTech")
    //{
    //    alienTech = col.gameObject;
    //    NearButton = true;
    //}


    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Door")
        {
            soundPlayed = false;
        }
        if (col.gameObject.tag == "Battery" || col.gameObject.tag == "DoorCollider" || col.gameObject.tag == "KeyCardB" || col.gameObject.tag == "KeyCardA" || col.gameObject.tag == "AudioTape" || col.gameObject.tag == "Door")
        {
            inFrontOfObject = false;
        }
    }

    void DiableAudio()
    {
        isPlaying = false;
        audioSource.enabled = false;
        audioField.SetActive(false);
    }

}




