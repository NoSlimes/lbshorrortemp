using UnityEngine;

public class BatterySpawner : MonoBehaviour
{
    public GameObject Battery;
    GameObject[] spawnBatteryPoints;
    GameObject currentPoint;
    int index;
    [SerializeField]int batteryAmount = 3;

    private void Start()
    {
        while(batteryAmount > 0){
            Quaternion randomRotation = new Quaternion(0, Random.Range(0, 359), 90, 0);
            spawnBatteryPoints = GameObject.FindGameObjectsWithTag("batterypoint"); //Finds all gameObjects with tag "enemypoint" and adds it to the array spawnEnemyPoints
            index = Random.Range(0, spawnBatteryPoints.Length); //Picks a gameObject at random from the array
            currentPoint = spawnBatteryPoints[index]; //The point that was picked
            GameObject battery = Instantiate(Battery, currentPoint.transform.position, randomRotation); //Instantiates the enemy gameObject at the point
            currentPoint.transform.parent = battery.transform;
            Debug.Log(currentPoint.name);
            Destroy(currentPoint);
            batteryAmount --;
        }
    }
}
