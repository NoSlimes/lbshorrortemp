using System.Collections;
using UnityEngine;

public class Key : MonoBehaviour
{
    public enum KeyNr{key1, key2, key3}
    public KeyNr keyNr;

    public void PickUpKey()
    {
        if (keyNr == KeyNr.key1)
        {
            InteractableController.hasKey1 = true;
        }
        else if (keyNr == KeyNr.key2)
        {
            InteractableController.hasKey2 = true;
        }
        else if (keyNr == KeyNr.key3)
        {
            InteractableController.hasKey3 = true;
        }
        Destroy(gameObject);
        FindObjectOfType<AudioManager>().Play("pickUpItem");
    }

}

