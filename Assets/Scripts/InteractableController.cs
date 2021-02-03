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

                if (hit.transform.tag == "Door")
                {
                    Animator anim = hit.transform.GetComponentInParent<Animator>();
                    if (Input.GetKeyDown(KeyCode.E))
                        anim.SetTrigger("OpenClose");

                    if (anim.GetCurrentAnimatorStateInfo(0).IsName("DoorOpen"))
                        Debug.Log("Door open");

                    if (anim.GetCurrentAnimatorStateInfo(0).IsName("DoorClose"))
                        Debug.Log("Door closed");
                }

                if (hit.transform.tag == "Battery")
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        PlayerController.currentBatteries += 1;
                        Debug.Log(PlayerController.currentBatteries);
                        Destroy(hit.transform.gameObject);
                    }

                }
            }
            Cursor.SetActive(true);
        }
        else Cursor.SetActive(false);
    }
}