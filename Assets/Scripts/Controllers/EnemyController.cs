using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookDistance = 50f;
    public float wanderRadius = 100f;
    private bool isPlayerDetected;

    public LayerMask ignore;
    Transform target;
    NavMeshAgent agent;
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();

    }

    #region Triggers
    //When enemy enters a trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Door")
        {
            Animator anim = other.GetComponentInParent<Animator>(); //Set the animator to the animator of the gameObject the enemy currently is at
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("DoorOpen")) //Checks the state of the animator, returns if the door is open
                return;

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("DoorClose")) //Checks the state of the animator, opens the door if the door is already closed
                anim.SetTrigger("OpenClose");
        }
    }

    //When enemy exits a trigger
    private void OnTriggerExit(Collider other)
    {   
        Animator anim = other.GetComponentInParent<Animator>(); //Set the animator to the animator of the gameObject the enemy currently is at
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("DoorClose")) //Checks the state of the animator, returns if the door is closed
            return;

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("DoorOpen")) //Checks the state of the animator, closes the door if the door is already open
            anim.SetTrigger("OpenClose");
    }

    #endregion

    // Update is called once per frame
    void Update()
    {
        Vector3 origin = transform.position + Vector3.up * 3;
        Vector3 targetHeight = target.position + Vector3.up * 0.5f;
        Vector3 dir = (targetHeight - origin);
        float distance = Vector3.Distance(target.position, transform.position);
        
        RaycastHit hit;

        //Draws a raycast towards the player
        if (Physics.Raycast(origin, dir, out hit, lookDistance, ~ignore))
        {   
            //If the raycast hits a gameObject with the tag "Player"
            if (hit.transform.tag == "Player")
            {
                agent.SetDestination(hit.transform.position);

                //If the enemy is closer to the player than the set stopping distance, turn towards the player
                if (distance <= agent.stoppingDistance)
                {
                    faceTarget();
                }
                isPlayerDetected = true;
                Debug.DrawRay(origin, dir * 1, Color.blue);
            } 
        }
        else 
            {
                isPlayerDetected = false;
                Debug.DrawRay(origin, dir * 1, Color.red);
            }
    }

    private void FixedUpdate()
    {
        //Sets the enemy to roam around randomly when player is not hit by the raycast
        if(!isPlayerDetected)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                agent.SetDestination(agent.RandomPosition(wanderRadius));
            }
        }

        Debug.Log(isPlayerDetected);
    }



    void faceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime*5f);
    }
}
