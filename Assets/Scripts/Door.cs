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
            GetComponent<NavMeshObstacle>().enabled = true;
        }
        else
        {
            GetComponent<NavMeshObstacle>().enabled = false;
        }
           
    }
    public void openCloseDoor()
    {
        if (!isLocked)
        {
            Animator anim = GetComponentInParent<Animator>(); //Set the animator to the animator of the Door currently looked at
                anim.SetTrigger("OpenClose");
            Debug.Log(anim.GetCurrentAnimatorStateInfo(0));
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
