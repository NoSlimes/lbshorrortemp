using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    GameObject[] spawnEnemyPoints;
    GameObject currentPoint;
    int index;
    [SerializeField]
    GameObject doorPivot;

    Quaternion currentRot;
    Quaternion targetRot;

    void Start()
    {
        Quaternion currentRot = doorPivot.transform.rotation;
        Quaternion targetRot = new Quaternion(0, 90, 0, 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            spawnEnemy();
            doorPivot.transform.rotation = Quaternion.Slerp(currentRot, targetRot, .1f*Time.deltaTime);
        }
    }

    private void spawnEnemy()
    {
        spawnEnemyPoints = GameObject.FindGameObjectsWithTag("enemypoint"); //Finds all gameObjects with tag "enemypoint" and adds it to the array spawnEnemyPoints
        index = Random.Range(0, spawnEnemyPoints.Length); //Picks a gameObject at random from the array
        currentPoint = spawnEnemyPoints[index]; //The point that was picked
        Instantiate(Enemy, currentPoint.transform); //Instantiates the enemy gameObject at the point
        print(currentPoint.name);
    }
}
