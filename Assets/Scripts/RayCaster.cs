using UnityEngine;

public class RayCaster : MonoBehaviour
{
    public float interactableRange=5f;
    public LayerMask InteractableLayer;
    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * interactableRange, Color.white);
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, interactableRange, InteractableLayer))
            {

                if (hit.transform.tag == "Door")
                {
                    Animator anim = hit.transform.GetComponentInParent<Animator>();
                    if (Input.GetKeyDown(KeyCode.E))
                        anim.SetTrigger("OpenClose");


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
        }
        
    }
}
