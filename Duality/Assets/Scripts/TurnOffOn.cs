using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TurnOffOn : MonoBehaviour {


    private NavMeshAgent agent;
    private WanderingAI wanderScript;
    private DamageHandler_Enemy enemyScript;


    // Use this for initialization
    void Start ()
    {
        wanderScript = gameObject.GetComponent<WanderingAI>();
        enemyScript = gameObject.GetComponent<DamageHandler_Enemy>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.enabled = false;
        wanderScript.enabled = false;
        enemyScript.enabled = false;
        Invoke("EnableNav", .2f);
        Invoke("EnableWander", .3f);
        Invoke("EnableEnemy", .4f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void EnableNav()
    {
        agent.enabled = true;
    }
    void EnableWander()
    {
        wanderScript.enabled = true;
    }
    void EnableEnemy()
    {
        enemyScript.enabled = true;
    }
}
