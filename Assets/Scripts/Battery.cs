using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    public void PickUpBattery()
    {
        PlayerController.currentBatteries += 1;         //Adds 1 to the current amount of batteries the player has got
        Debug.Log(PlayerController.currentBatteries);
        FindObjectOfType<AudioManager>().Play("pickUpItem");
        Destroy(this.gameObject);         //Destroys the battery gameObject
    }

}
