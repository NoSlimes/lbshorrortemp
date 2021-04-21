using System;
using System.Collections;
using UnityEngine;

public class OneTimeDoorCollider : MonoBehaviour
{
   [SerializeField] EnemySpawner enemyspawner;
    void Start()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Door door = GetComponentInChildren<Door>();
            enemyspawner.spawnEnemy();
            door.OpenCloseDoor();
            door.isLocked = true;
        }
    }
}
