using UnityEngine;
using UnityEngine.UI;

public class InteractableController : MonoBehaviour
{
    public float interactableRange=5f;
    public LayerMask InteractableLayer;
    public GameObject Cursor;
    [HideInInspector] public static bool hasKey1 = false;
    [HideInInspector] public static bool hasKey2 = false;
    [HideInInspector] public static bool hasKey3 = false;
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
                    door.lockedCheck();
                }

                //If the ray hits a gameObject with the tag "Battery"
                if (hit.transform.tag == "Battery")
                {
                    Battery battery = hit.transform.GetComponent<Battery>();
                    battery.pickUpBattery();
                }

                if (hit.transform.tag == "Key1")
                {
                    hasKey1 = true;
                    FindObjectOfType<AudioManager>().Play("pickUpItem");
                    Destroy(hit.transform.gameObject);
                }
                if (hit.transform.tag == "Key2")
                {
                    hasKey2 = true;
                    FindObjectOfType<AudioManager>().Play("pickUpItem");
                    Destroy(hit.transform.gameObject);
                }
                if (hit.transform.tag == "Key3")
                {
                    hasKey3 = true;
                    FindObjectOfType<AudioManager>().Play("pickUpItem");
                    Destroy(hit.transform.gameObject);
                }
            }
            Cursor.SetActive(true);
        }
        else Cursor.SetActive(false);
    }
}
