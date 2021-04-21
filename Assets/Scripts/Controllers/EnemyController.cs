using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private float TimeOutTime = .1f;
    public float lookDistance = 50f;
    public float wanderRadius = 100f;
    public static bool isPlayerDetected;
    private Vector3 targetDestination;

    public LayerMask ignore;
    Transform target;
    NavMeshAgent agent;

    bool stuck = false;
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
            Door door = other.GetComponentInChildren<Door>();
            Animator anim = other.GetComponentInChildren<Animator>(); //Set the animator to the animator of the gameObject the enemy currently is at
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("DoorOpen")) //Checks the state of the animator, returns if the door is open
                return;

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("DoorClose") | anim.GetCurrentAnimatorStateInfo(0).IsName("Start")) //Checks the state of the animator, opens the door if the door is already closed
                door.lockedCheck(); Debug.Log("OPEN");
        }
    }

    //When enemy exits a trigger
    private void OnTriggerExit(Collider other)
    {
        Door door = other.GetComponentInChildren<Door>(); 
        Animator anim = other.GetComponentInChildren<Animator>(); //Set the animator to the animator of the gameObject the enemy currently is at
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("DoorClose")) //Checks the state of the animator, returns if the door is closed
            return;

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("DoorOpen")) //Checks the state of the animator, closes the door if the door is already open
            door.lockedCheck(); Debug.Log("CLOSE");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Enemy")
            Destroy(this.gameObject);
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        float velocity = agent.velocity.magnitude;
        Vector3 origin = transform.position + Vector3.up * 3;
        Vector3 targetHeight = target.position + Vector3.up * 0.5f;
        Vector3 dir = (targetHeight - origin);
        float distance = Vector3.Distance(target.position, transform.position);
        
        RaycastHit hit;

        //Draws a raycast towards the player
        if (Physics.Raycast(origin, dir, out hit, lookDistance, ~ignore))
        {   
            
            //If the raycast hits a gameObject with the tag "Player"
            if (hit.transform.tag == "Player" && distance < lookDistance)
            {   
                
                agent.SetDestination(hit.transform.position);
                //If the enemy is closer to the player than the set stopping distance, stop and turn towards the player
                if (distance <= agent.stoppingDistance)
                {
                    attackPlayer();
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


        Mathf.Clamp(TimeOutTime, 0, TimeOutTime);
        if (velocity < 0.1 && TimeOutTime > 0 && distance > 1)
            TimeOutTime -= Time.deltaTime;
        else TimeOutTime = .1f;

        if(TimeOutTime <= 0f)
            stuck = true;


        
    }

     

    private void FixedUpdate()
    {
        //Sets the enemy to roam around randomly when player is not hit by the raycast
        if(!isPlayerDetected)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                randomRoam();
            }
        }

        if(stuck)
        {
            randomRoam();
            if(isPlayerDetected)
                isPlayerDetected = false;
            Debug.Log("stuck, setting new destination");
            stuck = false;
            TimeOutTime = .1f;
        }
    }

    void randomRoam()
    {
        agent.SetDestination(agent.RandomPosition(wanderRadius));
    }

    void attackPlayer()
    {
        playerSlaughtered.attacked = true;
    }
}
