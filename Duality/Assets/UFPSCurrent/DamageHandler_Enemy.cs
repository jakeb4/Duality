using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.AI;

public class DamageHandler_Enemy : vp_DamageHandler
{

    private Animator animator;

    private NavMeshAgent navmeshagent;

    private GameObject player;

    public float ChaseDistance = 10.0f;

    public static bool kill = false;

    //XPSystem xpsystem;

    public GameObject pistolBullet;

    public GameObject ShotgunShells;
    WanderingAI wander;

    Rigidbody rb;

    private float agentSpeed;

    private Transform parent;
    public static bool enemyinfrontOfDoor = false;

    //reference to Door animator
    private Animator DoorAnim;

    //reference to Door Parent Object
    private GameObject DoorParent;

    //reference to Door Object
    private GameObject Door;

    //reference to DoorInteract script component
    DoorInteract doorScript;

    Interact interact;

    private GameObject doorSound;
    private AudioSource S_doorSound;

    private GameObject doorCloseSound;
    private AudioSource S_doorCloseSound;

    protected override void Awake()
    {
        parent = gameObject.transform.parent;
        base.Awake();
        navmeshagent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agentSpeed = navmeshagent.speed;
    }

    void Start()
    {
        parent = gameObject.transform.parent;
        player = GameObject.FindGameObjectWithTag("PlayerSpawner");
        interact = player.GetComponent<Interact>();
        rb = gameObject.GetComponent<Rigidbody>();
        //xpsystem = player.GetComponent<XPSystem>();
        wander = gameObject.GetComponent<WanderingAI>();
        //wander.enabled = true;
        WanderingAI.wander = true;
    }

    public void Update()
    {
        if (navmeshagent.enabled)
        {
            if (player != null)
            {
                float dist = Vector3.Distance(player.transform.position, this.transform.position);

                if (dist < ChaseDistance)
                {

                    navmeshagent.SetDestination(player.transform.position);
                    //transform.LookAt(player.transform.position);
                    animator.SetBool("Attack", false);
                    animator.SetBool("IsFollow", true);
                    //wander.enabled = false;
                    WanderingAI.wander = false;
                    animator.SetBool("wander", WanderingAI.wander);

                    //if (enemyinfrontOfDoor == true && interact.DoorOpen == true)
                    //{
                    //    navmeshagent.SetDestination(player.transform.position);
                    //    animator.SetBool("Attack", false);
                    //    animator.SetBool("IsFollow", true);
                    //    animator.SetBool("wander", false);
                    //}
                    //else if (enemyinfrontOfDoor == true && interact.DoorOpen == false)
                    //{
                    //    animator.SetBool("Attack", false);
                    //    animator.SetBool("IsFollow", false);
                    //    animator.SetBool("wander", true);
                    //}

                }
                //not near player. Wander.
                else
                {
                    animator.SetBool("Attack", false);
                    animator.SetBool("IsFollow", false);
                    //wander.enabled = true;
                    WanderingAI.wander = true;
                    animator.SetBool("wander", WanderingAI.wander);
                    //navmeshagent.SetDestination(transform.position);
                }

                //attacking
                if (dist < 1.5f)
                {
                    transform.LookAt(player.transform.position);
                    animator.SetBool("Attack", true);
                    animator.SetBool("IsFollow", false);
                    //wander.enabled = false;
                    WanderingAI.wander = false;
                    animator.SetBool("wander", WanderingAI.wander);
                }
                else if (dist > 1.5f && dist < 3.0f)
                {
                    transform.LookAt(player.transform.position);
                    animator.SetBool("Attack", false);
                    animator.SetBool("IsFollow", true);
                    //wander.enabled = false;
                    WanderingAI.wander = false;
                    animator.SetBool("wander", WanderingAI.wander);
                }


            }
        }

        if (enemyinfrontOfDoor == true)
        {
            if (doorScript != null)
            {
                if (enemyinfrontOfDoor)
                {
                    //if (doorScript.isLocked == false)
                    //{
                    Door = DoorParent.transform./*GetChild(1)*/gameObject;
                    DoorAnim = Door.GetComponent<Animator>();
                    //vp_ItemPickup targetItem = hit.transform.gameObject.GetComponent<vp_ItemPickup>();
                    //open door
                    if (DoorAnim.enabled == false)
                    {
                        DoorAnim.enabled = true;
                    }
                    if (doorSound == null)
                    {
                        doorSound = GameObject.Find("DoorOpeningSound");
                        if (doorSound == null)
                        {
                            doorSound = Instantiate(Resources.Load("DoorOpeningSound"), transform.position, transform.rotation) as GameObject;
                        }
                        S_doorSound = doorSound.GetComponent<AudioSource>();
                    }
                    if(S_doorSound.isPlaying)
                    {
                        return;
                    }
                    else
                    {
                        S_doorSound.Play();
                    }
                    Destroy(doorSound, 3.0f);
                    DoorAnim.Play("Open");
                    Invoke("DisableAnim", 1);
                }

                //targetItem.OnKeyPress_Pickup(this.transform.parent.GetComponent<Collider>());
                //debug.log(hit.transform.gameObject.name);

                //}
            }
            //else if light is red then door is locked

        }
    }

    public override void Damage(vp_DamageInfo damageInfo)
    {
        if (CurrentHealth > 0)
        {

            animator.Play("Hit", 0, 0);
            base.Damage(damageInfo);
            //navmeshagent.enabled = false;

            AnimatorStateInfo si = animator.GetCurrentAnimatorStateInfo(0);
            if (si.IsName("Attack"))
            {
                //anim.Play("Hit", 0, 0);
            }
        }

        if (CurrentHealth <= 0.0f)
        {

            animator.SetBool("dead", true);
            animator.Play("Death");
            navmeshagent.enabled = false;
            //Destroy(gameObject, 3.0f);
            //Invoke("SetActive", 3.0f);
            kill = true;
            if (kill == true)
            {
                int randAmmo = UnityEngine.Random.Range(0, 2);

                if (randAmmo == 1)
                {
                    Instantiate(pistolBullet, gameObject.transform.position, gameObject.transform.rotation);
                }
                else
                {
                    Instantiate(ShotgunShells, gameObject.transform.position, gameObject.transform.rotation);
                }

                kill = false;
                //xpsystem.AddXP();
                // instantiate all the objects

                //Instantiate(Resources.Load("ExpCollectible"), gameObject.transform.position, gameObject.transform.rotation);

            }
            Die();
        }
    }
    void SetActive()
    {
        gameObject.SetActive(false);
    }
    public override void Die()
    {
        if (!enabled || !vp_Utility.IsActive(gameObject))
            return;
        if (m_Audio != null)
        {
            m_Audio.pitch = Time.deltaTime;
            m_Audio.PlayOneShot(DeathSound);
        }
        Destroy(GetComponent<vp_SurfaceIdentifier>());

        Destroy(gameObject, 3.0f);


    }

    public void OnHitEnd()
    {
        navmeshagent.enabled = true;
    }

    public void EndAttack()
    {
        float dist = Vector3.Distance(player.transform.position, this.transform.position);

        if (dist < 2.0f)
        {
            player.SendMessage("Damage", 4.0f, SendMessageOptions.DontRequireReceiver);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Door")
        {
            DoorParent = col.gameObject.transform.gameObject;
            doorScript = DoorParent.GetComponent<DoorInteract>();
            if (SwitchDimensions.Switched == true)
            {
                if (doorScript.keyCardA == true)
                {
                    if (interact.KeyCardACollected)
                    {


                        enemyinfrontOfDoor = true;
                        //navmeshagent.speed = 0;
                        //Invoke("EnableAgent", 1.0f);
                    }
                }
            }
            if (SwitchDimensions.Switched == true)
            {
                if (doorScript.keyCardB)
                {
                    if (interact.KeyCardBCollected)
                    {
                        enemyinfrontOfDoor = true;
                        //navmeshagent.speed = 0;
                        //Invoke("EnableAgent", 1.0f);

                    }
                }
            }
            if (SwitchDimensions.Switched == true)
            {
                if (!doorScript.keyCardB && !doorScript.keyCardA)
                {

                    enemyinfrontOfDoor = true;
                    //navmeshagent.speed = 0;
                    //Invoke("EnableAgent", 1.0f);


                }
            }
        }

    }
    void OnTriggerExit(Collider col)
    {
        if (SwitchDimensions.Switched == true)
        {
            if (col.gameObject.tag == "Door")
            {
                enemyinfrontOfDoor = false;
                if (DoorAnim != null)
                {
                    DoorAnim.Play("Close");
                    if(doorCloseSound == null)
                    {
                        doorCloseSound = GameObject.Find("DoorClosingSound");
                        if(doorCloseSound == null)
                        {
                            doorCloseSound = Instantiate(Resources.Load("DoorClosingSound"), transform.position, transform.rotation) as GameObject;
                        }
                        S_doorCloseSound = doorCloseSound.GetComponent<AudioSource>();
                        
                    }
                    if (S_doorCloseSound.isPlaying == false)
                    {
                        S_doorCloseSound.Play();
                    }
                    else
                    {
                        return;
                    }
                    Destroy(doorCloseSound, 3.0f);
                }
            }
        }
    }

    void EnableAgent()
    {
        navmeshagent.speed = agentSpeed;
    }

}