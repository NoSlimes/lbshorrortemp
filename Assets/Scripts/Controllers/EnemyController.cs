using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookDistance = 10f;
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
            Animator anim = other.GetComponentInParent<Animator>();
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("DoorOpen"))
                return;

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("DoorClose"))
                anim.SetTrigger("OpenClose");
        }
    }

    //When enemy exits a trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Door")
        {
            Animator anim = other.GetComponentInParent<Animator>();
            anim.SetTrigger("OpenClose");
        }
    }

    #endregion

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = (target.position - transform.position);
        float distance = Vector3.Distance(target.position, transform.position);
        
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir, out hit, ignore))
        {
            if (hit.transform.tag == "Player")
            {
                agent.SetDestination(hit.transform.position);

                if (distance <= agent.stoppingDistance)
                {
                    faceTarget();
                }
                isPlayerDetected = true;
            }
            else isPlayerDetected = false;

          //  Debug.Log(hit.transform.tag);
        }
            Debug.DrawRay(transform.position, dir * lookDistance, Color.blue);
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
}
