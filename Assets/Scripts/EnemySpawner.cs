using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    GameObject[] spawnEnemyPoints;
    GameObject currentPoint;
    int index;

    private void Start()
    {
        spawnEnemyPoints = GameObject.FindGameObjectsWithTag("enemypoint");
        index = Random.Range(0, spawnEnemyPoints.Length);
        currentPoint = spawnEnemyPoints[index];
        Instantiate(Enemy, currentPoint.transform);
        print(currentPoint.name);
    }
}
