using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderingAI : MonoBehaviour
{

    public float wanderRadius;
    public float wanderTimer;

    private Transform target;
    private NavMeshAgent agent;
    private float timer;
    public static bool wander;

    private Animator anim;


    // Use this for initialization
    void OnEnable()
    {
        //wander = true;
        
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (wander == true)
        {
            timer += Time.deltaTime;

            if (timer >= wanderTimer)
            {
                anim.SetBool("wander", wander);
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                if (newPos != null)
                {
                    if (agent.enabled == true)
                    {
                        if (agent.gameObject.activeInHierarchy == true)
                        {
                            agent.SetDestination(newPos);
                        }
                    }
                }
                timer = 0;
            }
        }
        
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    




}
