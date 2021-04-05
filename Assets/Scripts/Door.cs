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
            GetComponentInParent<Animator>().SetTrigger("OpenClose"); //Set the animator to the animator of the gameObject currently looked at
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("doorLocked");
           // StartCoroutine(_lockedPopUp());

        }
    }

    public void DoorOpenSFX()
    {
        
    }

    public void DoorCloseSFX()
    {

    }

    public void DoorSlamSFX()
    {

    }

    /*IEnumerator _lockedPopUp()
    {

    }
    */
}
