using System.Collections;
using System;
using UnityEngine;
using UnityEngine.AI;

public class Door : MonoBehaviour
{
    [SerializeField]public bool isLocked;

    private void Update()
    {
        if (isLocked)
        {
            NavMeshObstacle obstacle = GetComponent<NavMeshObstacle>();
            obstacle.enabled = true;
        }
        else
        {
            NavMeshObstacle obstacle = GetComponent<NavMeshObstacle>();
            obstacle.enabled = false;
        }
           
    }
    public void openCloseDoor()
    {
        if (!isLocked)
        {
            Animator anim = GetComponentInParent<Animator>(); //Set the animator to the animator of the gameObject currently looked at
            anim.SetTrigger("OpenClose");

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("DoorOpen")) //Checks the state of the animator, returns true if the door is open
                Debug.Log("Door open");

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("DoorClose")) //Checks the state of the animator, returns true if the door is closed
                Debug.Log("Door closed");
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("doorLocked");
           // StartCoroutine(_lockedPopUp());

        }
    }

    /*IEnumerator _lockedPopUp()
    {

    }
    */
}
