using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;
    public float wanderRadius = 100f;
    private bool isPlayerDetected;

    Transform target;
    NavMeshAgent agent;
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Door")
        {
            Animator anim = other.GetComponentInParent<Animator>();
            anim.SetTrigger("OpenClose");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Door")
        {
            Animator anim = other.GetComponentInParent<Animator>();
            anim.SetTrigger("OpenClose");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = (target.position - transform.position);
        float distance = Vector3.Distance(target.position, transform.position);


        if (distance <= lookRadius)
         {
             agent.SetDestination(target.position);

             if (distance <= agent.stoppingDistance)
             {
                 faceTarget();
             }
             isPlayerDetected = true;
         }
         else
         {
             isPlayerDetected = false;
         }

        //if (Physics.Raycast(transform.position, direction))


            Debug.DrawRay(transform.position, dir, Color.blue);
    }

    private void FixedUpdate()
    {
        if(!isPlayerDetected)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                agent.SetDestination(agent.RandomPosition(wanderRadius));
            }
        }
    }



    void faceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime*5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
