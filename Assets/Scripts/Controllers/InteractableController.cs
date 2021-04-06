using UnityEngine;
using UnityEngine.UI;

public class InteractableController : MonoBehaviour
{
    public float interactableRange=5f;
    public LayerMask InteractableLayer;
    public GameObject Cursor;
    private void Update()
    {
        bool interact = Input.GetButtonDown("Interact");
        Debug.DrawRay(transform.position, transform.forward * interactableRange, Color.white);
            RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, interactableRange, InteractableLayer))
        {
            if (interact)
            {
                //If the ray hits a gameObject with the tag "Door"
                if (hit.transform.tag == "Door")
                {
                    Door door = hit.transform.GetComponent<Door>();
                    door.openCloseDoor();
                }

                //If the ray hits a gameObject with the tag "Battery"
                if (hit.transform.tag == "Battery")
                {
                    Battery battery = hit.transform.GetComponent<Battery>();
                    battery.yoinkBattery();
                }
            }
            Cursor.SetActive(true);
        }
        else Cursor.SetActive(false);
    }
}
