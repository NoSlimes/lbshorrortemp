using UnityEngine;
using UnityEngine.UI;

public class InteractableController : MonoBehaviour
{
    public float interactableRange=5f;
    public LayerMask InteractableLayer;
    public GameObject Cursor;
    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * interactableRange, Color.white);
            RaycastHit hit;



        if (Physics.Raycast(transform.position, transform.forward, out hit, interactableRange, InteractableLayer))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //If the ray hits a gameObject with the tag "Door"
                if (hit.transform.tag == "Door")
                {
                    Animator anim = hit.transform.GetComponentInParent<Animator>(); //Set the animator to the animator of the gameObject currently looked at
                    if (Input.GetKeyDown(KeyCode.E))
                        anim.SetTrigger("OpenClose");

                    if (anim.GetCurrentAnimatorStateInfo(0).IsName("DoorOpen")) //Checks the state of the animator, returns true if the door is open
                        Debug.Log("Door open");

                    if (anim.GetCurrentAnimatorStateInfo(0).IsName("DoorClose")) //Checks the state of the animator, returns true if the door is closed
                        Debug.Log("Door closed");
                }

                //If the ray hits a gameObject with the tag "Battery"
                if (hit.transform.tag == "Battery")
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        //Adds 1 to the current amount of batteries the player has got
                        PlayerController.currentBatteries += 1;
                        Debug.Log(PlayerController.currentBatteries);
                        //Destroys the battery gameObject
                        Destroy(hit.transform.gameObject);
                    }

                }
            }
            Cursor.SetActive(true);
        }
        else Cursor.SetActive(false);
    }
}
