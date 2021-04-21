using System.Collections;
using System;
using UnityEngine;
using UnityEngine.AI;


public class Door : MonoBehaviour
{
    public bool isLocked;
    [HideInInspector]public Animator anim;
    [SerializeField] bool useKey1;
    [SerializeField] bool useKey2;
    [SerializeField] bool useKey3;

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
    public void LockedCheck()
    {
        if (!isLocked)
        {
            OpenCloseDoor();
        }
        else if (useKey1 && InteractableController.hasKey1)
        {
            OpenCloseDoor();
        }
        else if (useKey2 && InteractableController.hasKey2)
        {
            OpenCloseDoor();
        }
        else if (useKey3 && InteractableController.hasKey3)
        {
            OpenCloseDoor();
        }
        else
        {
            GetComponentInParent<DoorSoundCaller>().Play("DoorLocked");
           // StartCoroutine(_lockedPopUp());
        }

 
    }
    public void OpenCloseDoor()
    {
        anim = GetComponentInParent<Animator>(); //Set the animator to the animator of the Door currently looked at
        anim.SetTrigger("OpenClose");
        Debug.Log(this.anim);
    }


    /*IEnumerator _lockedPopUp()
    {

    }
    */
}
