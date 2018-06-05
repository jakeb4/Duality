using UnityEngine;
using UnityEngine.AI;

// Use physics raycast hit from mouse click to set agent destination
[RequireComponent(typeof(NavMeshAgent))]
public class MoveEnemy : MonoBehaviour
{
    NavMeshAgent m_Agent;
    RaycastHit m_HitInfo = new RaycastHit();
    private GameObject player;

    public float lookRadius;
    private bool playerIsClose = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        m_Agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        //if (vp_DamageHandler.dead == false)
       // {
            float distance = Vector3.Distance(player.transform.position, transform.position);

            if (distance <= lookRadius)
            {
                //playerIsClose = true;
                m_Agent.destination = player.transform.position;

            }
        //}
        //if(playerIsClose == true)
        //{
        //    m_Agent.destination = player.transform.position;
        //}

        //if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftShift))
        //{
        //    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    if (Physics.Raycast(ray.origin, ray.direction, out m_HitInfo))
        //        m_Agent.destination = m_HitInfo.point;
        //}
    }
}
