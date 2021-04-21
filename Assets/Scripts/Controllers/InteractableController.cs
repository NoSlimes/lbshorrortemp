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

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, interactableRange, InteractableLayer))
        {
            if (interact)
            {
                //If the ray hits a gameObject with the tag "Door"
                if (hit.transform.CompareTag("Door"))
                {
                    Door door = hit.transform.GetComponent<Door>();
                    door.LockedCheck();
                }

                //If the ray hits a gameObject with the tag "Battery"
                if (hit.transform.CompareTag("Battery"))
                {
                    Battery battery = hit.transform.GetComponent<Battery>();
                    battery.PickUpBattery();
                }

                if (hit.transform.CompareTag("Key"))
                {
                    Key key = hit.transform.GetComponent<Key>();
                    key.PickUpKey();
                }
            }
            Cursor.SetActive(true);
        }
        else Cursor.SetActive(false);
    }
}
